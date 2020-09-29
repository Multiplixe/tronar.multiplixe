using adduo.helper.envelopes;
using multiplixe.comum.exceptions;
using multiplixe.registrador_de_eventos.client.youtube.parsers;
using multiplixe.registrador_de_eventos.grpc.Protos;
using System;
using System.Net;

namespace multiplixe.registrador_de_eventos.client.youtube
{
    public class Client : BaseClient
    {
        private Youtube.YoutubeClient client { get; set; }

        public Client()
        {
            client = new Youtube.YoutubeClient(channel);
        }

        public ResponseEnvelope RegistrarLiveHashtag(Guid Id, Guid UsuarioId, string PostId, string PerfilId, DateTime DataEvento, string hashtag)
        {
            var parser = new RegistrarLiveHashtag();

            var request = parser.Request(Id, UsuarioId, PostId, PerfilId, DataEvento, hashtag);

            var response = client.RegistrarLiveHashtag(request);

            var envelope = new ResponseEnvelope();

            if(!response.Ok)
            {
                throw new GRPCException(HttpStatusCode.InternalServerError, response.Error);
            }

            return envelope;
        }


        public bool VerificarExistenciaLiveHashtag(Guid usuarioId, string postId)
        {
            var parser = new VerificarLiveHashtag();

            var request = parser.Request(usuarioId, postId);

            var response = client.VerificarExistenciaLiveHashtag(request);

            if (!response.Ok)
            {
                throw new GRPCException(HttpStatusCode.InternalServerError, response.Error);
            }

            return response.Existe;
        }
    }
}
