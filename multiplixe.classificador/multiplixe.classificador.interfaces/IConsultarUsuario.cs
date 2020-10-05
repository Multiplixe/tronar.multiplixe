using System;

namespace multiplixe.classificador.interfaces
{
    public interface IConsultarUsuario
    {
        public bool VerificarExistencia(Guid usuarioId);
    }
}
