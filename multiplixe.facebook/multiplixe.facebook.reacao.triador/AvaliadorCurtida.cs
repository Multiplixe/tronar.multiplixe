using multiplixe.facebook.dto.eventos;
using comum_dto = multiplixe.comum.dto;
using coreenums = multiplixe.comum.enums;
using coreinterfaces = multiplixe.comum.interfaces;

namespace multiplixe.facebook.reacao.triador
{
    public class AvaliadorCurtida : IAvaliadorCurtida
    {
        public coreinterfaces.IRegistradorEventosConsultas<Evento> consultarReacao { get; }
        public AvaliadorCurtida(coreinterfaces.IRegistradorEventosConsultas<Evento> consultarReacao)
        {
            this.consultarReacao = consultarReacao;
        }

        public bool Avaliar(comum_dto.EnvelopeEvento<Evento> envelope)
        {
            var ultimaReacao = this.consultarReacao.ObterUltimaReacao(envelope);

            var podeProcessar = ultimaReacao.Tipo == coreenums.TipoEventoEnum.none ||
                         ultimaReacao.Tipo == coreenums.TipoEventoEnum.descurtida;

            return podeProcessar;
        }
    }
}
