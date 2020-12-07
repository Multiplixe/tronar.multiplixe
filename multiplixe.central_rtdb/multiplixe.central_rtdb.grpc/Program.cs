using Microsoft.AspNetCore.Hosting;
using Microsoft.Extensions.Hosting;
using multiplixe.comum.enums;
using multiplixe.comum.helper;
using System;

namespace multiplixe.central_rtdb.grpc
{
    public class Program
    {
        public static void Main(string[] args)
        {
            var foregroundColor = Console.ForegroundColor;
            var backgroundColor = Console.BackgroundColor;

            Console.ForegroundColor = ConsoleColor.Black;
            Console.BackgroundColor = ConsoleColor.DarkYellow;
            Console.WriteLine("------------------------------------------");
            Console.WriteLine($"* Central RTDB - {DateTimeHelper.Now()}      ");
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
                    .UseUrls($"https://*:{(int)PortasServicosEnum.centralRTDB}");

                });
    }
}
