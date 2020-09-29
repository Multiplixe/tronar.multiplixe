using multiplixe.api.dto.settings;
using multiplixe.api.interfaces;
using multiplixe.twitter.dto.eventos;
using coreenums = multiplixe.comum.enums;

namespace multiplixe.api.log_eventos
{
    public class TwitterLogEventoService : LogEventoService<Evento>
    {
        public TwitterLogEventoService(
            EmpresaSettings empresaSettings,
            ILogEventoSettings<Evento> settings) : base(coreenums.RedeSocialEnum.twitter, empresaSettings, settings)
        {
        }
    }
}
