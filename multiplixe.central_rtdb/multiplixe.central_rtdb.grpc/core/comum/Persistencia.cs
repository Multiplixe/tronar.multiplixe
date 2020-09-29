using Firebase.Database;
using Firebase.Database.Query;
using raiz = multiplixe.central_rtdb.grpc.core;

namespace multiplixe.central_rtdb.grpc.core.comum
{
    public class Persistencia : raiz.Persistencia
    {
        public Persistencia(FirebaseClient firebaseClient) : base(firebaseClient)
        {
        }

        public override ChildQuery ObterChildQuery(string[] paths)
        {
            var query = firebaseClient
               .Child("common");

            foreach (var path in paths)
            {
                query = query.Child(path);
            }

            return query;
        }
    }
}
