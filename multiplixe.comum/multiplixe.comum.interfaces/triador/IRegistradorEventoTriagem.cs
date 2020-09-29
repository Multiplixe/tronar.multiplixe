using multiplixe.comum.dto;

namespace multiplixe.comum.interfaces.triador
{
    public interface IRegistradorEventoTriagem<T> where T : dto.EventoBase 
    {
        void RegistrarEvento(EnvelopeEvento<T> envelope);


    }
}