using multiplixe.classificador.grpc.Protos;
using System;
using coredto = multiplixe.comum.dto;
using coreexceptions = multiplixe.comum.exceptions;
using envelopes = adduo.helper.envelopes;

namespace multiplixe.classificador.client
{
    public class ClassificadorUsuarioClient : BaseClient
    {
        private Usuarios.UsuariosClient client { get; set; }

        public ClassificadorUsuarioClient()
        {
            client = new Usuarios.UsuariosClient(channel);
        }

        public void Registrar(envelopes.RequestEnvelope<coredto.Usuario> request)
        {
            var parser = new parsers.UsuarioRegistrar();

            var usuarioMessage = parser.Request(request);

            var usuarioResponse = client.Registrar(usuarioMessage);

            var envelope = parser.Response(usuarioResponse);

            if(!envelope.Success)
            {
                throw new coreexceptions.GRPCException(envelope.HttpStatusCode);
            }
        }

        public void Sincronizar(envelopes.RequestEnvelope<coredto.Usuario> request)
        {
            var parser = new parsers.UsuarioSincronizar();

            var usuarioMessage = parser.Request(request);

            var usuarioResponse = client.Sincronizar(usuarioMessage);

            var envelope = parser.Response(usuarioResponse);

            if (!envelope.Success)
            {
                throw new coreexceptions.GRPCException(envelope.HttpStatusCode);
            }
        }

        public void Deletar(Guid usuarioId)
        {
            var parser = new parsers.UsuarioDeletar();

            var request = parser.Request(usuarioId);

            var usuarioResponse = client.Deletar(request);

            var envelope = parser.Response(usuarioResponse);

            if (!envelope.Success)
            {
                throw new coreexceptions.GRPCException(envelope.HttpStatusCode);
            }
        }
    }
}
