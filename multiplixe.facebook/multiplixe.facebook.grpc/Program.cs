using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Hosting;
using Microsoft.Extensions.Hosting;
using multiplixe.comum.enums;
using multiplixe.comum.helper;

namespace multiplixe.facebook.grpc
{
    public class Program
    {
        public static void Main(string[] args)
        {
            Console.ForegroundColor = ConsoleColor.White;
            Console.BackgroundColor = ConsoleColor.DarkRed;

            Console.WriteLine("*************************************************");
            Console.WriteLine($"Facebook GRPC - {DateTimeHelper.Now()}");
            Console.WriteLine("*************************************************");
            Console.WriteLine("");
            Console.ResetColor();

            CreateHostBuilder(args).Build().Run();
        }

        // Additional configuration is required to successfully run gRPC on macOS.
        // For instructions on how to configure Kestrel and gRPC clients on macOS, visit https://go.microsoft.com/fwlink/?linkid=2099682
        public static IHostBuilder CreateHostBuilder(string[] args) =>
            Host.CreateDefaultBuilder(args)
                .ConfigureWebHostDefaults(webBuilder =>
                {
                    webBuilder.UseStartup<Startup>()
                    .UseUrls($"https://*:{(int)PortasServicosEnum.facebookGRPC}");
                });
    }
}
