using System;
using System.Text.Json.Serialization;
using coreinterfaces = multiplixe.comum.dto.interfaces;

namespace multiplixe.comum.dto.entries
{
    public class AuthAPI : adduo.helper.entries.BaseEntries, coreinterfaces.IEmpresaID
    {
        [JsonPropertyName("email")]
        public adduo.helper.entries.Email Email { get; set; }

        [JsonPropertyName("password")]
        public adduo.helper.entries.Password Senha { get; set; }

        [JsonIgnore]
        public Guid EmpresaId { get; set; }

        [JsonPropertyName("token")]
        public string Token { get; set; }

        public AuthAPI()
        {
            InitEntries();
        }

        public override void SetEntries()
        {
            AddEntry(Email);
            AddEntry(Senha);
        }

        public override void AddValidators()
        {
        }

        public override void InitEntries()
        {
            Email = new adduo.helper.entries.Email();
            Senha = new adduo.helper.entries.Password();
        }
    }
}
