using System;
using System.Reflection;
using System.Text;
using Api.Attributes;
using Api.Configs;
using AutoMapper;
using AutoMapper.EntityFrameworkCore;
using AutoMapper.EquivalencyExpression;
using Dal;
using IdentityServer4.Models;
using Lamar;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Logging;
using Microsoft.Extensions.Logging.Abstractions;
using Microsoft.IdentityModel.Tokens;
using Models.Models;
using Swashbuckle.AspNetCore.Swagger;

namespace Api
{
    public class Startup
    {
        private readonly IConfigurationRoot _configuration;

        private readonly IHostingEnvironment _env;

        /// <summary>
        /// Constructor
        /// </summary>
        /// <param name="env"></param>
        public Startup(IHostingEnvironment env)
        {
            _env = env;

            var builder = new ConfigurationBuilder()
                .SetBasePath(env.ContentRootPath)
                .AddJsonFile("appsettings.json", true, true)
                .AddJsonFile($"appsettings.{env.EnvironmentName}.json", true)
                .AddEnvironmentVariables();

            _configuration = builder.Build();
        }

        /// <summary>
        /// This method gets called by the runtime. Use this method to add services to the container.
        /// For more information on how to configure your application, visit https://go.microsoft.com/fwlink/?LinkID=398940
        /// </summary>
        /// <param name="services"></param>
        public void ConfigureContainer(ServiceRegistry services)
        {
            services.AddLogging();

            var jwtSettings = new JwtSettings();
            var section = _configuration.GetSection("JwtSettings");
            section.Bind(jwtSettings);
            services.Configure<JwtSettings>(section);
            
            services
                .AddAuthentication(x =>
                {
                    x.DefaultAuthenticateScheme = JwtBearerDefaults.AuthenticationScheme;
                    x.DefaultChallengeScheme = JwtBearerDefaults.AuthenticationScheme;
                })
                .AddJwtBearer(JwtBearerDefaults.AuthenticationScheme, x =>
                {
                    x.RequireHttpsMetadata = false;
                    x.SaveToken = true;
                    x.TokenValidationParameters = new TokenValidationParameters
                    {
                        ValidIssuer = jwtSettings.Issuer,
                        ValidAudience = jwtSettings.Audience,
                        IssuerSigningKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(jwtSettings.Key)),
                        ValidateIssuerSigningKey = true,
                        ValidateLifetime = true,
                        ClockSkew = TimeSpan.Zero // the default for this setting is 5 minutes
                    };
                });

            // If environment is localhost, then enable CORS policy, otherwise no cross-origin access
            services.AddCors(options =>
            {
                options.AddPolicy("CorsPolicy",
                    builder =>
                    {
                        builder.AllowAnyOrigin()
                            .AllowAnyMethod()
                            .AllowAnyHeader()
                            .AllowCredentials();
                    });
            });

            var entityDbContext = new EntityDbContext(opt =>
            {
                opt.UseSqlite(_configuration.GetValue<string>("ConnectionStrings:Sqlite"));
            });

            services.For<EntityDbContext>().Use(entityDbContext).Transient();

            // All the other service configuration.
            services.AddAutoMapper(opt =>
            {
                opt.AddProfiles(Assembly.Load("Models"));
                opt.AddCollectionMappers();
                opt.SetGeneratePropertyMaps(
                    new GenerateEntityFrameworkCorePrimaryKeyPropertyMaps<EntityDbContext>(entityDbContext.Model));
            });

            services.AddDbContext<EntityDbContext>();

            services.AddIdentity<User, IdentityRole>()
                .AddEntityFrameworkStores<EntityDbContext>()
                .AddDefaultTokenProviders()
                .AddClaimsPrincipalFactory<CustomUserClaimsPrincipalFactory>();
            
            services.AddMvc().SetCompatibilityVersion(Microsoft.AspNetCore.Mvc.CompatibilityVersion.Version_2_1);

            services.Configure<IISOptions>(iis =>
            {
                iis.AuthenticationDisplayName = "Windows";
                iis.AutomaticAuthentication = false;
            });

            var identityServerBuilderbuilder = services
                .AddIdentityServer(opt =>
                {
                    opt.Events.RaiseErrorEvents = true;
                    opt.Events.RaiseInformationEvents = true;
                    opt.Events.RaiseFailureEvents = true;
                    opt.Events.RaiseSuccessEvents = true;
                })
                .AddInMemoryIdentityResources(new IdentityResource[]
                {
                    new IdentityResources.OpenId(),
                    new IdentityResources.Profile()
                })
                .AddInMemoryApiResources(new []{ new ApiResource("api1", "My API #1") })
                .AddAspNetIdentity<EntityDbContext>();

            if (_env.IsDevelopment())
            {
                identityServerBuilderbuilder.AddDeveloperSigningCredential();
            }

            services.AddAuthentication();
            
            services.AddSwaggerGen(opt =>
            {
                opt.SwaggerDoc("v1", new Info
                {
                    Title = "dotnet-intermediate-workshop", Version = "v1",
                    Description = "Workshop exercise application"
                });
            });

            // Also exposes Lamar specific registrations
            // and functionality
            services.Scan(_ =>
            {
                _.TheCallingAssembly();
                _.Assembly("Dal");
                _.Assembly("Logic");
                _.WithDefaultConventions();
            });

            // Null logger for now
            services.For<ILoggerFactory>().Use(NullLoggerFactory.Instance);

            services.For<IConfigurationRoot>().Use(_ => _configuration);
        }

        /// <summary>
        /// This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
        /// </summary>
        /// <param name="app"></param>
        /// <param name="env"></param>
        public void Configure(IApplicationBuilder app, IHostingEnvironment env)
        {
            app.UseCors("CorsPolicy");

            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
                app.UseDatabaseErrorPage();
            }

            app.UseStaticFiles();
            app.UseIdentityServer();
            app.UseMvcWithDefaultRoute();

            // Enable middleware to serve generated Swagger as a JSON endpoint.
            app.UseSwagger();

            // Enable middleware to serve swagger-ui (HTML, JS, CSS, etc.), 
            // specifying the Swagger JSON endpoint.
            app.UseSwaggerUI(opt => { opt.SwaggerEndpoint("/swagger/v1/swagger.json", "My API V1"); });
        }
    }
}