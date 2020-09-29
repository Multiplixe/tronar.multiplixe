using System;

namespace multiplixe.comum.dto.entries
{
    public class UsuarioAtualizacao : UsuarioRegistro
    {
        public UsuarioAtualizacao() : base()
        {

        }

        public UsuarioAtualizacao(Guid id, Guid empresaId, string nome, string apelido, string email)
        {
            InitEntries();

            UsuarioId = id;
            EmpresaId = empresaId;
            Nome.Value = nome;
            Apelido.Value = apelido;
            Email.Value = email;
        }


        public override void SetEntries()
        {
            AddEntry(Nome);
            AddEntry(Apelido);
            AddEntry(Email);
        }
    }
}
