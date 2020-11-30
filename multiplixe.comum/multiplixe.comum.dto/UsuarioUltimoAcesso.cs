using multiplixe.comum.dto.interfaces;
using System;

namespace multiplixe.comum.dto
{
    public class UsuarioUltimoAcesso : IUsuarioID
    {
        public Guid UsuarioId { get; set; }
        public DateTime Acesso { get; set; }
    }
}
