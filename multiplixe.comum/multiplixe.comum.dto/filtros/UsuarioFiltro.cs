using multiplixe.comum.dto.interfaces;
using System;
using System.Collections.Generic;

namespace multiplixe.comum.dto.filtros
{
    public class UsuarioFiltro : IEmpresaID
    {
        public string Email { get; set; }
        public string Apelido { get; set; }
        public Guid UsuarioId { get; set; }
        public Guid EmpresaId { get; set; }
        public List<Guid> UsuariosIdLista { get; set; }
        public string UsuariosIdString { get { return string.Join(',', UsuariosIdLista); } }

        public UsuarioFiltro()
        {
            UsuariosIdLista = new List<Guid>();
        }
    }
}
