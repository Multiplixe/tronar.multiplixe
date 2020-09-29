using Firebase.Database;
using Newtonsoft.Json;

namespace multiplixe.central_rtdb.grpc.core.comum
{
    public class Atividade
    {
        private FirebaseClient firebaseClient { get; }

        public Atividade(FirebaseClient firebaseClient)
        {
            this.firebaseClient = firebaseClient;
        }

        public protos.Response Registrar(protos.AtividadeRequest atividade)
        {
            var valor = JsonConvert.DeserializeObject(atividade.Json);

            var persistencia = new Persistencia(firebaseClient);

            return persistencia.Put(new string[] { "activities", atividade.Nome }, valor);
        }
    }
}
