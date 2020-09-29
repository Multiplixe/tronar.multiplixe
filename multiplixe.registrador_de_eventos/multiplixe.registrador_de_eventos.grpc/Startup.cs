using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Http;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using multiplixe.comum.dapper;

namespace multiplixe.registrador_de_eventos.grpc
{
    public class Startup
    {
        // This method gets called by the runtime. Use this method to add services to the container.
        // For more information on how to configure your application, visit https://go.microsoft.com/fwlink/?LinkID=398940
        public void ConfigureServices(IServiceCollection services)
        {
            services.AddGrpc();

            services.AddTransient<DapperHelper>();
            services.AddTransient<servicos.facebook.Repositorio>();
            services.AddTransient<servicos.twitter.Repositorio>();
            services.AddTransient<servicos.facebook.Repositorio>();
            services.AddTransient<servicos.youtube.Repositorio>();
        }

        // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
        public void Configure(IApplicationBuilder app, IWebHostEnvironment env)
        {
            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
            }

            app.UseRouting();

            app.UseEndpoints(endpoints =>
            {
                endpoints.MapGrpcService<servicos.facebook.Servico>();
                endpoints.MapGrpcService<servicos.twitter.Servico>();
                endpoints.MapGrpcService<servicos.twitch.Servico>();
                endpoints.MapGrpcService<servicos.youtube.Servico>();

                endpoints.MapGet("/", async context =>
                {
                    await context.Response.WriteAsync("Communication with gRPC endpoints must be made through a gRPC client. To learn how to create a client, visit: https://go.microsoft.com/fwlink/?linkid=2086909");
                });
            });
        }
    }
}
