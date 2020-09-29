using multiplixe.comum.enums;
using multiplixe.usuarios.grpc.protos;
using System;
using adduohelper = adduo.helper;
using dto = multiplixe.comum.dto;

namespace multiplixe.usuarios.client
{
    public class TokenClient : BaseClient
    {
        private Token.TokenClient tokenClient { get; set; }
        private parsers.TokenRegistrar registrarParser { get; }
        private parsers.TokenObter obterParser { get; }

        public TokenClient()
        {
            tokenClient = new Token.TokenClient(channel);
            registrarParser = new parsers.TokenRegistrar();
            obterParser = new parsers.TokenObter();
        }

        public adduohelper.envelopes.ResponseEnvelope<dto.Token> RegistrarPushNotification(Guid usuarioId, string valor)
        {
            return Registrar(usuarioId, valor, TipoTokenEnum.PushNotification);
        }
        private adduohelper.envelopes.ResponseEnvelope<dto.Token> Registrar(Guid usuarioId, string valor, TipoTokenEnum tipo)
        {
            var request = registrarParser.Request(usuarioId, valor, tipo);

            var response = tokenClient.Registrar(request);

            var responseEnvelope = registrarParser.Response(response);

            return responseEnvelope;
        }

        public adduohelper.envelopes.ResponseEnvelope<dto.Token> ObterPushNotification(Guid usuarioId)
        {
            return Obter(usuarioId, TipoTokenEnum.PushNotification);
        }

        private adduohelper.envelopes.ResponseEnvelope<dto.Token> Obter(Guid usuarioId, TipoTokenEnum tipo)
        {
            var request = obterParser.Request(usuarioId, tipo);

            var response = tokenClient.Obter(request);

            var responseEnvelope = obterParser.Response(response);

            return responseEnvelope;
        }
    }
}
