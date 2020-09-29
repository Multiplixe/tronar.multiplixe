using System;

namespace multiplixe.comum.dto
{
    public class RedefinidorDeSenhaToken
    {
        public Guid Token { get; set; }
        public Guid UsuarioId { get; set; }
        public bool FoiUtilizado { get; set; }
        public DateTime DataSolicitacao { get; set; }
    }
}
