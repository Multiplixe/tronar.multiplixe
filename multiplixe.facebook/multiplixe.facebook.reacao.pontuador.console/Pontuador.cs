using multiplixe.facebook.dto.eventos;
using multiplixe.facebook.reacao.pontuador.console.regra;
using comum_dto = multiplixe.comum.dto;
using coreenums = multiplixe.comum.enums;
using corehelper = multiplixe.comum.helper;
using coreinterfaces = multiplixe.comum.interfaces;

namespace multiplixe.facebook.reacao.pontuador.console
{
    public class Pontuador : coreinterfaces.pontuador.IPontuador<Evento>
    {
        public comum_dto.Ponto Pontuar(comum_dto.EnvelopeEvento<Evento> envelope)
        {
            var regra = Factory.ObtemRegra(envelope.Evento);

            var pontos = regra.Pontuar(envelope.Evento);

            var eventoFacade = new EventoFacade(envelope.Evento);

            return new comum_dto.Ponto(envelope.Id, envelope.UsuarioId, envelope.EmpresaId, eventoFacade.PostId, eventoFacade.PerfilId, envelope.DataEvento, corehelper.DateTimeHelper.Now(), eventoFacade.Tipo, pontos, coreenums.RedeSocialEnum.facebook);
        }

    }
}
