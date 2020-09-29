using multiplixe.central_rtdb.client;
using System;

namespace multiplixe.usuarios.usuario.inicio
{
    public class Firebase
    {
        private RTDBUsuarioClient client { get; }

        public Firebase(RTDBUsuarioClient client)
        {
            this.client = client;
        }

        public void Iniciar(Guid usuarioId)
        {
            client.Iniciar(usuarioId);
        }

        public void Deletar(Guid usuarioId)
        {
            client.Deletar(usuarioId);
        }
    }
}
