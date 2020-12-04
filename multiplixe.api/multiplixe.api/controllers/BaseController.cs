using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Configuration;
using multiplixe.api.dto.settings;
using multiplixe.comum.dto.interfaces;
using multiplixe.comum.helper;
using multiplixe.enfileirador.client;
using System;
using System.Linq;
using adduohelper = adduo.helper.envelopes;

namespace multiplixe.api.controllers
{
    public class BaseController : ControllerBase
    {
        protected EmpresaSettings empresaSettings { get; set; }
        private IConfiguration configuration { get; }
        protected EnfileiradorClient enfileiradorClient { get; set; }

        public BaseController(IConfiguration configuration,
            EmpresaSettings empresaSettings)
        {
            this.configuration = configuration;
            this.empresaSettings = empresaSettings;
        }

        public void ConfiguraEmpresa(IEmpresaID dto)
        {
            dto.EmpresaId = empresaSettings.Id;
        }

        public Guid ObterEmpresaId()
        {
            return empresaSettings.Id;
        }

        public IActionResult SendPost(string url, object dado)
        {
            var response = WebRequestHelper.Post(url, dado);

            var result = ResultFactory(response);

            return result;
        }

        public IActionResult SendGet(string url)
        {
            var response = WebRequestHelper.Get(url);

            var result = ResultFactory(response);

            return result;
        }


        public IActionResult IntegrarGRPC(integracao_grpc.IIntegracaoGRPC integracao)
        {
            var response = integracao.Enviar();

            var result = ResultFactory(response);

            return result;
        }

        public IActionResult IntegrarGRPC<T>(integracao_grpc.IIntegracaoGRPC<T> integracao)
        {
            var response = integracao.Enviar();

            var result = ResultFactory(response);

            return result;
        }

        public IActionResult SendPost<T>(string url, object dado)
        {
            var response = WebRequestHelper.Post<T>(url, dado);

            var result = ResultFactory(response);

            return result;
        }

        public IActionResult SendGet<T>(string url)
        {
            var response = WebRequestHelper.Get<T>(url);

            var result = ResultFactory(response);

            return result;
        }

        public IActionResult ResultFactory(adduohelper.ResponseEnvelope response)
        {
            IActionResult result;

            if (response.HttpStatusCode == System.Net.HttpStatusCode.InternalServerError)
            {
                var message = response.Error.Messages.Any() ? response.Error.Messages.First() : string.Empty;

                result = StatusCode((int)response.HttpStatusCode, message);
            }
            else
            {
                result = StatusCode((int)response.HttpStatusCode, response);

            }

            return result;
        }
    }
}
