using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Cryptography;
using System.Text;

namespace multiplixe.comum.dto.oauth
{
    public class OAuth10Request
    {
        private string consumerKey { get; }
        private string consumerToken { get; }
        private string accessToken { get; }
        private string accessTokenSecret { get; }
        private string url { get; }
        private string method { get; }

        private Dictionary<string, string> querystring { get; set; }

        private Dictionary<string, string> dic { get; set; }

        private string Header { get; }

        public OAuth10Request(string url, string method, string consumerKey, string consumerToken, string accessToken, string accessTokenSecret) : this(url, method, consumerKey, consumerToken, accessToken, accessTokenSecret, new Dictionary<string, string>())
        {

        }
        public OAuth10Request(string url, string method, string consumerKey, string consumerToken, string accessToken, string accessTokenSecret, Dictionary<string, string> querystring)
        {
            this.url = url;
            this.method = method;
            this.consumerKey = consumerKey;
            this.consumerToken = consumerToken;
            this.accessToken = accessToken;
            this.accessTokenSecret = accessTokenSecret;
            this.querystring = querystring;

            Header = Build();
        }

        private string Build()
        {
            string nonce = DateTime.Now.Ticks.ToString();
            string signatureMethod = "HMAC-SHA1";
            Int64 timestamp = (Int64)(DateTime.UtcNow - new DateTime(1970, 1, 1, 0, 0, 0, 0, DateTimeKind.Utc)).TotalSeconds;
            string version = "1.0";

            dic = new Dictionary<string, string>();
            dic["oauth_nonce"] = nonce;
            dic["oauth_signature_method"] = signatureMethod;
            dic["oauth_timestamp"] = timestamp.ToString();
            dic["oauth_consumer_key"] = this.consumerKey;
            dic["oauth_version"] = version;
            dic["oauth_token"] = accessToken;

            StringBuilder baseString = new StringBuilder(this.method.ToUpper());
            baseString.Append("&");
            baseString.Append(Uri.EscapeDataString(this.url));
            baseString.Append("&");

            if (querystring.Any())
            {
                baseString.Append(Uri.EscapeDataString("include_entities=true"));
                baseString.Append(Uri.EscapeDataString("&"));
            }

            baseString.Append(Uri.EscapeDataString(string.Join("&", dic.OrderBy(kpv => kpv.Key).Select(kpv => $"{kpv.Key}={kpv.Value}"))));

            foreach (var item in querystring)
            {
                var entity = string.Format("&{0}={1}", item.Key, item.Value);
                baseString.AppendFormat(Uri.EscapeDataString(entity));
            }

            string signatureKey = string.Concat(Uri.EscapeDataString(this.consumerToken), "&", Uri.EscapeDataString(this.accessTokenSecret));

            using (HMACSHA1 hasher = new HMACSHA1(Encoding.ASCII.GetBytes(signatureKey)))
            {
                dic["oauth_signature"] = Convert.ToBase64String(hasher.ComputeHash(Encoding.ASCII.GetBytes(baseString.ToString())));
            }

            string headerFormat = "OAuth " + string.Join(",", dic.OrderBy(kpv => kpv.Key).Select(kpv => $"{kpv.Key}=\"{Uri.EscapeDataString(kpv.Value)}\""));

            return headerFormat;
        }

        public override string ToString()
        {
            return Header;
        }
    }
}
