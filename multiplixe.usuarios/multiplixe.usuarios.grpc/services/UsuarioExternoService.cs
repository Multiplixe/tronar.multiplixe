using Grpc.Core;
using multiplixe.usuarios.grpc.protos;
using System.Threading.Tasks;

namespace multiplixe.usuarios.grpc.services
{
    public class UsuarioExternoService : UsuarioExterno.UsuarioExternoBase
    {
        private externo.autenticacao.Servico autenticacaoService { get; }

        private parsers.UsuarioAutenticar autenticarParser { get; }

        public UsuarioExternoService(
            externo.autenticacao.Servico autenticacaoService,
            parsers.UsuarioAutenticar autenticarParser)
        {
            this.autenticacaoService = autenticacaoService;
            this.autenticarParser = autenticarParser;
        }

        public override Task<AutenticarResponse> Autenticar(AutenticarRequest autenticarRequest, ServerCallContext context)
        {
            var request = autenticarParser.Request(autenticarRequest);

            var envelope = autenticacaoService.Autenticar(request);

            var response = autenticarParser.Response(envelope);

            return Task.FromResult(response);
        }
    }
}
