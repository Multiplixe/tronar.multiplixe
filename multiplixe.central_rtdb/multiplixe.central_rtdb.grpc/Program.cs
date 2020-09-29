using Microsoft.AspNetCore.Hosting;
using Microsoft.Extensions.Hosting;
using multiplixe.comum.enums;

namespace multiplixe.central_rtdb.grpc
{
    public class Program
    {
        public static void Main(string[] args)
        {
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
