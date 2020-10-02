using System;
using System.Collections.Generic;
using System.Text.Json.Serialization;

namespace multiplixe.comum.dto.classificacao
{
    public class Classificacao
    {
        [JsonPropertyName("id")]
        public Guid UsuarioId { get; set; }

        [JsonPropertyName("socialMedias")]
        public List<RedeSocial> RedesSociais { get; set; }

        [JsonPropertyName("pointing")]
        public Pontuacao Pontuacao { get; set; }

        [JsonPropertyName("balance")]
        public Saldo Saldo { get; set; }

        [JsonPropertyName("level")]
        public Nivel Nivel { get; set; }

        public Classificacao()
        {
            RedesSociais = new List<RedeSocial>();
            Nivel = new Nivel();
            Pontuacao = new Pontuacao();
            Saldo = new Saldo();
        }

    }
}
