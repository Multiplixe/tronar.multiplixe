using multiplixe.comum.dto;
using System;
using System.Collections.Generic;
using System.Text;
using corehelper = multiplixe.comum.helper;
using adduohelper = adduo.helper.envelopes;
using Microsoft.AspNetCore.Http;

namespace multiplixe.twitch.ping
{
    public class PingKeyService
    {
        private readonly TwitchPingConfig pingConfig;

        public PingKeyService(TwitchPingConfig pingConfig)
        {
            this.pingConfig = pingConfig;
        } 

        public string Gerar(DateTime data)
        {
            return corehelper.CriptografiaHelper.Criptografar(data.ToString(), pingConfig.ChavePingKey);
        }


        public adduohelper.ResponseEnvelope<TwitchPingResponse> ObterProximoPingkey()
        {
            var pingResponse = new TwitchPingResponse();

            var data = corehelper.DateTimeHelper.Now();

            var novoPingHeader = Gerar(data);

            pingResponse.AdicionarUltimoPing(novoPingHeader);

            var response = new adduohelper.ResponseEnvelope<TwitchPingResponse>(pingResponse);

            return response;
        }

        public DateTime ExtrairPingKey(string pingKeyHeader)
        {
            var pingKeyDecript = corehelper.CriptografiaHelper.Descriptografar(pingKeyHeader, pingConfig.ChavePingKey);

            var pingkeyDateTime = Convert.ToDateTime(pingKeyDecript);

            return pingkeyDateTime;
        }

        public int ExtrairPingPausa(string pingPausaHeader)
        {
            var pausa = 0;

            if (!string.IsNullOrEmpty(pingPausaHeader))
            {
                var f = Convert.FromBase64String(pingPausaHeader);
                var fatorString = Encoding.UTF8.GetString(f);
                int.TryParse(fatorString, out pausa);
            }

            return pausa;
        }
    }
}
