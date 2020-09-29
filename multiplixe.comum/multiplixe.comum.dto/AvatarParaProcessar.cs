using multiplixe.comum.dto.interfaces;
using System;

namespace multiplixe.comum.dto
{
    public class AvatarParaProcessar : IEmpresaID, IUsuarioID
    {
        public Guid EmpresaId { get; set; }
        
        public Guid UsuarioId { get; set; }

        public string Caminho { get; set; }

        public Avatar Avatar { get; set; }

        public AvatarParaProcessar()
        {
            Avatar = new Avatar();
        }
    }
}
