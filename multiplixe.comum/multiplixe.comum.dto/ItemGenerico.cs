using adduo.helper.envelopes;
using System.Text.Json.Serialization;

namespace multiplixe.comum.dto
{
    public class ItemGenerico
    {
        [JsonPropertyName("id")]
        public int Id { get; set; }

        [JsonPropertyName("name")]
        public string Nome { get; set; }

        public ItemGenerico()
        {
        }

        public ItemGenerico(int id, string nome)
        {
            Id = id;
            Nome = nome;
        }
    }
}
