using coredto = multiplixe.comum.dto;
using coreinterface = multiplixe.comum.interfaces.pontuador;

namespace multiplixe.comum.pontuador
{
    public class RegraNulllable<T> : coreinterface.IRegraPontuador<T> where T  : coredto.EventoBase
    {
        public int Pontuar(T evento)
        {
            return 0;
        }
    }
}
