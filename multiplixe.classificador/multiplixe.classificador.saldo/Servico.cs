using System;

namespace multiplixe.classificador.saldo
{
    public class Servico
    {
        private Repositorio repositorio { get; }

        public Servico(Repositorio repositorio)
        {
            this.repositorio = repositorio;
        }
        public void Processar(Guid usuarioId)
        {
            repositorio.Processar(usuarioId);
        }
    }
}
