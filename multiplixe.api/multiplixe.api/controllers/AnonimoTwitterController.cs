using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Configuration;
using multiplixe.api.dto.settings;
using multiplixe.api.log_eventos;
using multiplixe.enfileirador.client;
using System;
using System.Security.Cryptography;
using System.Text;
using comum_dto = multiplixe.comum.dto;
using corehelper = multiplixe.comum.helper;
using twitter_dto = multiplixe.twitter.dto;
using twitteroauth = multiplixe.twitter.oauth;

namespace multiplixe.api.controllers
{
    [ApiController]
    [Route("twitter")]
    public class AnonimoTwitterController : BaseController
    {
        private TwitterLogEventoService twitterLogEventoService { get; }
        private twitteroauth.dtos.AuthContext authContext { get; }

        public AnonimoTwitterController(
                IConfiguration configuration,
                EmpresaSettings empresaSettings,
                TwitterLogEventoService twitterLogEventoService,
                EnfileiradorClient enfileiradorClient,
                twitteroauth.dtos.AuthContext authContext) : base(configuration, empresaSettings)
        {
            this.enfileiradorClient = enfileiradorClient;
            this.twitterLogEventoService = twitterLogEventoService;
            this.authContext = authContext;
        }

        [HttpPost]
        public ActionResult Post([FromBody] twitter_dto.eventos.Evento evento)
        {
            twitterLogEventoService.LogarEvento(evento);

            var envelope = new comum_dto.EnvelopeEvento<twitter_dto.eventos.Evento>(evento);
            envelope.DataEvento = corehelper.DateTimeHelper.Now();

            ConfiguraEmpresa(envelope);

            enfileiradorClient.EnfileirarParaTriadorTwitter(envelope);

            return Ok();
        }

        [HttpGet]
        public twitter_dto.CRCResponse Get([FromQuery(Name = "crc_token")] string crc_token)
        {
            twitterLogEventoService.LogarRequestInicial(Request);

            if (string.IsNullOrEmpty(crc_token))
            {
                return null;
            }
            else
            {
                var encoding = new ASCIIEncoding();
                var key = encoding.GetBytes(authContext.ConsumerSecret);
                var crc_tokenBytes = encoding.GetBytes(crc_token);

                using (HMACSHA256 hMACSHA256 = new HMACSHA256(key))
                {
                    var hash = hMACSHA256.ComputeHash(crc_tokenBytes);

                    var response = string.Format("sha256={0}", Convert.ToBase64String(hash));

                    var crc = new twitter_dto.CRCResponse(response);

                    return crc;
                }
            }
        }
    }
}