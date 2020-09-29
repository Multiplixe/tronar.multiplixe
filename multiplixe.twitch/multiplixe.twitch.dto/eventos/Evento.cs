using comum_dto = multiplixe.comum.dto;
using enums = multiplixe.comum.enums;

namespace multiplixe.twitch.dto.eventos
{
    public class Evento : comum_dto.EventoBase
    {
        public enums.TipoEventoEnum TipoEvento { get; set; }
        public string PerfilId { get; set; }

        public EventoPing Ping { get; set; }

    }
}
