using multiplixe.comum.dto;
using multiplixe.notificador.client;

namespace multiplixe.posclassificador.console.tarefas
{
    public class TwitchWhisper : ITarefaPosClassificador
    {
        private NotificadorClient client { get; }

        public TwitchWhisper(NotificadorClient client)
        {
            this.client = client;
        }

        public void Executar(UsuarioParaProcessar usuarioParaProcessars)
        {
            client.TwitchPubSub(usuarioParaProcessars);
        }
    }
}
