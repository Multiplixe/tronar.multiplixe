using multiplixe.comum.enums;
using multiplixe.registrador_de_eventos.client.twitch.parsers;
using multiplixe.registrador_de_eventos.grpc.Protos;
using System;

namespace multiplixe.registrador_de_eventos.client.twitch
{
    public class Client : BaseClient
    {
        private Twitch.TwitchClient client { get; set; }

        public Client()
        {
            client = new Twitch.TwitchClient(channel);
        }

        public void RegistrarPing(Guid id, Guid usuarioId, string perfilId, string postId, DateTime dataEvento, DateTime ultimoEvento, DateTime atualEvento, int toleranciaSegundos, int frequenciaMinutos, int pausaMilissegundos, TipoEventoEnum tipo)
        {
            var parser = new Ping();

            var request = parser.Request(id, usuarioId, perfilId, postId, dataEvento, ultimoEvento, atualEvento, toleranciaSegundos, frequenciaMinutos, pausaMilissegundos, tipo);

            client.RegistrarPingAsync(request)
              .GetAwaiter();
        }
    }
}
