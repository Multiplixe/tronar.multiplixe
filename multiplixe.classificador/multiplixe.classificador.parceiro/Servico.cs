using multiplixe.classificador.interfaces;
using System;

namespace multiplixe.classificador.parceiro
{
    public class Servico : IConsultarParceiro
    {
        private Repositorio repositorio { get; }

        public Servico(Repositorio repositorio )
        {
            this.repositorio = repositorio;
        }
 
        public results.Parceiro Obter(Guid usuarioId)
        {
            return repositorio.Obter(usuarioId);
        }

        public bool VerificarExistencia(Guid parceiroId)
        {
            var result = repositorio.Obter(parceiroId);

            return result is results.Parceiro;
        }
    }
}
