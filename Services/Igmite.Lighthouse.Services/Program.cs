using Microsoft.AspNetCore;
using Microsoft.AspNetCore.Hosting;
using Microsoft.Extensions.Configuration;
using System;
using System.IO;

namespace Igmite.Lighthouse.Services
{
    public class Program
    {
        public static void Main(string[] args)
        {
            CreateWebHostBuilder(args).Build().Run();
        }

        public static IWebHostBuilder CreateWebHostBuilder(string[] args)
        {
            try
            {
                var config = new ConfigurationBuilder()
                    .SetBasePath(Directory.GetCurrentDirectory())
                    .AddJsonFile("appsettings.json", optional: true)
                    .AddCommandLine(args)
                    .Build();

                string[] urls = new string[2];
                urls[0] = string.Format("http://*:{0}/", config["Settings:ServiceIPPort"]);
                urls[1] = string.Format("https://*:{0}/", config["Settings:ServiceIPPort"]);

                return WebHost.CreateDefaultBuilder(args)
                   .UseContentRoot(Directory.GetCurrentDirectory())
                   .UseUrls(urls)
                   .UseKestrel(options =>
                   {
                       options.Limits.MaxRequestBodySize = 52428800; //50MB
                       options.ListenAnyIP(int.Parse(config["Settings:ServiceIPPort"]));
                   })
                   .UseStartup<Startup>();
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.Message);
                Logging.ErrorManager.Instance.WriteErrorLogsInFile(ex);

                throw ex;
            }
        }
    }
}