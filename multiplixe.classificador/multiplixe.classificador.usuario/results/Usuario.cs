using multiplixe.comum.dapper;
using System;

namespace multiplixe.classificador.usuario.results
{
    public class Usuario : BaseResult
    {
        public Guid Id { get; set; }
        public Guid EmpresaId { get; set; }
    }
}
