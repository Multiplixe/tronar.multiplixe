using adduohelper = adduo.helper.envelopes;
using dto = multiplixe.comum.dto;

using multiplixe.usuarios.grpc.protos;
using System.Collections.Generic;

namespace multiplixe.usuarios.client
{
    public class UsuarioClient : BaseClient
    {
        private Usuario.UsuarioClient usuarioClient { get; set; }
        private parsers.UsuarioRegistrar registrarParser { get; }
        private parsers.UsuarioAtualizar atualizarParser { get; }
        private parsers.UsuarioObter obterParser { get; }
        private parsers.UsuarioListar listarParser { get; }

        public UsuarioClient()
        {
            usuarioClient = new Usuario.UsuarioClient(channel);
            registrarParser = new parsers.UsuarioRegistrar();
            atualizarParser = new parsers.UsuarioAtualizar();
            obterParser = new parsers.UsuarioObter();
            listarParser = new parsers.UsuarioListar();
        }

        public adduohelper.ResponseEnvelope<dto.entries.UsuarioRegistro> Registrar(adduohelper.RequestEnvelope<dto.entries.UsuarioRegistro> request)
        {
            var usuarioMessage = registrarParser.Request(request);

            var usuarioResponse = usuarioClient.Registrar(usuarioMessage);

            var response = registrarParser.Response(usuarioResponse);

            return response;
        }

        public adduohelper.ResponseEnvelope<dto.entries.UsuarioAtualizacao> Atualizar(adduohelper.RequestEnvelope<dto.entries.UsuarioAtualizacao> request)
        {
            var usuarioMessage = atualizarParser.Request(request);

            var usuarioResponse = usuarioClient.Atualizar(usuarioMessage);

            var response = atualizarParser.Response(usuarioResponse);

            return response;
        }

        public adduohelper.ResponseEnvelope<dto.Usuario> Obter(adduohelper.RequestEnvelope<dto.filtros.UsuarioFiltro> request)
        {
            var usuarioFiltro = obterParser.Request(request);

            var usuarioResponse = usuarioClient.Obter(usuarioFiltro);

            var response = obterParser.Response(usuarioResponse);

            return response;
        }

        public adduohelper.ResponseEnvelope<List<dto.Usuario>> Listar(adduohelper.RequestEnvelope<dto.filtros.UsuarioFiltro> request)
        {
            var usuarioFiltro = listarParser.Request(request);

            var usuarioResponse = usuarioClient.Listar(usuarioFiltro);

            var response = listarParser.Response(usuarioResponse);

            return response;
        }
    }
}
