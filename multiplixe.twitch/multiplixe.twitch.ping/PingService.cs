using adduo.helper.envelopes;
using multiplixe.comum.dto;
using multiplixe.comum.exceptions;
using multiplixe.comum.helper;
using multiplixe.enfileirador.client;
using multiplixe.twitch.dto.eventos;
using multiplixe.usuarios.client;
using System;
using System.Net;
using adduohelper = adduo.helper.envelopes;
using comum_dto = multiplixe.comum.dto;
using corehelper = multiplixe.comum.helper;
using enums = multiplixe.comum.enums;

namespace multiplixe.twitch.ping
{
    public class PingService : BaseService
    {
        private readonly PingKeyService pingKeyService;
        private readonly EnfileiradorClient enfileiradorClient;
        private readonly PingValidar pingValidar;

        private TwitchPingConfig pingConfig { get; }

        public PingService(
            PingKeyService pingKeyService,
            TwitchPingConfig pingConfig,
            PerfilClient perfilClient,
            EnfileiradorClient enfileiradorClient,
            PingValidar pingValidar) : base(perfilClient)
        {
            this.pingKeyService = pingKeyService;
            this.pingConfig = pingConfig;
            this.enfileiradorClient = enfileiradorClient;
            this.pingValidar = pingValidar;
        }

        public adduohelper.ResponseEnvelope<TwitchPingResponse> Iniciar(string user_id, Guid empresaId)
        {
            var responsePerfil = ObterPerfil(user_id, empresaId);

            if (!responsePerfil.Success)
            {
                throw new NotFoundException();
            }

            var pingResponse = CriarTwitchPingResponse();

            var response = new adduohelper.ResponseEnvelope<TwitchPingResponse>(pingResponse);

            return response;
        }

        private TwitchPingResponse CriarTwitchPingResponse()
        {
            var response = new TwitchPingResponse
            {
                Chamada = pingConfig.Chamada,
                FrequenciaMinutos = pingConfig.FrequenciaMinutos,
            };

            var data = corehelper.DateTimeHelper.Now();
            var novoPingHeader = pingKeyService.Gerar(data);

            response.AdicionarUltimoPing(novoPingHeader);

            return response;
        }

        public ResponseEnvelope<TwitchPingResponse> Pingar(string twitchUserId, string channelId, string is_unlinked, string pingKeyHeader, string pingPausaHeader, Guid empresaId)
        {
            pingValidar.Validar(is_unlinked, twitchUserId, pingKeyHeader);

            var evento = GerarEvento(twitchUserId, channelId, pingKeyHeader, pingPausaHeader);

            var envelope = new comum_dto.EnvelopeEvento<dto.eventos.Evento>(evento);
            envelope.DataEvento = evento.Ping.Atual;
            envelope.EmpresaId = empresaId;

            enfileiradorClient.EnfileirarParaTriadorTwitch(envelope);

            var proximoPing = ProximoPingkey();

            return proximoPing;
        }

        private Evento GerarEvento(string user_id, string channel_id, string pingKeyHeader, string pingPausaHeader)
        {
            var evento = new Evento()
            {
                TipoEvento = enums.TipoEventoEnum.ping,
                PerfilId = user_id,
                Ping = new EventoPing()
                {
                    Atual = corehelper.DateTimeHelper.Now(),
                    Ultimo = pingKeyService.ExtrairPingKey(pingKeyHeader),
                    PausaMilissegundos = pingKeyService.ExtrairPingPausa(pingPausaHeader),
                    FrequenciaMinutos = pingConfig.FrequenciaMinutos,
                    ToleranciaSegundos = pingConfig.ToleranciaSegundos,
                    PerfilId = user_id,
                    PostId = $"{channel_id}-{DateTimeHelper.Now().ToString("yyyyMMddHHmm")}"
                }
            };

            return evento;
        }

        private ResponseEnvelope<TwitchPingResponse> ProximoPingkey()
        {
            var pingResponse = CriarTwitchPingResponse();

            var response = new adduohelper.ResponseEnvelope<TwitchPingResponse>(pingResponse);

            return response;
        }
    }
}
