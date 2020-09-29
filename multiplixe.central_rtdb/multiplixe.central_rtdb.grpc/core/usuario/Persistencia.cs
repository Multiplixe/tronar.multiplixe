using Firebase.Database;
using Firebase.Database.Query;
using System;
using raiz = multiplixe.central_rtdb.grpc.core;

namespace multiplixe.central_rtdb.grpc.core.usuario
{
    public class Persistencia : raiz.Persistencia
    {
        private string usuarioId { get; }

        public Persistencia(string usuarioId, FirebaseClient firebaseClient) : base(firebaseClient)
        {
            this.usuarioId = usuarioId;
        }

        public override ChildQuery ObterChildQuery(string[] paths)
        {
            var query = firebaseClient
               .Child("users")
               .Child(usuarioId.ToString());

            foreach (var path in paths)
            {
                query = query.Child(path);
            }

            return query;
        }
    }
}
