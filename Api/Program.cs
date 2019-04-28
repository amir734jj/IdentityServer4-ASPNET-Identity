using System.IO;
using Lamar.Microsoft.DependencyInjection;
using Microsoft.AspNetCore.Hosting;

namespace Api
{
    public class Program
    {
        public static void Main(string[] args)
        {
            // This can be reviwed on Azure's application insights
            System.Diagnostics.Trace.TraceInformation("Application server starting");

            // Start Webserver
            var host = new WebHostBuilder()
                .UseLamar()
                .UseKestrel()
                .UseContentRoot(Directory.GetCurrentDirectory())
                .UseIISIntegration()
                .UseStartup<Startup>()
                .Build();

            host.Run();
        }
    }
}