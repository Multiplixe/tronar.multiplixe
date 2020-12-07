using adduo.helper.envelopes;
using adduo.helper.extensionmethods;
using multiplixe.comum.enums;
using multiplixe.usuarios.perfil.grpc.Protos;
using System;
using System.Net;

namespace multiplixe.usuarios.client.parsers
{
    public class Desconectar
    {
        public DesconectarRequest Request(Guid usuarioId, string perfilId, RedeSocialEnum redesocial, bool ativo)
        {
            return new DesconectarRequest
            {
                UsuarioId = usuarioId.ToString(),
                PerfilId = perfilId.EmptyIfNull(),
                RedeSocial = (int)redesocial,
                Ativo = ativo
            };
        }

        public ResponseEnvelope Response(DesconectarResponse response)
        {
            var envelope = new ResponseEnvelope();

            envelope.HttpStatusCode = (HttpStatusCode)response.HttpStatusCode;

            if (!envelope.Success)
            {
                envelope.Error = new ErrorEnvelope()
                {
                    Exception = new Exception(response.Erro),
                    Messages = new System.Collections.Generic.List<string> { response.Erro }
                };
            }

            return envelope;
        }
    }
}
