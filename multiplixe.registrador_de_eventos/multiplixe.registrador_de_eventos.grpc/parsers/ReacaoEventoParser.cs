using multiplixe.registrador_de_eventos.grpc.Protos;
using multiplixe.registrador_de_eventos.grpc.results;

namespace multiplixe.registrador_de_eventos.grpc.parsers
{
    public class ReacaoEventoParser
    {
        public static ReacaoEventoMessage Parse(ReacaoResult result)
        {
            var message = new ReacaoEventoMessage
            {
                Evento = new EventoMessage
                {
                    Id = result.Id.ToString(),
                    PostId = result.PostId,
                    UsuarioId = result.UsuarioId.ToString(),
                },
                Intensidade = result.Intensidade ?? string.Empty,
                Tipo = (TipoEventoEnumMessage)result.Tipo
            };

            return message;
        }

    }
}
