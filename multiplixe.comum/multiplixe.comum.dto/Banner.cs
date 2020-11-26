using System;
using System.Security.Cryptography;
using System.Text.Json.Serialization;

namespace multiplixe.comum.dto
{
    public class Banner
    {
        [JsonPropertyName("id")]
        public Guid Id { get; set; }

        [JsonPropertyName("image")]
        public string Imagem { get; set; }

        [JsonPropertyName("thumb")]
        public string Thumb { get; set; }

        [JsonPropertyName("url")]
        public string URL { get; set; }

        public Banner()
        {

        }

        public Banner(string id, string imagem, string thumb, string uRL)
        {
            Id = Guid.Parse(id);
            Imagem = imagem;
            Thumb = thumb;
            URL = uRL;
        }
    }
}
