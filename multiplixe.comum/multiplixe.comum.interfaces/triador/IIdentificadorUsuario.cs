using adduo.helper.envelopes;

namespace multiplixe.comum.interfaces.triador
{
    public interface IIdentificadorUsuario<T> where T : dto.EventoBase
    {
        ResponseEnvelope<dto.Perfil> Identificar(dto.EnvelopeEvento<T> evento);
    }
}
