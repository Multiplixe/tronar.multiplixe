using multiplixe.comum.dapper;
using System;

namespace multiplixe.usuarios.perfil.results
{
    public class Perfil : BaseResult
    {
        public Guid EmpresaId { get; set; }
        public Guid UsuarioId { get; set; }
        public string PerfilId { get; set; }
        public string Nome { get; set; }
        public int RedeSocialId { get; set; }
        public bool Ativo { get; set; }
        public DateTime DataCadastro { get; set; }
        public string Token { get; set; }
        public string ImagemUrl { get; set; }
        public string Login { get; set; }
    }
}
