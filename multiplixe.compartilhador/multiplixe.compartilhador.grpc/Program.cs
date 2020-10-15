using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Hosting;
using Microsoft.Extensions.Hosting;
using multiplixe.comum.enums;
using multiplixe.comum.helper;

namespace multiplixe.compartilhador.grpc
{
    public class Program
    {
        public static void Main(string[] args)
        {
            var foregroundColor = Console.ForegroundColor;
            var backgroundColor = Console.BackgroundColor;
            
            Console.ForegroundColor = ConsoleColor.Yellow;
            Console.BackgroundColor = ConsoleColor.Blue;
            Console.WriteLine("------------------------------------------");
            Console.WriteLine($"* Compartilhador - {DateTimeHelper.Now()}      ");
            Console.WriteLine("------------------------------------------");
            Console.WriteLine("");
            
            Console.ForegroundColor = foregroundColor;
            Console.BackgroundColor = backgroundColor;

            CreateHostBuilder(args).Build().Run();
        }

        // Additional configuration is required to successfully run gRPC on macOS.
        // For instructions on how to configure Kestrel and gRPC clients on macOS, visit https://go.microsoft.com/fwlink/?linkid=2099682
        public static IHostBuilder CreateHostBuilder(string[] args) =>
            Host.CreateDefaultBuilder(args)
                .ConfigureWebHostDefaults(webBuilder =>
                {
                    webBuilder.UseStartup<Startup>()
                                .UseUrls($"https://*:{(int)PortasServicosEnum.compartilhador}");

                });
    }
}
