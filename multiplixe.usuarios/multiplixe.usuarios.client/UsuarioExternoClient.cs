using multiplixe.usuarios.grpc.protos;
using adduohelper = adduo.helper.envelopes;
using dto = multiplixe.comum.dto;

namespace multiplixe.usuarios.client
{
    public class UsuarioExternoClient : BaseClient
    {
        private UsuarioExterno.UsuarioExternoClient grpcService { get; set; }
        private parsers.UsuarioExternoAutenticar autenticarParser { get; }

        public UsuarioExternoClient()
        {
            grpcService = new UsuarioExterno.UsuarioExternoClient(channel);
            autenticarParser = new parsers.UsuarioExternoAutenticar();
        }

        public adduohelper.ResponseEnvelope<dto.externo.AutenticacaoResponse> Autenticar(dto.externo.AutenticacaoRequest autenticacaoRequest)
        {
            var request = autenticarParser.Request(autenticacaoRequest);

            var autenticacaoResponse = grpcService.Autenticar(request);

            var response = autenticarParser.Response(autenticacaoResponse);

            return response;
        }
    }
}
