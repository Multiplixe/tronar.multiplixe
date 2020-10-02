using adduo.helper.envelopes;
using multiplixe.usuarios.client;
using comum_dto = multiplixe.comum.dto;

namespace multiplixe.api.integracao_grpc
{
    public class AutenticacaoExterna : IIntegracaoGRPC<comum_dto.externo.AutenticacaoResponse>
    {
        private comum_dto.externo.AutenticacaoRequest request { get; }

        public AutenticacaoExterna(comum_dto.externo.AutenticacaoRequest request)
        {
            this.request = request;
        }

        public ResponseEnvelope<comum_dto.externo.AutenticacaoResponse> Enviar()
        {
            var grcp = new UsuarioExternoClient();
            return grcp.Autenticar(request);
        }
    }
}
