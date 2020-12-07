using multiplixe.usuarios.perfil.grpc.Protos;
using Grpc.Core;
using System;
using System.Threading.Tasks;
using System.Net;
using multiplixe.comum.enums;
using adduo.helper.envelopes.exeptions;

namespace multiplixe.usuarios.grpc.services
{
    public class PerfilService : Perfil.PerfilBase
    {
        private perfil.PerfilServico perfilService { get; }
        private perfil.AccessTokenServico accessTokenServico { get; }
        private parsers.PerfilRegistrar perfilRegistrarParser { get; }
        private parsers.PerfilObterPerfisConectados perfilObterPerfisConectadosParser { get; }
        private parsers.PerfilObter perfilObterParser { get; }
        private parsers.Desconectar desconectar { get; }

        

        public PerfilService(
            perfil.PerfilServico perfilService,
            perfil.AccessTokenServico accessTokenServico,
            parsers.PerfilRegistrar perfilRegistrarParser,
            parsers.PerfilObterPerfisConectados perfilObterPerfisConectadosParser,
            parsers.PerfilObter perfilObterParser,
            parsers.Desconectar desconectar)
        {
            this.perfilService = perfilService;
            this.accessTokenServico = accessTokenServico;
            this.perfilRegistrarParser = perfilRegistrarParser;
            this.perfilObterPerfisConectadosParser = perfilObterPerfisConectadosParser;
            this.perfilObterParser = perfilObterParser;
            this.desconectar = desconectar;
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
            catch (ArgumentException ex)
            {
                response.HttpStatusCode = (int)HttpStatusCode.BadRequest;
            }
            catch (EnvelopeException env)
            {
                response.HttpStatusCode = (int)env.HttpStatusCode;
            }
            catch (Exception)
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
            var response = new AccessTokenResponse();

            try
            {
                var usuarioId = Guid.Parse(request.UsuarioId);
                var redesocial = (RedeSocialEnum)request.RedeSocial;

                var envelopeResponse = accessTokenServico.Obter(usuarioId, redesocial);

                response.HttpStatusCode = (int)envelopeResponse.HttpStatusCode;

                if (envelopeResponse.Success)
                {
                    response.Token = envelopeResponse.Item.Token;
                }
            }
            catch (EnvelopeException env)
            {
                response.HttpStatusCode = (int)env.HttpStatusCode;
            }
            catch (Exception)
            {
                response.HttpStatusCode = (int)HttpStatusCode.InternalServerError;
            }

            return Task.FromResult(response);
        }


        public override Task<DesconectarResponse> Desconectar(DesconectarRequest request, ServerCallContext context)
        {
            var response = new DesconectarResponse();

            try
            {
                var perfil = desconectar.Request(request);

                perfilService.Desconectar(perfil);

                response.HttpStatusCode = (int)HttpStatusCode.OK;

            }
            catch (Exception ex)
            {
                response.Erro = ex.Message; 
                response.HttpStatusCode = (int)HttpStatusCode.InternalServerError;
            }

            return Task.FromResult(response);
        }

    }
}
