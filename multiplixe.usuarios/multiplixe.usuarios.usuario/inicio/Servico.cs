using System;
using System.Collections.Generic;
using System.Text;

namespace multiplixe.usuarios.usuario.inicio
{
    public class Servico
    {
        private Firebase firebase { get; }

        public Servico(Firebase firebase)
        {
            this.firebase = firebase;
        }

        public void Iniciar(Guid usuarioId)
        {
            firebase.Iniciar(usuarioId);
        }

        public void Deletar(Guid usuarioId)
        {
            firebase.Deletar(usuarioId);
        }
    }
}
