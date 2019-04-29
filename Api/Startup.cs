using System.Reflection;
using System.Text;
using Api.Configs;
using AutoMapper;
using AutoMapper.EntityFrameworkCore;
using AutoMapper.EquivalencyExpression;
using Dal;
using Lamar;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.HttpOverrides;
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

        public readonly IHostingEnvironment _env;

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

            // Configure Entity Framework Identity for Auth
            services.AddIdentity<User, IdentityRole>(x =>
                {
                    x.User.RequireUniqueEmail = true;
                })
                .AddEntityFrameworkStores<EntityDbContext>()
                .AddDefaultTokenProviders();

            services.AddAuthentication(options =>
                {
                    options.DefaultAuthenticateScheme = JwtBearerDefaults.AuthenticationScheme;
                    options.DefaultChallengeScheme = JwtBearerDefaults.AuthenticationScheme;
                })
                .AddJwtBearer(config =>
                {
                    config.RequireHttpsMetadata = false;
                    config.SaveToken = true;

                    config.TokenValidationParameters = new TokenValidationParameters()
                    {
                        ValidIssuer = _configuration.GetValue<string>("JwtSettings:Issuer"),
                        ValidAudience = _configuration.GetValue<string>("JwtSettings:Issuer"),
                        IssuerSigningKey =
                            new SymmetricSecurityKey(
                                Encoding.UTF8.GetBytes(_configuration.GetValue<string>("JwtSettings:Key")))
                    };
                });

            services.Configure<JwtSettings>(_configuration.GetSection("JwtSettings"));

            // Add framework services
            services.AddMvc().AddJsonOptions(options =>
            {
                options.SerializerSettings.ReferenceLoopHandling = Newtonsoft.Json.ReferenceLoopHandling.Ignore;
            });

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


            app.UseDefaultFiles();
            app.UseStaticFiles();

            app.UseForwardedHeaders(new ForwardedHeadersOptions
            {
                // Read and use headers coming from reverse proxy: X-Forwarded-For X-Forwarded-Proto
                // This is particularly important so that HttpContet.Request.Scheme will be correct behind a SSL terminating proxy
                ForwardedHeaders = ForwardedHeaders.XForwardedFor |
                                   ForwardedHeaders.XForwardedProto
            });

            app.UseAuthentication();
            app.UseMvc();

            // Enable middleware to serve generated Swagger as a JSON endpoint.
            app.UseSwagger();

            // Enable middleware to serve swagger-ui (HTML, JS, CSS, etc.), 
            // specifying the Swagger JSON endpoint.
            app.UseSwaggerUI(opt => { opt.SwaggerEndpoint("/swagger/v1/swagger.json", "My API V1"); });
        }
    }
}