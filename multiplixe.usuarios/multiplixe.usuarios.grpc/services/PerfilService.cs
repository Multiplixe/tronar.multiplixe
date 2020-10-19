using multiplixe.usuarios.perfil.grpc.Protos;
using Grpc.Core;
using System;
using System.Threading.Tasks;
using System.Net;

namespace multiplixe.usuarios.grpc.services
{
    public class PerfilService : Perfil.PerfilBase
    {
        private perfil.PerfilServico perfilService { get; }
        private parsers.PerfilRegistrar perfilRegistrarParser { get; }
        private parsers.PerfilObterPerfisConectados perfilObterPerfisConectadosParser { get; }
        private parsers.PerfilObter perfilObterParser { get; }

        public PerfilService(
            perfil.PerfilServico perfilService,
            parsers.PerfilRegistrar perfilRegistrarParser,
            parsers.PerfilObterPerfisConectados perfilObterPerfisConectadosParser,
            parsers.PerfilObter perfilObterParser)
        {
            this.perfilService = perfilService;
            this.perfilRegistrarParser = perfilRegistrarParser;
            this.perfilObterPerfisConectadosParser = perfilObterPerfisConectadosParser;
            this.perfilObterParser = perfilObterParser;
        }

        public override Task<PerfilResponse> Obter(PerfilFiltro perfilFiltro, ServerCallContext context)
        {
            var response = new PerfilResponse();

            try
            {
                var filtro = perfilObterParser.Request(perfilFiltro);

                var envelope = perfilService.Obter(filtro);

                response = new PerfilResponse
                {
                    HttpStatusCode = (int)envelope.HttpStatusCode,
                };

                if (envelope.Success)
                {
                    response.Perfil = new PerfilMessage
                    {
                        Nome = envelope.Item.Nome,
                        UsuarioId = envelope.Item.UsuarioId.ToString(),
                        EmpresaId = envelope.Item.EmpresaId.ToString(),
                        PerfilId = envelope.Item.PerfilId,
                        RedeSocial = envelope.Item.RedeSocialId,
                        DataCadastro = envelope.Item.DataCadastro.Ticks,
                        Ativo = envelope.Item.Ativo,
                        ImagemUrl = envelope.Item.ImagemUrl,
                        Login = envelope.Item.Login
                    };
                }

            }
            catch (Exception ex)
            {
                response.HttpStatusCode = (int)HttpStatusCode.InternalServerError;
            }

            return Task.FromResult(response);
        }

        public override Task<PerfilResponse> Registrar(PerfilMessage request, ServerCallContext context)
        {
            var response = new PerfilResponse();

            try
            {
                var requestEnvelope = perfilRegistrarParser.Request(request);

                var envelope = perfilService.Registrar(requestEnvelope);

                response = new PerfilResponse
                {
                    HttpStatusCode = (int)envelope.HttpStatusCode,
                };
            }
            catch (Exception ex)
            {
                response.HttpStatusCode = (int)HttpStatusCode.InternalServerError;
            }

            return Task.FromResult(response);
        }

        public override Task<PerfilConectadoResponse> ObterPerfisConectados(PerfilFiltro filtro, ServerCallContext context)
        {
            var response = new PerfilConectadoResponse();

            try
            {
                var usuarioId = Guid.Parse(filtro.UsuarioId);

                var envelopeResponse = perfilService.ObterPerfisConectados(usuarioId);

                response = perfilObterPerfisConectadosParser.Response(envelopeResponse);
            }
            catch (Exception ex)
            {
                response.HttpStatusCode = (int)HttpStatusCode.InternalServerError;
            }

            return Task.FromResult(response);
        }

        public override Task<AccessTokenResponse> ObterAccessToken(AccessTokenRequest request, ServerCallContext context)
        {
            return Task.FromResult(new AccessTokenResponse {
                HttpStatusCode = 200,
                Token = "token do usuario na rede social XPTO"
            });

        }
    }
}
