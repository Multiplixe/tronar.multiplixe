using multiplixe.comum.dto.interfaces;
using System;
using System.Text.Json.Serialization;
using adduoform = adduo.helper;

namespace multiplixe.comum.dto.entries
{
    public class UsuarioRegistro : adduoform.entries.BaseEntries, IEmpresaID, IUsuarioID
    {
        [JsonPropertyName("name")]
        public adduoform.entries.Name Nome { get; set; }

        [JsonPropertyName("nickname")]
        public adduoform.entries.String32 Apelido { get; set; }

        [JsonPropertyName("email")]
        public adduoform.entries.Email Email { get; set; }

        [JsonPropertyName("password")]
        public adduoform.entries.Password Senha { get; set; }

        [JsonIgnore]
        public Guid EmpresaId { get; set; }

        [JsonIgnore]
        public Guid UsuarioId { get; set; }

        public UsuarioRegistro()
        {
            InitEntries();
        }

        public UsuarioRegistro(string nome, string apelido, string email, string senha, Guid empresaId)
        {
            InitEntries();

            Nome.Value = nome;
            Apelido.Value = apelido;
            Email.Value = email;
            Senha.Value = senha;
            EmpresaId = empresaId;
        }

        public override void InitEntries()
        {
            Nome = new adduoform.entries.Name();
            Apelido = new adduoform.entries.String32();
            Email = new adduoform.entries.Email();
            Senha = new adduoform.entries.Password();
        }

        public override void SetEntries()
        {
            AddEntry(Nome);
            AddEntry(Apelido);
            AddEntry(Email);
            AddEntry(Senha);
        }
    }
}
