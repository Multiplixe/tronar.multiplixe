using multiplixe.enfileirador.client;
using multiplixe.twitter.dto.eventos;
using multiplixe.twitter.reacao.triador.evento_triado;
using comum_dto = multiplixe.comum.dto;
using coreinterfaces = multiplixe.comum.interfaces;

namespace multiplixe.twitter.reacao.triador
{
    public class Triador : coreinterfaces.triador.ITriador<EventoReacao>
    {
        private coreinterfaces.triador.IRegistradorEventoTriagem<EventoReacao> registradorReacao { get; }
        private coreinterfaces.triador.IAvaliadorDeEvento<EventoReacao> avaliadorDeEvento { get; }
        private EnfileiradorClient enfileirador { get; }
        private coreinterfaces.IRegistradorEventosConsultas<EventoReacao> consultarReacao { get; }

        public Triador(
             coreinterfaces.triador.IAvaliadorDeEvento<EventoReacao> avaliadorDeEvento,
             coreinterfaces.triador.IRegistradorEventoTriagem<EventoReacao> registradorReacao,
           coreinterfaces.IRegistradorEventosConsultas<EventoReacao> consultarReacao,
            EnfileiradorClient enfileirador)
        {
            this.avaliadorDeEvento = avaliadorDeEvento;
            this.registradorReacao = registradorReacao;
            this.enfileirador = enfileirador;
            this.consultarReacao = consultarReacao;
        }

        public coreinterfaces.triador.IEventoTriado Triar(comum_dto.EnvelopeEvento<EventoReacao> envelopoe)
        {
            return new ReacaoCurtida(avaliadorDeEvento, registradorReacao, enfileirador, envelopoe);
        }
    }
}
