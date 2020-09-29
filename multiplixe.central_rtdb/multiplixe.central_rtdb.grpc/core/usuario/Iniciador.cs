using Firebase.Database;

namespace multiplixe.central_rtdb.grpc.core.usuario
{
    public class Iniciador  
    {
        private FirebaseClient firebaseClient { get; }

        public Iniciador(FirebaseClient firebaseClient)
        {
            this.firebaseClient = firebaseClient;
        }

        public protos.Response Iniciar(string usuarioId)
        {
            var persistencia = new Persistencia(usuarioId, firebaseClient);
            return persistencia.Put(new string[] { "activities" }, new { score = 0, avatar = 0, connection = 0 });
        }

        public protos.Response Deletar(string usuarioId)
        {
            var persistencia = new Persistencia(usuarioId, firebaseClient);
            return persistencia.Delete(new string[] { });
        }
    }
}
