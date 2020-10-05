using System;

namespace multiplixe.classificador.transacao
{
    public class Saldo
    {
        private Repositorio repositorio { get; }
  
        public Saldo(
            Repositorio repositorio)
        {
            this.repositorio = repositorio;
        }
        public void Processar(Guid usuarioId)
        {
            repositorio.Processar(usuarioId);
        }
    }
}
