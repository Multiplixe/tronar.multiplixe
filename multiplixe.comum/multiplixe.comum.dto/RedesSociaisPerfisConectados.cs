using multiplixe.comum.enums;
using System.Collections.Generic;
using System.Linq;
using System.Text.Json.Serialization;

namespace multiplixe.comum.dto
{
    public class RedesSociaisPerfisConectados
    {
        [JsonPropertyName("facebook")]
        public List<Perfil> Facebook { get { return Obter(RedeSocialEnum.facebook); } }

        [JsonPropertyName("twitter")]
        public List<Perfil> Twitter { get { return Obter(RedeSocialEnum.twitter); } }

        [JsonPropertyName("twitch")]
        public List<Perfil> Twitch { get { return Obter(RedeSocialEnum.twitch); } }

        [JsonPropertyName("youtube")]
        public List<Perfil> Youtube { get { return Obter(RedeSocialEnum.youtube); } }

        [JsonPropertyName("hasConnection")]
        public bool TemConexao { get; set; }

        [JsonIgnore]
        public List<Perfil> Perfis { get; set; }

        public RedesSociaisPerfisConectados()
        {
            Perfis = new List<Perfil>();
        }

        private List<Perfil> Obter(RedeSocialEnum redesocial)
        {
            return Perfis.Where(w => w.RedeSocial == redesocial).ToList();
        }
    }
}
