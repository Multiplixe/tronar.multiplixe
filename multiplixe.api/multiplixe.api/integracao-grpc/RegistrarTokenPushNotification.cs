using adduo.helper.envelopes;
using multiplixe.comum.dto;
using multiplixe.usuarios.client;
using adduohelper = adduo.helper.envelopes;
using comum_dto =  multiplixe.comum.dto;

namespace multiplixe.api.integracao_grpc
{
    public class RegistrarTokenPushNotification : IIntegracaoGRPC<comum_dto.Token>
    {
        private adduohelper.RequestEnvelope<comum_dto.Token> request { get; }

        public RegistrarTokenPushNotification(adduohelper.RequestEnvelope<comum_dto.Token> request)
        {
            this.request = request;
        }
        public ResponseEnvelope<Token> Enviar()
        {
            var client = new TokenClient();

            return client.RegistrarPushNotification(request.Item.UsuarioId, request.Item.Valor);
        }
    }
}
