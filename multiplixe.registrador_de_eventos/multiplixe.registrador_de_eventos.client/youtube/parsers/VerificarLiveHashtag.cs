using multiplixe.registrador_de_eventos.grpc.Protos;
using System;

namespace multiplixe.registrador_de_eventos.client.youtube.parsers
{
    public class VerificarLiveHashtag
    {
        public VerificarLiveHashtagMessage Request(Guid usuarioId, string postId)
        {
            return new VerificarLiveHashtagMessage
            {
                UsuarioId = usuarioId.ToString(),
                PostId = postId,
            };
        }
    }
}
