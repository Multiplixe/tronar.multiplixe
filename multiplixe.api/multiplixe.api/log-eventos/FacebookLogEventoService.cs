using coreenums = multiplixe.comum.enums;
using facebook_dto =  multiplixe.facebook.dto;
using multiplixe.api.dto.settings;
using multiplixe.api.interfaces;

namespace multiplixe.api.log_eventos
{
    public class FacebookLogEventoService : LogEventoService<facebook_dto.eventos.Evento>
    {
        public FacebookLogEventoService(
             EmpresaSettings empresaSettings,
             ILogEventoSettings<facebook_dto.eventos.Evento> settings) : base(coreenums.RedeSocialEnum.facebook, empresaSettings, settings)
        {
        }
    }
}
