using adduo.helper.envelopes;
using Firebase.Auth;
using multiplixe.comum.dto;
using System;

namespace multiplixe.usuarios.externo.autenticacao
{
    public class Firebase
    {
        private Parametros parametros { get; }

        public Firebase(Parametros parametros)
        {
            this.parametros = parametros;
        }

        public void Autenticar(string email, string senha)
        {
            _ = new FirebaseAuthProvider(new FirebaseConfig(parametros.firebase_api_key))
            .SignInWithEmailAndPasswordAsync(email, senha)
            .GetAwaiter()
            .GetResult();
        }
    }
}
