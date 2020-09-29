using multiplixe.classificador.client;
using System;

namespace multiplixe.usuarios.usuario.exclusao
{
    public class Servico
    {
        private Repositorio repositorio { get; }
        private Firebase firebase { get; }
        private exclusao.Firebase iniciador { get; }
        private ClassificadorUsuarioClient classificador { get; }

        public Servico(Repositorio repositorio, Firebase firebase, exclusao.Firebase iniciador, ClassificadorUsuarioClient classificador)
        {
            this.repositorio = repositorio;
            this.firebase = firebase;
            this.iniciador = iniciador;
            this.classificador = classificador;
        }

        public void Rollback(Guid id)
        {
            repositorio.Deletar(id);
            firebase.Deletar(id);
            iniciador.Deletar(id);
            classificador.Deletar(id);
        }

    }
}
