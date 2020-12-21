using Grpc.Core;
using multiplixe.twitter.grpc.Protos;
using multiplixe.twitter.webhook;
using System;
using System.Net;
using System.Threading.Tasks;

namespace multiplixe.twitter.grpc.Services
{
    public class WebhookService : Webhook.WebhookBase
    {
        private readonly CRCService cRCService;

        public WebhookService(CRCService cRCService)
        {
            this.cRCService = cRCService;
        }

        public override Task<CRCResponse> ProcessarCRC(CRCRequest request, ServerCallContext context)
        {
            var response = new CRCResponse();

            try
            {
                var empresaId = Guid.Parse(request.EmpresaId);
                response.ResponseToken = cRCService.ProcessarCRC(request.CRC, empresaId, request.ContaRedeSocial);
                response.HttpStatusCode = (int)HttpStatusCode.OK;
            }
            catch (ArgumentNullException ex)
            {
                response.HttpStatusCode = (int)HttpStatusCode.BadRequest;
                response.Erro = string.Format("{0} - {1}", ex.Message, ex.ParamName);  
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
