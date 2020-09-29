using adduohelper = adduo.helper.envelopes;
using System;
using System.Collections.Generic;
using System.Dynamic;
using System.Net;
using System.Net.Http;
using System.Threading.Tasks;

namespace multiplixe.comum.helper
{
    public class WebRequestHelper
    {
        private static Task<HttpResponseMessage> GetAsync(string url, Dictionary<string, string> headers)
        {
            try
            {
                var client = new HttpClient();

                foreach (var header in headers)
                {
                    client.DefaultRequestHeaders.TryAddWithoutValidation(header.Key, header.Value);
                }

                return client.GetAsync(url) ;
            }
            catch (Exception ex)
            {
                //TODO Logar
                throw ex;
            }
        }

        public static adduohelper.ResponseEnvelope Get(string url)
        {
            return Get(url, new Dictionary<string, string>());
        }

        public static adduohelper.ResponseEnvelope Get(string url, Dictionary<string, string> headers)
        {
            var response = GetAsync(url, headers).Result;

            var envelope = CriarEnvelope(response);

            return envelope;
        }

        public static adduohelper.ResponseEnvelope<T> Get<T>(string url)
        {
            return Get<T>(url, new Dictionary<string, string>());
        }

        public static adduohelper.ResponseEnvelope<T> Get<T>(string url, Dictionary<string, string> headers)
        {
            var response = GetAsync(url, headers).Result;

            var envelope = CriarEnvelope<T>(response);

            return envelope;
        }

        public static adduohelper.ResponseEnvelope<T> GetExterno<T>(string url, Dictionary<string, string> headers)
        {
            var response = GetAsync(url, headers).Result;

            var envelope = CriarEnvelopeExterno<T>(response);

            return envelope;
        }

        public static adduohelper.ResponseEnvelope<T> GetExterno<T>(string url)
        {
            return GetExterno<T>(url, new Dictionary<string, string>());
        }

        private static adduohelper.ResponseEnvelope<T> CriarEnvelope<T>(HttpResponseMessage response)
        {
            var envelope = CriarEnvelope(response);

            var envelopeT = new adduohelper.ResponseEnvelope<T>(envelope);

            if (envelope.HttpStatusCode != HttpStatusCode.InternalServerError)
            {
                string json = response.Content.ReadAsStringAsync().Result;

                if (!string.IsNullOrEmpty(json))
                {
                    envelopeT = Deserializar<adduohelper.ResponseEnvelope<T>>(json);
                }
            }

            return envelopeT;
        }

        private static adduohelper.ResponseEnvelope CriarEnvelope(HttpResponseMessage response)
        {
            var envelope = new adduohelper.ResponseEnvelope();

            envelope.HttpStatusCode = response.StatusCode;

            if (response.StatusCode == System.Net.HttpStatusCode.InternalServerError)
            {
                envelope.Error.Messages.Add(response.ReasonPhrase);
            }

            return envelope;
        }


        private static Task<HttpResponseMessage> PostAsync(string url, object dado)
        {
            return PostAsync(url, dado, new Dictionary<string, string>());
        }

        private static Task<HttpResponseMessage> PostAsync(string url, object dado, Dictionary<string, string> headers)
        {
            try
            {
                using (var httpClientHandler = new HttpClientHandler())
                {
                    httpClientHandler.ServerCertificateCustomValidationCallback = (message, cert, chain, errors) => true;
                    var client = new HttpClient();

                    foreach (var header in headers)
                    {
                        client.DefaultRequestHeaders.TryAddWithoutValidation(header.Key, header.Value);
                    }

                    var stringContent = StringContent(dado);
                    return client.PostAsync(url, stringContent);
                }
            }
            catch (Exception ex)
            {
                //TODO Logar
                throw ex;
            }
        }

        public static adduohelper.ResponseEnvelope Post(string url, object dado)
        {
            return Post(url, dado, new Dictionary<string, string>());
        }

        public static adduohelper.ResponseEnvelope Post(string url, object dado, Dictionary<string, string> headers)
        {
            var response = PostAsync(url, dado, headers).Result;

            var envelope = CriarEnvelope(response);

            return envelope;
        }

        public static adduohelper.ResponseEnvelope<T> Post<T>(string url, object dado)
        {
            var response = PostAsync(url, dado).Result;

            var envelope = CriarEnvelope<T>(response);

            return envelope;
        }

        public static adduohelper.ResponseEnvelope<T> PostExterno<T>(string url, object dado, Dictionary<string, string> headers)
        {
            var response = PostAsync(url, dado, headers).Result;

            var envelope = CriarEnvelopeExterno<T>(response);

            return envelope;
        }

        public static adduohelper.ResponseEnvelope<T> PostExterno<T>(string url, object dado)
        {
            return PostExterno<T>(url, dado, new Dictionary<string, string>());
        }

        private static adduohelper.ResponseEnvelope<T> CriarEnvelopeExterno<T>(HttpResponseMessage response)
        {
            var envelope = CriarEnvelope(response);

            var envelopeT = new adduohelper.ResponseEnvelope<T>(envelope);

            if (envelope.HttpStatusCode != HttpStatusCode.InternalServerError)
            {
                string json = response.Content.ReadAsStringAsync().Result;

                if (!string.IsNullOrEmpty(json))
                {
                    envelopeT.Item = Deserializar<T>(json);
                }
            }

            return envelopeT;
        }

        private static StringContent StringContent(object dado)
        {
            var json = SerializadorHelper.Serializar(dado);
            return HttpHelper.StringContent(json);
        }

        private static T Deserializar<T>(string json)
        {
            return DeserializadorHelper.Deserializar<T>(json);
        }

    }
}
