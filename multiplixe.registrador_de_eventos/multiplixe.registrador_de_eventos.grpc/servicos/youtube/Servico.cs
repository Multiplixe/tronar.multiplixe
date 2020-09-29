using adduo.helper.extensionmethods;
using Grpc.Core;
using multiplixe.registrador_de_eventos.grpc.Protos;
using System;
using System.Threading.Tasks;

namespace multiplixe.registrador_de_eventos.grpc.servicos.youtube
{
    public class Servico : Youtube.YoutubeBase
    {
        private Repositorio repositorio { get; }

        public Servico(Repositorio repositorio)
        {
            this.repositorio = repositorio;
        }


        public override Task<ResponseMessage> RegistrarLiveHashtag(LiveHashtagEventoMessage message, ServerCallContext context)
        {
            var response = new ResponseMessage();

            try
            {
                repositorio.RegistrarLiveHashtag(
                    message.Evento.Id.ToGuid(),
                    message.Evento.UsuarioId.ToGuid(),
                    message.Evento.PostId,
                    message.Evento.PerfilId,
                    new System.DateTime(message.Evento.DataEvento),
                    message.Hashtag);

                response.Ok = true;
            }
            catch (Exception ex)
            {
                response.Error = ex.Message;
                // ## TODO Log
            }


            return Task.FromResult(response);
        }


        public override Task<ResponseMessage> VerificarExistenciaLiveHashtag(VerificarLiveHashtagMessage message, ServerCallContext context)
        {
            var response = new ResponseMessage();

            try
            {
                var existe = repositorio.VerificarExistenciaLiveHashtag(message.UsuarioId.ToGuid(), message.PostId);

                response.Ok = true;
                response.Existe = existe;
            }
            catch (Exception ex)
            {
                response.Error = ex.Message;
                // ## TODO Log
            }

            return Task.FromResult(response);
        }
    }
}
