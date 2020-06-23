using Microsoft.AspNetCore.Hosting;
using Microsoft.Extensions.Hosting;
using Reinforced.Typings.Attributes;

[assembly: TsGlobal(
        UseModules = true,
        AutoOptionalProperties = true,
        DiscardNamespacesWhenUsingModules = true,
        CamelCaseForProperties = true,
        CamelCaseForMethods = true
    )]
namespace incognote.web
{
    public class Program
    {
        public static void Main(string[] args)
        {
            CreateHostBuilder(args).Build().Run();
        }

        public static IHostBuilder CreateHostBuilder(string[] args) =>
            Host.CreateDefaultBuilder(args)
                .ConfigureWebHostDefaults(webBuilder =>
                {
                    webBuilder.UseStartup<Startup>();
                });
    }
}
