using comum_dto = multiplixe.comum.dto;
using coreinterfaces = multiplixe.comum.interfaces;
using System;
using multiplixe.facebook.dto.eventos;
using multiplixe.facebook.dto.enums;
using multiplixe.facebook.reacao.triador;
using multiplixe.enfileirador.client;
using multiplixe.comum.triador;

namespace multiplixe.facebook.triador.console
{
    public class Triador : coreinterfaces.triador.ITriador<Evento>
    {
        private coreinterfaces.triador.IRegistradorEventoTriagem<Evento> registradorReacao { get; }
        private IAvaliadorCurtida avaliadorCurtida { get; }
        private IAvaliadorDescurtida avaliadorDescurtida { get; }
        private EnfileiradorClient enfileiradorClient { get; }

        public Triador(
            coreinterfaces.triador.IRegistradorEventoTriagem<Evento> registradorReacao,
            IAvaliadorCurtida avaliadorCurtida,
            IAvaliadorDescurtida avaliadorDescurtida,
            EnfileiradorClient enfileiradorClient)
        {
            this.registradorReacao = registradorReacao;
            this.avaliadorCurtida = avaliadorCurtida;
            this.avaliadorDescurtida = avaliadorDescurtida;
            this.enfileiradorClient = enfileiradorClient;
        }


        public coreinterfaces.triador.IEventoTriado Triar(comum_dto.EnvelopeEvento<Evento> envelope)
        {
            coreinterfaces.triador.IEventoTriado eventoTriado = new EventoTriadoNullable();

            var eventoFacade = new EventoFacade(envelope.Evento);

            if (eventoFacade.Value.item == EventoItemEnum.reaction.ToString())
            {
                if (eventoFacade.Value.verb == EventoVerbEnum.add.ToString())
                {
                    eventoTriado = new Reacao(registradorReacao, avaliadorCurtida, enfileiradorClient, envelope);
                }
                else if (eventoFacade.Value.verb == EventoVerbEnum.remove.ToString())
                {
                    eventoTriado = new Reacao(registradorReacao, avaliadorDescurtida, enfileiradorClient, envelope);
                }
                else
                {
                    throw new Exception($"Facebook: Tipo de reação não identificada {eventoFacade.Value.verb}");
                }
            }

            return eventoTriado;
        }
    }
}
