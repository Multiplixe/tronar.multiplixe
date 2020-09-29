using multiplixe.comum.dapper;
using System;

namespace multiplixe.usuarios.token.results
{
    public class Token : BaseResult
    {
        public Guid UsuarioId { get; set; }
        public string Valor { get; set; }
        public int Tipo { get; set; }
    }
}
