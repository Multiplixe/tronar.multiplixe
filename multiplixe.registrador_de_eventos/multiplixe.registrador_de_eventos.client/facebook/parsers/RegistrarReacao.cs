using multiplixe.comum.enums;
using multiplixe.comum.helper;
using multiplixe.registrador_de_eventos.grpc.Protos;
using System;

namespace multiplixe.registrador_de_eventos.client.facebook.parsers
{
    public class RegistrarReacao
    {
        public  ReacaoEventoMessage Request(Guid id, Guid usuarioId, string perfilId, string postId, DateTime dataEvento, object evento, string intensidade, TipoEventoEnum tipo)
        {
            var json = SerializadorHelper.Serializar(evento);

            var reacaoEventoMessage = new ReacaoEventoMessage
            {
                Evento = new EventoMessage()
                {
                    Id = id.ToString(),
                    UsuarioId = usuarioId.ToString(),
                    PerfilId = perfilId,
                    PostId = postId,
                    DataEvento = dataEvento.Ticks,
                    Json = json
                },
                Intensidade = intensidade,
                Tipo = (TipoEventoEnumMessage)(int)tipo
            };

            return reacaoEventoMessage;
        }
    }
}
