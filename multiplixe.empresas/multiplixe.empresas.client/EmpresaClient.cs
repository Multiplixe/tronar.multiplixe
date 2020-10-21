using dto = multiplixe.comum.dto;
using System;
using System.Collections.Generic;
using adduohelper = adduo.helper;

namespace multiplixe.empresas.client
{
    public class EmpresaClient
    {
        public adduohelper.envelopes.ResponseEnvelope<List<dto.empresas.Empresa>> ObterAtivas()
        {

            var empresas = new List<dto.empresas.Empresa>()
            {
                new dto.empresas.Empresa
                {
                    Id = Guid.Parse("5f22e669-8cf2-4702-a828-b32e832a6ba6"),
                    Nome = "Falkol"
                }
            };

            var response = new adduohelper.envelopes.ResponseEnvelope<List<dto.empresas.Empresa>>(empresas);

            return response;
        }

        public adduohelper.envelopes.ResponseEnvelope<dto.empresas.Urls> ObterUrls(Guid empresaId)
        {
            var response = new adduohelper.envelopes.ResponseEnvelope<dto.empresas.Urls>();

            response.Item = new dto.empresas.Urls
            {
                UrlApp = "https://socialgames.tronar.com.br/falkol"
            };

            return response;
        }

        public adduohelper.envelopes.ResponseEnvelope<dto.empresas.TwitchInfo> ObterInfoTwitch(Guid empresaId)
        {
            var response = new adduohelper.envelopes.ResponseEnvelope<dto.empresas.TwitchInfo>();

            response.Item = new dto.empresas.TwitchInfo
            {
                ChannelId = "530843921",
                ExtensionSecretId = "41pX5DUi1UqaBPIkhT2lVWRrqLB5ic1+/6sskCfWI5g=",
                ClientId = "wv45gqkjrr8eetuwhzfy045flhs27v"
            };

            return response;
        }

        public adduohelper.envelopes.ResponseEnvelope<dto.empresas.FacebookInfo> ObterInfoFacebook(Guid empresaId)
        {
            var response = new adduohelper.envelopes.ResponseEnvelope<dto.empresas.FacebookInfo>();

            response.Item = new dto.empresas.FacebookInfo
            {
                AppId = "234854220945907",
                AppSecret = "1d8654798190790951fae1dfb6588b9d",
                GraphApiVersao = "v8.0"
            };

            return response;
        }

        public adduohelper.envelopes.ResponseEnvelope<dto.empresas.FirebaseInfo> ObterInfoFirebase(Guid empresaId)
        {
            var response = new adduohelper.envelopes.ResponseEnvelope<dto.empresas.FirebaseInfo>();

            response.Item = new dto.empresas.FirebaseInfo
            {
                ProjectID = "multipixel-falkol",
                SecretAppID = "a5CPmoX70B0av3BAdbLgtgJjrKKJbW95MSdNwkeh",
                ApiKey = "AIzaSyBGbKBn5tZ1bsR4SvFmPMGs-JQ017U3aeM",
                Usuario = "falkol@multiplixe.com.br",
                Senha = "falkol,123",
                Bucket = "multipixel-falkol"
            };

            return response;
        }


    }
}
