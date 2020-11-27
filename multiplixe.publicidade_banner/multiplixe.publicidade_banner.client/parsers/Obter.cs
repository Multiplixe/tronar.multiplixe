using adduo.helper.envelopes;
using multiplixe.publicidade_banner.grpc.Protos;
using System;
using System.Collections.Generic;
using System.Net;
using System.Text;
using dto = multiplixe.comum.dto;

namespace multiplixe.publicidade_banner.client.parsers
{
    public class Obter
    {
        public ObterRequest Request(Guid usuarioId)
        {
            return new ObterRequest { UsuarioId = usuarioId.ToString() };
        }

        public ResponseEnvelope<List<dto.Banner>> Response(ObterResponse response)
        {
            var envelope = new ResponseEnvelope<List<dto.Banner>>();

            envelope.HttpStatusCode = (HttpStatusCode)response.HttpStatusCode;

            if (envelope.Success)
            {
                foreach (var item in response.Banners)
                {
                    envelope.Item.Add(new dto.Banner(item.Id, item.Imagem, item.Thumb, item.URL));
                }
            }
            else
            {
                envelope.Error = new ErrorEnvelope
                {
                    Messages = new List<string> { response.Erro }
                };
            }

            return envelope;
        }
    }
}
