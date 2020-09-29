using Microsoft.AspNetCore.Http;
using multiplixe.classificador.client;
using multiplixe.comum.helper;
using multiplixe.twitch.dto.eventos;
using multiplixe.twitch.ping.dtos;
using multiplixe.usuarios.client;
using System;
using System.Net;
using System.Text;
using adduohelper = adduo.helper.envelopes;
using comum_dto = multiplixe.comum.dto;
using corehelper = multiplixe.comum.helper;
using enums = multiplixe.comum.enums;

namespace multiplixe.twitch.ping
{
    public class PingService
    {
        private readonly string pingKeyHeader = "ping-key";
        private readonly string pingPausaHeader = "ping-pause";

        private PingConfig pingConfig { get; }
        private PerfilClient perfilClient { get; }
        private ClassificadorClient classificadorClient { get; }

        public PingService(
            PingConfig pingConfig,
            PerfilClient perfilClient,
            ClassificadorClient classificadorClient)
        {
            this.pingConfig = pingConfig;
            this.perfilClient = perfilClient;
            this.classificadorClient = classificadorClient;
        }

        private string GerarPingkey(DateTime data)
        {
            return corehelper.CriptografiaHelper.Criptografar(data.ToString(), pingConfig.ChavePingKey);
        }

        public adduohelper.ResponseEnvelope<PingResponse> ProximoPingkey()
        {
            var pingResponse = new PingResponse();

            var data = corehelper.DateTimeHelper.Now();

            var novoPingHeader = GerarPingkey(data);

            pingResponse.AdicionarUltimoPing(novoPingHeader);

            var response = new adduohelper.ResponseEnvelope<PingResponse>(pingResponse);

            return response;
        }

        public adduohelper.ResponseEnvelope<PingResponse> PingInicial(string user_id, Guid empresaId)
        {
            var responsePerfil = ObterPerfil(user_id, empresaId);

            var pingResponse = new PingResponse();
            pingResponse.Chamada = pingConfig.Chamada;

            var response = new adduohelper.ResponseEnvelope<PingResponse>(pingResponse);

            if (responsePerfil.Success)
            {
                var perfil = responsePerfil.Item;

                var data = corehelper.DateTimeHelper.Now();

                var novoPingHeader = GerarPingkey(data);

                pingResponse.AdicionarUltimoPing(novoPingHeader);
                pingResponse.FrequenciaMinutos = pingConfig.FrequenciaMinutos;

                try
                {
                    var responseClassificacao = classificadorClient.ObterClassificacao(perfil.UsuarioId);

                    if (responseClassificacao.Success)
                    {
                        pingResponse.Classificacao = responseClassificacao.Item;
                    }
                }
                catch (Exception ex)
                {
                    // ## TODO logo
                }

            }
            else
            {
                response.HttpStatusCode = HttpStatusCode.NotFound;
            }


            return response;
        }

        private adduohelper.ResponseEnvelope<comum_dto.Perfil> ObterPerfil(string user_id, Guid empresaId)
        {
            return perfilClient.Obter(empresaId, enums.RedeSocialEnum.twitch, user_id);

        }

        public Evento GerarEvento(IHeaderDictionary header, string user_id, string channel_id)
        {
            var evento = new Evento()
            {
                TipoEvento = enums.TipoEventoEnum.ping,
                PerfilId = user_id,
                Ping = new EventoPing()
                {
                    Atual = corehelper.DateTimeHelper.Now(),
                    Ultimo = ExtrairPingKey(header),
                    PausaMilissegundos = ExtrairPingPausa(header),
                    FrequenciaMinutos = pingConfig.FrequenciaMinutos,
                    ToleranciaSegundos = pingConfig.ToleranciaSegundos,
                    PerfilId = user_id,
                    PostId = $"{channel_id}-{DateTimeHelper.Now().ToString("yyyyMMddHHmm")}"
                }
            };

            return evento;
        }


        private DateTime ExtrairPingKey(IHeaderDictionary header)
        {
            var pingKeyDecript = corehelper.CriptografiaHelper.Descriptografar(header[pingKeyHeader], pingConfig.ChavePingKey);

            var pingkeyDateTime = Convert.ToDateTime(pingKeyDecript);

            return pingkeyDateTime;
        }

        private int ExtrairPingPausa(IHeaderDictionary header)
        {
            var pingPausa = string.Empty;

            if (header.ContainsKey(pingPausaHeader))
            {
                pingPausa = header[pingPausaHeader];
            }

            var pausa = 0;

            if (!string.IsNullOrEmpty(pingPausa))
            {
                var f = Convert.FromBase64String(pingPausa);
                var fatorString = Encoding.UTF8.GetString(f);
                int.TryParse(fatorString, out pausa);
            }

            return pausa;
        }


    }
}
