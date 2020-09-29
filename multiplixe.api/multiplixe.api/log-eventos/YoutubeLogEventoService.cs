using multiplixe.api.dto.settings;
using multiplixe.api.interfaces;
using coreenums = multiplixe.comum.enums;

namespace multiplixe.api.log_eventos
{
    public class YoutubeLogEventoService : LogEventoService<YoutubeEventoTest>
    {
        public YoutubeLogEventoService(
            EmpresaSettings empresaSettings,
            ILogEventoSettings<YoutubeEventoTest> settings) : base(coreenums.RedeSocialEnum.youtube, empresaSettings, settings)
        {
        }
    }
}
