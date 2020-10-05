using System;

namespace multiplixe.classificador.interfaces
{
    public interface IConsultarParceiro
    {
        public bool VerificarExistencia(Guid parceiroId);
    }
}
