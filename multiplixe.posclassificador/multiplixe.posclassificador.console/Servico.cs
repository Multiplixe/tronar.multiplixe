using multiplixe.central_rtdb.client;
using multiplixe.notificador.client;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using coredto = multiplixe.comum.dto;

namespace multiplixe.posclassificador.console
{
    public class Servico
    {
        private RTDBAtividadeClient rtdbAtividadeClient { get; }
        private NotificadorClient notificadorClient { get; }

        public Servico(RTDBAtividadeClient rtdbAtividadeClient,
           NotificadorClient notificadorClient)
        {
            this.rtdbAtividadeClient = rtdbAtividadeClient;
            this.notificadorClient = notificadorClient;
        }

        public void Processar(coredto.UsuarioParaProcessar usuarioParaProcessar)
        {
            try
            {
                var tasks = new List<Task>();

                var tarefas = new List<ITarefaPosClassificador>();
                tarefas.Add(new tarefas.Firebase(rtdbAtividadeClient));
                tarefas.Add(new tarefas.TwitchWhisper(notificadorClient));

                foreach (var tarefa in tarefas)
                {
                    tasks.Add(Task.Run(() =>
                    {
                        tarefa.Executar(usuarioParaProcessar);
                    }));
                }

                Task.WaitAll(tasks.ToArray());
            }
            catch(Exception ex)
            {
                //## TODO log
            }
        }
    }
}
