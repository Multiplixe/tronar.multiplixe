using multiplixe.twitch.dto.eventos;
using comum_dto = multiplixe.comum.dto;
using coreinterfaces = multiplixe.comum.interfaces;

namespace multiplixe.twitch.ping.triador
{
    public class Avaliador : coreinterfaces.triador.IAvaliadorDeEvento<EventoPing>
    {
        public bool Avaliar(comum_dto.EnvelopeEvento<EventoPing> envelope)
        {
            var evento = envelope.Evento;

            var subtracao = evento.Atual - evento.Ultimo.AddMilliseconds(evento.PausaMilissegundos);

            var frequenciaSegundos = (evento.FrequenciaMinutos * 60);

            var segundosToleradosAposUltimoPing = frequenciaSegundos + evento.ToleranciaSegundos;

            var valido = subtracao.TotalSeconds >= frequenciaSegundos &&
                        subtracao.TotalSeconds <= segundosToleradosAposUltimoPing;

            return valido;
        }
    }
}
