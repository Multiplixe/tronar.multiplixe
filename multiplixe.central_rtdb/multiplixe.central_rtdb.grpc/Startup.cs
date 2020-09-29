using Firebase.Database;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Http;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using multiplixe.central_rtdb.grpc.services;
using multiplixe.empresas.client;
using System;
using System.Threading.Tasks;

namespace multiplixe.central_rtdb.grpc
{
    public class Startup
    {
        public IConfiguration Configuration { get; }

        public Startup(IConfiguration configuration)
        {
            Configuration = configuration;
        }


        // This method gets called by the runtime. Use this method to add services to the container.
        // For more information on how to configure your application, visit https://go.microsoft.com/fwlink/?LinkID=398940
        public void ConfigureServices(IServiceCollection services)
        {
            services.AddTransient<core.usuario.Atividade>();
            services.AddTransient<core.usuario.Iniciador>();
            services.AddTransient<core.comum.Atividade>();

            // ##TODO reestruturar para Saas, cada cliente com sua configuração
            var empresaClient = new EmpresaClient();
            var firebaseInfo = empresaClient.ObterInfoFirebase(Guid.Empty);

            services.AddTransient<FirebaseClient>(o =>
            {
                return new FirebaseClient($"https://{firebaseInfo.Item.ProjectID}.firebaseio.com",
                new FirebaseOptions
                {
                    AuthTokenAsyncFactory = () => Task.FromResult(firebaseInfo.Item.SecretAppID)
                });
            });


            services.AddGrpc();
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
                endpoints.MapGrpcService<RTDBService>();

                endpoints.MapGet("/", async context =>
                {
                    await context.Response.WriteAsync("Communication with gRPC endpoints must be made through a gRPC client. To learn how to create a client, visit: https://go.microsoft.com/fwlink/?linkid=2086909");
                });
            });
        }
    }
}
