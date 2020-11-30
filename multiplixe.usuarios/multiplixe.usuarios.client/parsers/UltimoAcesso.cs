using adduo.helper.envelopes;
using multiplixe.comum.dto;
using multiplixe.usuarios.grpc.protos;
using System;
using System.Net;

namespace multiplixe.usuarios.client.parsers
{
    public class UltimoAcesso
    {
        public UltimoAcessoRequest Request(UsuarioUltimoAcesso request)
        {
            return new UltimoAcessoRequest
            {
                UsuarioId = request.UsuarioId.ToString(),
                Acesso = request.Acesso.Ticks
            };
        }

        public ResponseEnvelope<UsuarioUltimoAcesso> Response(UltimoAcessoResponse response)
        {
            var envelope = new ResponseEnvelope<UsuarioUltimoAcesso>();

            envelope.HttpStatusCode = (HttpStatusCode)response.HttpStatusCode;

            if(!envelope.Success)
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
