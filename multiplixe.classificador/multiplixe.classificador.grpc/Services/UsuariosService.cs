using multiplixe.classificador.grpc.Protos;
using Grpc;
using System;
using System.Net;
using System.Threading.Tasks;
using multiplixe.classificador.usuario;
using Grpc.Core;

namespace multiplixe.classificador.grpc.Services
{
    public class UsuariosService : Usuarios.UsuariosBase
    {
        private parsers.UsuarioRegistrar registrarParser { get; }
        private parsers.UsuarioSincronizar sincronizarParser { get; }
        private parsers.UsuarioDeletar deletarParser { get; }
        private Servico servico { get; }

        public UsuariosService(
            parsers.UsuarioRegistrar registrarParser,
            parsers.UsuarioSincronizar sincronizarParser,
            parsers.UsuarioDeletar deletarParser,
            Servico servico)
        {
            this.sincronizarParser = sincronizarParser;
            this.registrarParser = registrarParser;
            this.deletarParser = deletarParser;
            this.servico = servico;
        }

        public override Task<UsuarioResponse> Registrar(UsuarioRequest usuarioMessage, ServerCallContext context)
        {
            var usuarioResponse = new UsuarioResponse();

            try
            {
                var request = registrarParser.Request(usuarioMessage);

                var response = servico.Registrar(request);

                usuarioResponse.HttpStatusCode = (int)response.HttpStatusCode;

            }
            catch (Exception ex)
            {
                //## TODO log

                usuarioResponse.HttpStatusCode = (int)HttpStatusCode.InternalServerError;
            }

            return Task.FromResult(usuarioResponse);
        }

        public override Task<UsuarioResponse> Sincronizar(UsuarioRequest usuarioMessage, ServerCallContext context)
        {
            var usuarioResponse = new UsuarioResponse();

            try
            {
                var request = sincronizarParser.Request(usuarioMessage);

                var response = servico.Sincronizar(request);

                usuarioResponse.HttpStatusCode = (int)response.HttpStatusCode;

            }
            catch (Exception ex)
            {
                //## TODO log

                usuarioResponse.HttpStatusCode = (int)HttpStatusCode.InternalServerError;
            }

            return Task.FromResult(usuarioResponse);
        }

        public override Task<UsuarioResponse> Deletar(UsuarioRequest usuarioMessage, ServerCallContext context)
        {
            var usuarioResponse = new UsuarioResponse();

            try
            {
                var usuarioId = deletarParser.Request(usuarioMessage);

                var response = servico.Deletar(usuarioId);

                usuarioResponse.HttpStatusCode = (int)response.HttpStatusCode;

            }
            catch (Exception ex)
            {
                //## TODO log

                usuarioResponse.HttpStatusCode = (int)HttpStatusCode.InternalServerError;
            }

            return Task.FromResult(usuarioResponse);
        }

    }
}
