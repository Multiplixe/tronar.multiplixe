using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Http;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using multiplixe.empresas.client;
using multiplixe.twitch.grpc.Services;
using multiplixe.twitch.oauth;
using multiplixe.twitch.oauth.dtos;
using multiplixe.twitch.ping.dtos;
using multiplixe.usuarios.client;

namespace multiplixe.twitch.grpc
{
    public class Startup
    {

        public IConfiguration Configuration { get; }

        public Startup(IConfiguration configuration)
        {
            Configuration = configuration;
        }


        public void ConfigureServices(IServiceCollection services)
        {
            services.AddGrpc();

            services.AddHttpClient<TwitchHttpClient>();

            services.AddTransient<EmpresaClient>();
            services.AddTransient<oauth.OAuthServico>();
            services.AddTransient<IntegracaoUsuario>();
            services.AddTransient<PerfilClient>();


            var twitchParams = Configuration.GetSection("OAuth").Get<TwitchParams>();
            var twitchPing = Configuration.GetSection("Ping").Get<PingConfig>();

            services.AddSingleton(twitchParams);
            services.AddSingleton(twitchPing);
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
                endpoints.MapGrpcService<OAuthService>();

                endpoints.MapGet("/", async context =>
                {
                    await context.Response.WriteAsync("Communication with gRPC endpoints must be made through a gRPC client. To learn how to create a client, visit: https://go.microsoft.com/fwlink/?linkid=2086909");
                });
            });
        }
    }
}
