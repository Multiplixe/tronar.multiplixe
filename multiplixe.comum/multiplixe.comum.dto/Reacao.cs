using multiplixe.comum.enums;
using System;

namespace multiplixe.comum.dto
{
    public class Reacao
    {
        public Guid EventoId { get; set; }
        public Guid UsuarioId { get; set; }
        public string PostId { get; set; }
        public TipoEventoEnum Tipo { get; set; }
    }
}
