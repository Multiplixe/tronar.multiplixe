using adduo.helper.envelopes;
using multiplixe.comum.dto;
using multiplixe.comum.enums;
using multiplixe.registrador_de_eventos.grpc.Protos;
using System;

namespace multiplixe.registrador_de_eventos.client.twitter.parsers
{
    public class UltimaReacao
    {
        public UltimaReacaoMessage Request(Guid usuarioId, string postId)
        {
            var request = new UltimaReacaoMessage
            {
                UsuarioId = usuarioId.ToString(),
                PostId = postId
            };

            return request;
        }

        public ResponseEnvelope<Reacao> Response(ResponseMessage response)
        {
            var evento = new Reacao
            {
                Tipo = TipoEventoEnum.none
            };

            if (response.Ok)
            {
                var reacaoMessage = response.Item.Unpack<ReacaoEventoMessage>();
                evento.EventoId = Guid.Parse(reacaoMessage.Evento.Id);
                evento.UsuarioId = Guid.Parse(reacaoMessage.Evento.UsuarioId);
                evento.PostId = reacaoMessage.Evento.PostId;
                evento.Tipo = (TipoEventoEnum)(int)reacaoMessage.Tipo;
            }

            var envelope = new ResponseEnvelope<Reacao>(evento);

            return envelope;
        }
    }
}
