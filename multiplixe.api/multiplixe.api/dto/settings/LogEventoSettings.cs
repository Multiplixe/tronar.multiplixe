using multiplixe.api.interfaces;
using comum_dto =  multiplixe.comum.dto;

namespace multiplixe.api.dto.settings
{
    public class LogEventoSettings<T> : ILogEventoSettings<T> where T : comum_dto.EventoBase
    {
        public bool LogarEvento { get; set; }
        public bool LogarRequestInicial { get; set; }
    }
}
