using System;
using comum_dto = multiplixe.comum.dto;

namespace multiplixe.twitch.dto.eventos
{
    public class EventoPing : comum_dto.EventoBase
    {
        public string PerfilId { get; set; }
        public string PostId { get; set; }
        public int ToleranciaSegundos { get; set; }
        public int FrequenciaMinutos { get; set; }
        public int PausaMilissegundos { get; set; }
        public DateTime Ultimo { get; set; }
        public DateTime Atual { get; set; }
    }
}
