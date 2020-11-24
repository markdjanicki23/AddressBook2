using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using AddressBook2.Utilities;
using Microsoft.AspNetCore.Hosting;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.Hosting;
using Microsoft.Extensions.Logging;

namespace AddressBook2
{
    public class Program
    {
        public static async Task Main(string[] args)
        {
            var host = CreateHostBuilder(args).Build();
            await DataHelper.ManageData(host);
            host.Run();
        }

        public static IHostBuilder CreateHostBuilder(string[] args) =>
            Host.CreateDefaultBuilder(args)
                .ConfigureWebHostDefaults(webBuilder =>
                {
                    webBuilder.CaptureStartupErrors(true);
                    webBuilder.UseSetting(WebHostDefaults.DetailedErrorsKey, "true");
                    webBuilder.UseStartup<Startup>();
                });
    }
}
