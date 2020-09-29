using adduo.helper.extensionmethods;
using Google.Protobuf.WellKnownTypes;
using Grpc.Core;
using multiplixe.registrador_de_eventos.grpc.parsers;
using multiplixe.registrador_de_eventos.grpc.Protos;
using System;
using System.Threading.Tasks;

namespace multiplixe.registrador_de_eventos.grpc.servicos.twitter
{
    public class Servico : Twitter.TwitterBase
    {
        private Repositorio repositorio { get; }

        public Servico(Repositorio repositorio)
        {
            this.repositorio = repositorio;
        }

        public override Task<ResponseMessage> RegistrarReacao(ReacaoEventoMessage reacao, ServerCallContext context)
        {
            repositorio.RegistrarReacao(
                reacao.Evento.Id.ToGuid(),
                reacao.Evento.UsuarioId.ToGuid(),
                reacao.Evento.PostId,
                reacao.Evento.PerfilId,
                new DateTime(reacao.Evento.DataEvento),
                (int)reacao.Tipo);

            return Task.FromResult(new ResponseMessage
            {
                Ok = true
            });
        }


        public override Task<ResponseMessage> UltimaReacao(UltimaReacaoMessage ultimaReacaoMessage, ServerCallContext context)
        {
            try
            {
                var usuarioId = Guid.Parse(ultimaReacaoMessage.UsuarioId);

                var reacaoResult = repositorio.ObterUltimaReacao(usuarioId, ultimaReacaoMessage.PostId);

                var responseMessage = new ResponseMessage();

                if (reacaoResult != null)
                {
                    var reacaoEventoMessage = ReacaoEventoParser.Parse(reacaoResult);
                    responseMessage.Item = Any.Pack(reacaoEventoMessage);
                    responseMessage.Ok = true;
                }

                return Task.FromResult(responseMessage);
            }
            catch (Exception ex)
            {
                //##TODO log
                throw ex;
            }
        }
    }
}
