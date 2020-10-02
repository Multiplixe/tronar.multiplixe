using multiplixe.classificador.client;
using multiplixe.empresas.client;
using multiplixe.usuarios.client;
using System;
using System.Collections.Generic;
using coredto = multiplixe.comum.dto;
using coreenums = multiplixe.comum.enums;
using corehelper = multiplixe.comum.helper;

namespace multiplixe.notificador.twitch.pubsub.console
{
    public class Whisper
    {
        private GeradorJWT jwt { get; }
        private TwitchSettings twitchSettings { get; }
        private PerfilClient perfilClient { get; }
        private EmpresaClient empresaClient { get; }
        private ClassificadorClient classificadorClient { get; }

        public Whisper(
            GeradorJWT jwt,
            TwitchSettings twitchSettings,
            PerfilClient perfilClient,
            EmpresaClient empresaClient,
            ClassificadorClient classificadorClient)
        {
            this.jwt = jwt;
            this.twitchSettings = twitchSettings;
            this.perfilClient = perfilClient;
            this.empresaClient = empresaClient;
            this.classificadorClient = classificadorClient;
        }

        public void Publicar(coredto.UsuarioParaProcessar usuarioParaProcessar)
        {
            var responsePerfil = perfilClient.Obter(usuarioParaProcessar.UsuarioId, coreenums.RedeSocialEnum.twitch);

            if (responsePerfil.Success)
            {
                var perfil = responsePerfil.Item;

                var responseTwitchInfo = empresaClient.ObterInfoTwitch(perfil.EmpresaId);
                responseTwitchInfo.ThrownIfError();

                var twitchInfo = responseTwitchInfo.Item;

                var claims = GerarClaims(perfil, twitchInfo);

                var token = jwt.Gerar(twitchInfo.ExtensionSecretId, claims);

                var pontuacao = ObterPontuacaoTotal(usuarioParaProcessar.UsuarioId);

                var message = GerarMessage(pontuacao, perfil);

                var headers = GerarHeader(twitchInfo, token);

                var url = GerarUrl(twitchInfo);

                var response = corehelper.WebRequestHelper.Post(url, message, headers);

                response.ThrownIfError();
            }
        }

        private coredto.classificacao.Pontuacao ObterPontuacaoTotal(Guid usuarioId)
        {
            var response = classificadorClient.ObterClassificacao(usuarioId);

            response.ThrownIfError();

            return response.Item.Pontuacao;
        }

        private string GerarUrl(coredto.empresas.TwitchInfo twitchInfo)
        {
            var urlbase = new Uri(twitchSettings.UrlApi);
            var url = new Uri(urlbase, string.Format(twitchSettings.WhisperPathApi, twitchInfo.ChannelId));

            return url.ToString();

        }

        private object GerarMessage(coredto.classificacao.Pontuacao pontuacao, coredto.Perfil perfil)
        {
            var o = new
            {
                activity = "score",
                score = new
                {
                    pointing = new
                    {
                        value = pontuacao.Valor
                    }
                }
            };

            var message = new
            {
                message = corehelper.SerializadorHelper.Serializar(o),
                content_type = "application/json",
                targets = new string[] { $"whisper-U{perfil.PerfilId}" }
            };

            return message;
        }

        private Dictionary<string, string> GerarHeader(coredto.empresas.TwitchInfo twitchInfo, string token)
        {
            var headers = new Dictionary<string, string>
                    {
                        {  "Authorization" , "Bearer " + token },
                        {  "client-id" , twitchInfo.ClientId },
                         { "content-type" , "application/json" }
                    };

            return headers;
        }

        private Dictionary<string, object> GerarClaims(coredto.Perfil perfil, coredto.empresas.TwitchInfo twitchInfo)
        {
            var claims = new Dictionary<string, object>();

            claims["ex"] = corehelper.DateTimeHelper.Timestamp(new TimeSpan(6, 0, 0));
            claims["user_id"] = perfil.PerfilId;
            claims["role"] = "external";
            claims["channel_id"] = twitchInfo.ChannelId;
            claims["pubsub_perms"] = new
            {
                listen = new string[] { $"whisper-U{perfil.PerfilId}" },
                send = new string[] { "whisper-*" }
            };

            return claims;
        }
    }
}
