using multiplixe.comum.enums;
using System;

namespace multiplixe.usuarios.perfil
{
    public class Filtro
    {
        public Guid UsuarioId { get; set; }
        public Guid EmpresaId { get; set; }
        public string PerfilId { get; set; }
        public RedeSocialEnum RedeSocial { get; set; }
    }
}
