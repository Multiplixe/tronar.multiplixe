using multiplixe.comum.dapper;
using System;

namespace multiplixe.compartilhador.post.results
{
    public class Post : BaseResult
    {
        public int Id { get; set; }
        public int UsuarioId { get; set; }
        public int CompartilhamentoId { get; set; }
        public DateTime Atualizacao { get; set; }
        public int RedeSocialId { get; set; }
        public string Identificador { get; set; }
    }
}
