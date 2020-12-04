using Grpc.Core;
using multiplixe.facebook.autenticacao;
using multiplixe.facebook.grpc.Protos;
using System;
using System.Net;
using System.Security.Authentication;
using System.Threading.Tasks;

namespace multiplixe.facebook.grpc.Services
{
    public class AutenticacaoService : Autenticacao.AutenticacaoBase
    {
        private readonly Servico servico;

        public AutenticacaoService(autenticacao.Servico servico)
        {
            this.servico = servico;
        }

        public override Task<ProcessarCodeResponse> ProcessarCode(ProcessarCodeRequest request, ServerCallContext context)
        {
            var response = new ProcessarCodeResponse();

            try
            {
                var usuarioId = Guid.Parse(request.UsuarioId);
                var empresaId = Guid.Parse(request.EmpresaId);

                servico.ProcessarCode(request.Code, usuarioId, empresaId);

                response.HttpStatusCode = (int)HttpStatusCode.OK;
            }
            catch (ArgumentNullException)
            {
                response.HttpStatusCode = (int)HttpStatusCode.BadRequest;
            }
            catch (UnauthorizedAccessException)
            {
                response.HttpStatusCode = (int)HttpStatusCode.Unauthorized;
            }
            catch (InvalidCredentialException)
            {
                response.HttpStatusCode = (int)HttpStatusCode.BadRequest;
            }
            catch (Exception ex)
            {
                response.HttpStatusCode = (int)HttpStatusCode.InternalServerError;
                response.Erro = ex.Message;
            }

            return Task.FromResult(response);
        }

        public override Task<ObterURLAutorizacaoResponse> ObterURLAutorizacao(ObterURLAutorizacaoRequest request, ServerCallContext context)
        {
            var response = new ObterURLAutorizacaoResponse();

            try
            {
                var empresaId = Guid.Parse(request.EmpresaId);

                response.URL = servico.ObterURLAutorizacao(empresaId);

                response.HttpStatusCode = (int)HttpStatusCode.OK;
            }
            catch (Exception ex)
            {
                response.HttpStatusCode = (int)HttpStatusCode.InternalServerError;
                response.Erro = ex.Message;
            }

            return Task.FromResult(response);

        }
    }
}
