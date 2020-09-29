using FirebaseAdmin.Auth;
using System;

namespace multiplixe.usuarios.usuario.exclusao
{
    public class Firebase
    {
        public void Deletar(Guid id)
        {
            FirebaseAuth
                .DefaultInstance
                .DeleteUserAsync(id.ToString())
                .GetAwaiter();

        }
    }
}
