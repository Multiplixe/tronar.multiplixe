using comum_dto =  multiplixe.comum.dto;

namespace multiplixe.api.interfaces
{
    public interface ILogEventoSettings<T> where T : comum_dto.EventoBase
    {
        bool LogarEvento { get; set; }
        bool LogarRequestInicial { get; set; }
    }
}
