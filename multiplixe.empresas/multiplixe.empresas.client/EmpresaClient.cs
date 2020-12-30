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

        public adduohelper.envelopes.ResponseEnvelope<dto.empresas.TwitchInfo> ObterInfoTwitch(Guid empresaId, string contaRedeSocial)
        {
            var response = new adduohelper.envelopes.ResponseEnvelope<dto.empresas.TwitchInfo>();

            response.Item = new dto.empresas.TwitchInfo
            {
                ChannelId = "530843921",
                ExtensionSecretId = "41pX5DUi1UqaBPIkhT2lVWRrqLB5ic1+/6sskCfWI5g=",
                ClientId = "80nsxhmsqq2yo6rg6u44jnhga6svoq",
                ClientSecret = "qt9y1ayu76obaae28vn45ksvym5fla"
            };

            return response;
        }

        public adduohelper.envelopes.ResponseEnvelope<dto.empresas.FacebookInfo> ObterInfoFacebook(Guid empresaId)
        {
            var response = new adduohelper.envelopes.ResponseEnvelope<dto.empresas.FacebookInfo>();

            if (Guid.Parse("5F22E669-8CF2-4702-A828-B32E832A6BA6") == empresaId)
            {
                response.Item = new dto.empresas.FacebookInfo
                {
                    AppId = "1131957397262911",
                    AppSecret = "00aa83ae50e70cb04a3b16c0df1d6034",
                    GraphApiVersao = "v9.0",
                    URLRedirectOauth = "https://app.multiplyx.me/webview/facebook-callback/5F22E669-8CF2-4702-A828-B32E832A6BA6/voraxgg"
                };
            }
            else if (Guid.Parse("E0E6DCE8-4FFD-4500-AD26-20ADECF10A05") == empresaId)
            {
                response.Item = new dto.empresas.FacebookInfo
                {
                    AppId = "178629563033023",
                    AppSecret = "498ab00bad908e95dcae570c47fd530e",
                    GraphApiVersao = "v8.0",
                    URLRedirectOauth = "http://localhost:4200/webview/facebook-callback/E0E6DCE8-4FFD-4500-AD26-20ADECF10A05/voraxgg"
                };
            }

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

        public adduohelper.envelopes.ResponseEnvelope<dto.empresas.TwitterInfo> ObterInfoTwitter(Guid empresaId, string contaRedeSocial)
        {
            var response = new adduohelper.envelopes.ResponseEnvelope<dto.empresas.TwitterInfo>();

            if (Guid.Parse("5F22E669-8CF2-4702-A828-B32E832A6BA6") == empresaId)
            {
                if (contaRedeSocial.ToLower().Equals("voraxgg"))
                {
                    response.Item = new dto.empresas.TwitterInfo
                    {
                        UrlApi = "https://api.twitter.com",
                        ApiKey = "f7a38ZW7um7G0dKKpyWk2qjfg",
                        ConsumerSecret = "qwwZbUmvCQnNk7D3VtWycYAXWnAvsJ85hCstm6fiK7DvSxJk9S",
                        Token = "768588171902877696-QfkRh41AUJbox4uOOYp7GahUYiCqo71",
                        TokenSecret = "dNBylYWHesom6HRwIGZuxiOf3TjO3HBocpD6o9eWieuyi"

                    };
                }
            }
            else if (Guid.Parse("E0E6DCE8-4FFD-4500-AD26-20ADECF10A05") == empresaId)
            {
                response.Item = new dto.empresas.TwitterInfo
                {
                    UrlApi = "https://api.twitter.com",
                    ApiKey = "f7a38ZW7um7G0dKKpyWk2qjfg",
                    ConsumerSecret = "qwwZbUmvCQnNk7D3VtWycYAXWnAvsJ85hCstm6fiK7DvSxJk9S",
                    Token = "768588171902877696-QfkRh41AUJbox4uOOYp7GahUYiCqo71",
                    TokenSecret = "dNBylYWHesom6HRwIGZuxiOf3TjO3HBocpD6o9eWieuyi"

                };
            }

            return response;
        }
    }
}
