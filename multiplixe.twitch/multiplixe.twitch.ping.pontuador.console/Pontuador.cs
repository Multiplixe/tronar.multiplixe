using multiplixe.twitch.dto.eventos;
using multiplixe.twitch.ping.pontuador.console.regras;
using comum_dto = multiplixe.comum.dto;
using enums = multiplixe.comum.enums;
using corehelper = multiplixe.comum.helper;
using multiplixe.comum.interfaces.pontuador;

namespace multiplixe.twitch.ping.pontuador.console
{
    public class Pontuador : IPontuador<EventoPing>
    {
        public comum_dto.Ponto Pontuar(comum_dto.EnvelopeEvento<EventoPing> envelope)
        {
            var regra = Factory.RegraPontuacaoPadrao(envelope.Evento);

            var pontos = regra.Pontuar(envelope.Evento);

            return new comum_dto.Ponto(envelope.Id,
                envelope.UsuarioId,
                envelope.EmpresaId,
                envelope.Evento.PostId,
                envelope.Evento.PerfilId,
                envelope.DataEvento,
                corehelper.DateTimeHelper.Now(),
                enums.TipoEventoEnum.ping,
                pontos,
                enums.RedeSocialEnum.twitch);
        }

    }
}
