using multiplixe.twitter.dto.eventos;
using comum_dto = multiplixe.comum.dto;
using coreinterfaces = multiplixe.comum.interfaces;
using coreenums = multiplixe.comum.enums;

namespace multiplixe.twitter.reacao.triador
{
    public class Avaliador : coreinterfaces.triador.IAvaliadorDeEvento<EventoReacao>
    {
        private coreinterfaces.IRegistradorEventosConsultas<EventoReacao> consultarReacao { get; }

        public Avaliador(coreinterfaces.IRegistradorEventosConsultas<EventoReacao> consultarReacao)
        {
            this.consultarReacao = consultarReacao;
        }

        public bool Avaliar(comum_dto.EnvelopeEvento<EventoReacao> envelope)
        {
            var ultimaReacao = this.consultarReacao.ObterUltimaReacao(envelope);

            var valido = ultimaReacao.Tipo == coreenums.TipoEventoEnum.none ||
                         ultimaReacao.Tipo == coreenums.TipoEventoEnum.descurtida;

            return valido;
        }
    }
}
