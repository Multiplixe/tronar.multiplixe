using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Http;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using multiplixe.empresas.client;
using multiplixe.twitter.grpc.Services;
using multiplixe.twitter.oauth;
using multiplixe.twitter.webhook;
using multiplixe.usuarios.client;

namespace multiplixe.twitter.grpc
{
    public class Startup
    {
        // This method gets called by the runtime. Use this method to add services to the container.
        // For more information on how to configure your application, visit https://go.microsoft.com/fwlink/?LinkID=398940
        public void ConfigureServices(IServiceCollection services)
        {
            services.AddGrpc();

            services.AddTransient<EmpresaClient>();
            services.AddTransient<CRCService>();
            services.AddTransient<OAuthServico>();
            services.AddTransient<IntegracaoUsuario>();
            services.AddTransient<PerfilClient>();
            services.AddTransient<EmpresaClient>();

            services.AddHttpClient<TwitterHttpClient>();
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
                endpoints.MapGrpcService<WebhookService>();
                endpoints.MapGrpcService<OAuthService>();

                endpoints.MapGet("/", async context =>
                {
                    await context.Response.WriteAsync("Communication with gRPC endpoints must be made through a gRPC client. To learn how to create a client, visit: https://go.microsoft.com/fwlink/?linkid=2086909");
                });
            });
        }
    }
}
