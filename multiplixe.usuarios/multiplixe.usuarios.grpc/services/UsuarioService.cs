using multiplixe.usuarios.grpc.protos;
using Grpc.Core;
using System.Threading.Tasks;
using System;
using System.Linq.Expressions;
using System.Net;

namespace multiplixe.usuarios.grpc.services
{
    public class UsuarioService : Usuario.UsuarioBase
    {
        private usuario.registro.Servico registroService { get; }
        private parsers.UsuarioRegistrar registrarParser { get; }

        private usuario.atualizacao.Servico atulizacaoService { get; set; }
        private parsers.UsuarioAtualizar atualizarParser { get; }
        private usuario.consulta.Servico consultaService { get; }
        private parsers.UsuarioObter obterParser { get; }
        private parsers.UsuarioListar listarParser { get; }
        private parsers.UsuarioAutenticar autenticarParser { get; }
        private parsers.UltimoAcesso ultimoAcesso { get; }

        public UsuarioService(
            usuario.registro.Servico registroService,
            parsers.UsuarioRegistrar registrarParser,
            usuario.atualizacao.Servico atulizacaoService,
            parsers.UsuarioAtualizar atualizarParser,
            usuario.consulta.Servico consultaService,
            parsers.UsuarioObter obterParser,
            parsers.UsuarioAutenticar autenticarParser,
             parsers.UsuarioListar listarParser,
             parsers.UltimoAcesso ultimoAcesso)
        {
            this.registroService = registroService;
            this.registrarParser = registrarParser;
            this.atualizarParser = atualizarParser;
            this.consultaService = consultaService;
            this.obterParser = obterParser;
            this.listarParser = listarParser;
            this.autenticarParser = autenticarParser;
            this.atulizacaoService = atulizacaoService;
            this.ultimoAcesso = ultimoAcesso;
        }

        public override Task<UsuarioRegistroResponse> Registrar(UsuarioRegistroRequest usuarioMessage, ServerCallContext context)
        {
            var request = registrarParser.Request(usuarioMessage);

            var responseEnvelope = registroService.Registrar(request);

            var response = registrarParser.Response(responseEnvelope);

            return Task.FromResult(response);
        }

        public override Task<UsuarioAtualizacaoResponse> Atualizar(UsuarioAtualizacaoRequest usuarioMessage, ServerCallContext context)
        {
            var request = atualizarParser.Request(usuarioMessage);

            var responseEnvelope = atulizacaoService.Atualizar(request);

            var response = atualizarParser.Response(responseEnvelope);

            return Task.FromResult(response);
        }

        public override Task<UsuarioResponse> Obter(UsuarioFiltroRequest usuarioFiltro, ServerCallContext context)
        {
            var filtro = obterParser.Request(usuarioFiltro);

            var responseEnvelope = consultaService.Obter(filtro);

            var response = obterParser.Response(responseEnvelope);

            return Task.FromResult(response);
        }

        public override Task<UsuarioResponse> Listar(UsuarioFiltroRequest usuarioFiltro, ServerCallContext context)
        {
            var filtro = listarParser.Request(usuarioFiltro);

            var responseEnvelope = consultaService.Listar(filtro);

            var response = listarParser.Response(responseEnvelope);

            return Task.FromResult(response);
        }

        public override Task<UltimoAcessoResponse> UltimoAcesso(UltimoAcessoRequest ultimoAcessoRequest, ServerCallContext context)
        {
            var response = new UltimoAcessoResponse();

            try
            {
                var request = ultimoAcesso.Request(ultimoAcessoRequest);

                atulizacaoService.UltimoAcesso(request);

                response.HttpStatusCode = (int)HttpStatusCode.OK;

            }
            catch(Exception ex)
            {
                response.HttpStatusCode = (int)HttpStatusCode.InternalServerError;
                response.Erro = ex.Message;
            }

            return Task.FromResult(response);
        }
    }
}
