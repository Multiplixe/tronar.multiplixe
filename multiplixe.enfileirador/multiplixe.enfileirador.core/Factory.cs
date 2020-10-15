using System;
using System.Linq;
using dto = multiplixe.comum.dto;

namespace multiplixe.enfileirador.core
{
    public class Factory
    {
        private static string hostName = "localhost";

        private static string filaTriadorFacebook = "triador.facebook";
        private static string filaTriadorTwitter = "triador.twitter";
        private static string filaTriadorTwitch = "triador.twitch";
        private static string filaTriadorYoutube = "triador.youtube";

        private static string filaPontuadorReacaoFacebook = "pontuador.reacao.facebook";
        private static string filaPontuadorReacaoTwitter = "pontuador.reacao.twitter";
        private static string filaPontuadorPingTwitch = "pontuador.ping.twitch";
        private static string filaPontuadorLiveHashtagYoutube = "pontuador.live.hashtag.youtube";
        
        private static string filaConsolidador = "consolidador";

        private static string filaClassificador = "classificador.v2";

        private static string filaPosClassificador = "pos-classificador";

        private static string filaNotificadorEmail = "notificador.email";
        private static string filaNotificadorPush = "notificador.push";
        private static string filaNotificadorTwitchPubSub = "notificador.twitch.pubsub";

        private static string filaAvatar = "avatar";

        private static string filaCompartilhadorFacebook = "compartilhador.facebook";
        private static string filaCompartilhadorTwitter = "compartilhador.twitter";


        private static string filaVerificacao = "verificacao";

        public static dto.EnfileiradorConfig TriadorFacebook()
        {
            return Obtem(filaTriadorFacebook);
        }
        public static dto.EnfileiradorConfig TriadorTwitter()
        {
            return Obtem(filaTriadorTwitter);
        }

        public static dto.EnfileiradorConfig TriadorTwitch()
        {
            return Obtem(filaTriadorTwitch);
        }

        public static dto.EnfileiradorConfig TriadorYoutube()
        {
            return Obtem(filaTriadorYoutube);
        }

        public static dto.EnfileiradorConfig PontuadorReacaoFacebook()
        {
            return Obtem(filaPontuadorReacaoFacebook);
        }

        public static dto.EnfileiradorConfig PontuadorReacaoTwitter()
        {
            return Obtem(filaPontuadorReacaoTwitter);
        }

        public static dto.EnfileiradorConfig PontuadorPingTwitch()
        {
            return Obtem(filaPontuadorPingTwitch);
        }

        public static dto.EnfileiradorConfig PontuadorLiveHashtagYoutube()
        {
            return Obtem(filaPontuadorLiveHashtagYoutube);
        }

        public static dto.EnfileiradorConfig Consolidador()
        {
            return Obtem(filaConsolidador);
        }

        public static dto.EnfileiradorConfig Classificador()
        {
            return Obtem(filaClassificador);
        }


        public static dto.EnfileiradorConfig Verificacao()
        {
            return Obtem(filaVerificacao);
        }

        public static dto.EnfileiradorConfig NotificadorEmail()
        {
            return Obtem(filaNotificadorEmail);
        }
        public static dto.EnfileiradorConfig NotificadorPush()
        {
            return Obtem(filaNotificadorPush);
        }

        public static dto.EnfileiradorConfig NotificadorTwitchPubSub()
        {
            return Obtem(filaNotificadorTwitchPubSub);
        }

        public static dto.EnfileiradorConfig PosClassificador()
        {
            return Obtem(filaPosClassificador);
        }

        public static dto.EnfileiradorConfig Avatar()
        {
            return Obtem(filaAvatar);
        }

        public static dto.EnfileiradorConfig CompartilhadorFacebook()
        {
            return Obtem(filaCompartilhadorFacebook);
        }

        public static dto.EnfileiradorConfig CompartilhadorTwitter()
        {
            return Obtem(filaCompartilhadorTwitter);
        }


        public static dto.EnfileiradorConfig Obtem(string nomeDaFila)
        {
            var durableConfig = new string[]
            {
                filaTriadorFacebook,
                filaPontuadorReacaoFacebook,
                filaPontuadorReacaoTwitter,
                filaPontuadorPingTwitch,
                filaPontuadorLiveHashtagYoutube,
                filaTriadorTwitter,
                filaTriadorTwitch,
                filaTriadorYoutube,
                filaClassificador,
                filaNotificadorEmail,
                filaNotificadorPush,
                filaVerificacao,
                filaConsolidador,
                filaPosClassificador,
                filaNotificadorTwitchPubSub,
                filaAvatar,
                filaCompartilhadorFacebook,
                filaCompartilhadorTwitter
            };

            if (durableConfig.Contains(nomeDaFila))
            {
                return GerarConfig(nomeDaFila, true, false, false, false);
            }

            throw new Exception($"Fila {nomeDaFila} não exite!");
        }

        private static dto.EnfileiradorConfig GerarConfig(string nomeFila, bool durable, bool exclusive, bool autoDelete, bool autoAck)
        {
            return new dto.EnfileiradorConfig(hostName, nomeFila, durable, exclusive, autoDelete, autoAck);
        }
    }
}
