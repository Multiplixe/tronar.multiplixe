using multiplixe.youtube.livehashtag.pontuador.console.regras;
using coredto = multiplixe.comum.dto;
using coreenums = multiplixe.comum.enums;
using corehelper = multiplixe.comum.helper;
using coreinterfaces = multiplixe.comum.interfaces;

namespace multiplixe.youtube.livehashtag.pontuador.console
{
    public class Pontuador : coreinterfaces.pontuador.IPontuador<dto.eventos.LiveHashtag>
    {
        public coredto.Ponto Pontuar(coredto.EnvelopeEvento<dto.eventos.LiveHashtag> envelope)
        {
            var regra = Factory.ObtemRegra(envelope.Evento);

            var pontos = regra.Pontuar(envelope.Evento);

            return new coredto.Ponto(envelope.Id, envelope.UsuarioId, envelope.EmpresaId, envelope.Evento.PostId, envelope.Evento.PerfilId, envelope.DataEvento, corehelper.DateTimeHelper.Now(), coreenums.TipoEventoEnum.hashtag,  pontos, coreenums.RedeSocialEnum.youtube);
        }
    }
}
