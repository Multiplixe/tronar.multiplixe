using System;

namespace multiplixe.comum.dto
{
    public class Login
    {
        public Guid EmpresaId { get; set; }
        public string Email { get; set; }
        public string Senha { get; set; }

        public Login()
        {

        }

        public Login(Guid empresaId, string email, string senha)
        {
            EmpresaId = empresaId;
            Email = email;
            Senha = senha;
        }

    }
}
