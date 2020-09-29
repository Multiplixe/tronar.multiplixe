using adduo.helper.extensionmethods;
using Grpc.Core;
using multiplixe.registrador_de_eventos.grpc.Protos;
using System;
using System.Threading.Tasks;

namespace multiplixe.registrador_de_eventos.grpc.servicos.twitch
{
    public class Servico : Protos.Twitch.TwitchBase
    {
        private Repositorio repositorio { get; }

        public Servico(Repositorio repositorio)
        {
            this.repositorio = repositorio;
        }

        public override Task<ResponseMessage> RegistrarPing(PingEventoMessage ping, ServerCallContext context)
        {
            repositorio.RegistrarPing(
                ping.Evento.Id.ToGuid(),
                ping.Evento.UsuarioId.ToGuid(),
                ping.Evento.PostId,
                ping.Evento.PerfilId,
                new DateTime(ping.Evento.DataEvento),
                ping.ToleranciaSegundos,
                ping.FrequenciaMinutos,
                ping.PausaMilissegundos,
                new DateTime(ping.Atual),
                new DateTime(ping.Ultimo));

            return Task.FromResult(new ResponseMessage
            {
                Ok = true
            });
        }
    }
}
