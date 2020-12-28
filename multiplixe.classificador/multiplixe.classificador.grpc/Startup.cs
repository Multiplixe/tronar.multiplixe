using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Http;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using multiplixe.central_rtdb.client;
using multiplixe.classificador.grpc.Services;
using multiplixe.classificador.parceiro.results;
using multiplixe.comum.dapper;
using multiplixe.empresas.client;
using multiplixe.enfileirador.client;

namespace multiplixe.classificador.grpc
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

            var parametrosRanking = Configuration.GetSection("Ranking").Get<ranking.Parametros>();

            services
            .AddSingleton<IConfiguration>(Configuration)
            .AddTransient<nivel.Servico>()
            .AddTransient<nivel.Repositorio>()
            .AddTransient<nivel.Regras>()
            .AddTransient<nivel.FronteiroServico>()
            .AddSingleton<ranking.Parametros>(parametrosRanking)
            .AddTransient<ranking.Repositorio>()
            .AddTransient<ranking.Servico>()
            .AddTransient<pontuacao.Repositorio>()
            .AddTransient<pontuacao.Servico>()
            .AddTransient<classificacao.Servico>()
            .AddTransient<classificacao.Repositorio>()
            .AddTransient<usuario.Servico>()
            .AddTransient<usuario.Repositorio>()
            .AddTransient<transacao.Saldo>()
            .AddTransient<transacao.Debito>()
            .AddTransient<transacao.Estorno>()
            .AddTransient<transacao.Repositorio>()
            .AddTransient(typeof(interfaces.IConsultarUsuario), typeof(usuario.Servico))
            .AddTransient(typeof(interfaces.IConsultarParceiro), typeof(parceiro.Servico))
            .AddTransient<parceiro.Servico>()
            .AddTransient<parceiro.Repositorio>()
            .AddTransient<EmpresaClient>()
            .AddTransient<EnfileiradorClient>()
            .AddTransient<DapperHelper>()
            .AddTransient<parsers.UsuarioSincronizar>()
            .AddTransient<parsers.UsuarioDeletar>()
            .AddTransient<parsers.UsuarioRegistrar>()
            .AddTransient<parsers.ObterClassificacao>()
            .AddTransient<parsers.TransacaoDebitar>()
            .AddTransient<parsers.TransacaoEstornar>()
            .AddTransient<RTDBAtividadeComumClient>();

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
                endpoints.MapGrpcService<ClassificadorService>();
                endpoints.MapGrpcService<RankingService>();
                endpoints.MapGrpcService<UsuariosService>();
                endpoints.MapGrpcService<TransacaoService>();

                endpoints.MapGet("/", async context =>
                {
                    await context.Response.WriteAsync("Communication with gRPC endpoints must be made through a gRPC client. To learn how to create a client, visit: https://go.microsoft.com/fwlink/?linkid=2086909");
                });
            });
        }
    }
}
