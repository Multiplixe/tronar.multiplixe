using multiplixe.central_rtdb.client;
using coredto = multiplixe.comum.dto;

namespace multiplixe.posclassificador.console.tarefas
{
    public class Firebase : ITarefaPosClassificador
    {
        private RTDBAtividadeClient rtdbAtividadeClient { get; }

        public Firebase(RTDBAtividadeClient rtdbAtividadeClient)
        {
            this.rtdbAtividadeClient = rtdbAtividadeClient;
        }

        public void Executar(coredto.UsuarioParaProcessar usuarioParaProcessar)
        {
            rtdbAtividadeClient.RegistrarClassificacao(usuarioParaProcessar.UsuarioId);
        }
    }
}
