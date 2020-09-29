using multiplixe.comum.dapper;
using System;

namespace multiplixe.usuarios.usuario.results
{
    public class Usuario : BaseResult
    {
        public Guid Id { get; set; }
        public string Nome { get; set; }
        public string Apelido { get; set; }
        public string Email { get; set; }
        public Guid EmpresaId { get; set; }
        public DateTime DataCadastro { get; set; }
    }
}
