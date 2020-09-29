using multiplixe.api.dto.settings;
using multiplixe.api.interfaces;
using coreenums = multiplixe.comum.enums;

namespace multiplixe.api.log_eventos
{
    public class InstagramLogEventoService : LogEventoService<InstagramEventTest>
    {
        public InstagramLogEventoService(
             EmpresaSettings empresaSettings,
             ILogEventoSettings<InstagramEventTest> settings) : base(coreenums.RedeSocialEnum.instagram, empresaSettings, settings)
        {
        }
    }
}
