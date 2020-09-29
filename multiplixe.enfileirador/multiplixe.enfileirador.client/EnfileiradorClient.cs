using Grpc.Net.Client;
using multiplixe.comum.dto;
using multiplixe.comum.enums;
using multiplixe.comum.helper;
using multiplixe.comum.helper.grpc;
using multiplixe.enfileirador.grpc.Protos;
using System;
using System.Collections.Generic;
using System.Text;

namespace multiplixe.enfileirador.client
{
    public class EnfileiradorClient
    {
        private GrpcChannel channel { get; set; }

        private Enfileirador.EnfileiradorClient client { get; set; }

        public EnfileiradorClient()
        {
            channel = GrcpChannelHelper.CreateChannel(PortasServicosEnum.enfileirador);

            client = new Enfileirador.EnfileiradorClient(channel);
        }

        #region COnfigs

        public EnfileiradorConfig TriadorFacebook()
        {
            return core.Factory.TriadorFacebook();
        }
        public EnfileiradorConfig TriadorTwitter()
        {
            return core.Factory.TriadorTwitter();
        }

        public EnfileiradorConfig TriadorTwitch()
        {
            return core.Factory.TriadorTwitch();
        }

        public EnfileiradorConfig TriadorYoutube()
        {
            return core.Factory.TriadorYoutube();
        }


        public EnfileiradorConfig PontuadorReacaoFacebook()
        {
            return core.Factory.PontuadorReacaoFacebook();
        }

        public EnfileiradorConfig PontuadorReacaoTwitter()
        {
            return core.Factory.PontuadorReacaoTwitter();
        }

        public EnfileiradorConfig PontuadorPingTwitch()
        {
            return core.Factory.PontuadorPingTwitch();
        }

        public EnfileiradorConfig PontuadorLiveHashtagYoutube()
        {
            return core.Factory.PontuadorLiveHashtagYoutube();
        }

        public EnfileiradorConfig Classificador()
        {
            return core.Factory.Classificador();
        }

        public EnfileiradorConfig NotificadorEmail()
        {
            return core.Factory.NotificadorEmail();
        }

        public EnfileiradorConfig NotificadorPush()
        {
            return core.Factory.NotificadorPush();
        }

        public EnfileiradorConfig NotificadorTwitchPubSub()
        {
            return core.Factory.NotificadorTwitchPubSub();
        }

        public EnfileiradorConfig Verificacao()
        {
            return core.Factory.Verificacao();
        }

        public EnfileiradorConfig Pontuador()
        {
            return core.Factory.Pontuador();
        }

        public EnfileiradorConfig PosClassificador()
        {
            return core.Factory.PosClassificador();
        }

        public EnfileiradorConfig Avatar()
        {
            return core.Factory.Avatar();
        }


        #endregion

        public void EnfileirarParaTriadorFacebook(object dto)
        {
            var config = TriadorFacebook();
            Enfileirar(config.Nome, dto);
        }

        public void EnfileirarParaTriadorTwitter(object dto)
        {
            var config = TriadorTwitter();
            Enfileirar(config.Nome, dto);
        }

        public void EnfileirarParaTriadorTwitch(object dto)
        {
            var config = TriadorTwitch();
            Enfileirar(config.Nome, dto);
        }

        public void EnfileirarParaTriadorYoutube(object dto)
        {
            var config = TriadorYoutube();
            Enfileirar(config.Nome, dto);
        }

        public void EnfileirarParaPontuadorReacaoFacebook(object dto)
        {
            var config = PontuadorReacaoFacebook();
            Enfileirar(config.Nome, dto);
        }

        public void EnfileirarParaPontuadorReacaoTwitter(object dto)
        {
            var config = PontuadorReacaoTwitter();
            Enfileirar(config.Nome, dto);
        }

        public void EnfileirarParaPontuadorPingTwitch(object dto)
        {
            var config = PontuadorPingTwitch();
            Enfileirar(config.Nome, dto);
        }

        public void EnfileirarParaPontuadorLiveHashtagYoutube(object dto)
        {
            var config = PontuadorLiveHashtagYoutube();
            Enfileirar(config.Nome, dto);
        }

        public void EnfileirarParaClassificador(object dto)
        {
            var config = Classificador();
            Enfileirar(config.Nome, dto);
        }

        public void EnfileirarParaNotificadorEmail(object dto)
        {
            var config = NotificadorEmail();
            Enfileirar(config.Nome, dto);
        }

        public void EnfileirarParaNotificadorPush(object dto)
        {
            var config = NotificadorPush();
            Enfileirar(config.Nome, dto);
        }

        public void EnfileirarParaNotificadorTwichPubSub(object dto)
        {
            var config = NotificadorTwitchPubSub();
            Enfileirar(config.Nome, dto);
        }

        public void EnfileirarParaPontuador(object dto)
        {
            var config = Pontuador();
            Enfileirar(config.Nome, dto);
        }

        public void EnfileirarParaPosClassificador(object dto)
        {
            var config = PosClassificador();
            Enfileirar(config.Nome, dto);
        }

        public void EnfileirarAvatar(object dto)
        {
            var config = Avatar();
            Enfileirar(config.Nome, dto);
        }

        public void Enfileirar(string nomeFila, object dto)
        {
            var json = Serializar(dto);

            var message = new ItemMessage
            {
                NomeFila = nomeFila,
                Json = json
            };

            client.EnfileirarAsync(message).GetAwaiter();
        }

        private string Serializar(object dto)
        {
            return SerializadorHelper.Serializar(dto);
        }
    }
}
