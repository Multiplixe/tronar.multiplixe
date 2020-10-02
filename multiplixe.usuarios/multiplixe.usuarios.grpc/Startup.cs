using FirebaseAdmin;
using Google.Apis.Auth.OAuth2;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Http;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using multiplixe.central_rtdb.client;
using multiplixe.classificador.client;
using multiplixe.comum.dapper;
using multiplixe.comum.helper;
using multiplixe.usuarios.grpc.services;
using System.IO;

namespace multiplixe.usuarios.grpc
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
            services.AddTransient<perfil.Servico>();
            services.AddTransient<perfil.Repositorio>();

            services.AddTransient<usuario.registro.Firebase>();
            services.AddTransient<usuario.registro.FirebaseParserError>();
            services.AddTransient<usuario.registro.Repositorio>();
            services.AddTransient<usuario.registro.Validador>();
            services.AddTransient<usuario.registro.Servico>();

            services.AddTransient<usuario.atualizacao.Firebase>();
            services.AddTransient<usuario.atualizacao.FirebaseParserError>();
            services.AddTransient<usuario.atualizacao.Repositorio>();
            services.AddTransient<usuario.atualizacao.Validador>();
            services.AddTransient<usuario.atualizacao.Servico>();

            services.AddTransient<usuario.exclusao.Servico>();
            services.AddTransient<usuario.exclusao.Firebase>();
            services.AddTransient<usuario.exclusao.Repositorio>();

            services.AddTransient<usuario.consulta.Servico>();
            services.AddTransient<usuario.consulta.Repositorio>();

            services.AddTransient<usuario.inicio.Firebase>();
            services.AddTransient<usuario.inicio.Servico>();

            services.AddTransient<externo.autenticacao.Firebase>();
            services.AddTransient<externo.autenticacao.Servico>();

            services.AddTransient<parsers.UsuarioRegistrar>();
            services.AddTransient<parsers.UsuarioAtualizar>();
            services.AddTransient<parsers.UsuarioAutenticar>();
            services.AddTransient<parsers.UsuarioObter>();
            services.AddTransient<parsers.UsuarioListar>();
            services.AddTransient<parsers.PerfilRegistrar>();
            services.AddTransient<parsers.PerfilObterPerfisConectados>();
            services.AddTransient<parsers.TokenRegistrar>();
            services.AddTransient<parsers.TokenObter>();
            services.AddTransient<parsers.PerfilObter>();
            services.AddTransient<ClassificadorUsuarioClient>();
            services.AddTransient<DapperHelper>();
            services.AddTransient<token.Servico>();
            services.AddTransient<token.Repositorio>();
            services.AddTransient<RTDBUsuarioClient>();
            services.AddTransient<RTDBAtividadeClient>();

            var googleCredentialsPath = Path.Combine(Directory.GetCurrentDirectory(), "google-credential.json");

            FirebaseApp.Create(new AppOptions
            {
                Credential = GoogleCredential.FromFile(googleCredentialsPath)
            });

            var parametros = Configuration.GetSection("Parametros").Get<externo.autenticacao.Parametros>();

            services.AddSingleton(parametros);

            services.AddGrpc(options =>
            {
                options.EnableDetailedErrors = true;
            });
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
                endpoints.MapGrpcService<PerfilService>();
                endpoints.MapGrpcService<UsuarioService>();
                endpoints.MapGrpcService<UsuarioExternoService>();
                endpoints.MapGrpcService<tokenervice>();

                endpoints.MapGet("/", async context =>
                {
                    await context.Response.WriteAsync("Communication with gRPC endpoints must be made through a gRPC client. To learn how to create a client, visit: https://go.microsoft.com/fwlink/?linkid=2086909");
                });
            });
        }
    }
}
