using multiplixe.registrador_de_eventos.grpc.Protos;
using System;

namespace multiplixe.registrador_de_eventos.client.youtube.parsers
{
    public class RegistrarLiveHashtag
    {
        public LiveHashtagEventoMessage Request(Guid id, Guid usuarioId, string postId, string perfilId, DateTime dataEvento, string hashtag)
        {
            return new LiveHashtagEventoMessage
            {
                Evento = new EventoMessage
                {
                    Id = id.ToString(),
                    UsuarioId = usuarioId.ToString(),
                    PostId = postId,
                    PerfilId = perfilId,
                    DataEvento = dataEvento.Ticks,
                    Json = string.Empty
                },
                Hashtag = hashtag
            };
        }
    }
}
