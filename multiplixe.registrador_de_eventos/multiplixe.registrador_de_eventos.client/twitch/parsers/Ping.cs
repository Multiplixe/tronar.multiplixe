using multiplixe.comum.enums;
using multiplixe.registrador_de_eventos.grpc.Protos;
using System;

namespace multiplixe.registrador_de_eventos.client.twitch.parsers
{
    public class Ping
    {

        public PingEventoMessage Request(Guid id, Guid usuarioId, string perfilId, string postId, DateTime dataEvento, DateTime ultimoEvento, DateTime atualEvento, int toleranciaSegundos, int frequenciaMinutos, int pausaMilissegundos, TipoEventoEnum tipo)
        {
            var pingEventoMessage = new PingEventoMessage
            {
                Evento = new EventoMessage()
                {
                    Id = id.ToString(),
                    UsuarioId = usuarioId.ToString(),
                    PerfilId = perfilId,
                    PostId = postId,
                    DataEvento = dataEvento.Ticks,
                    Json = string.Empty
                },
                Ultimo = ultimoEvento.Ticks,
                Atual = atualEvento.Ticks,
                ToleranciaSegundos = toleranciaSegundos,
                FrequenciaMinutos = frequenciaMinutos,
                PausaMilissegundos = pausaMilissegundos
            };

            return pingEventoMessage;
        }
    }
}
