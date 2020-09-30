using dto = multiplixe.comum.dto;

namespace multiplixe.consolidador.saldo
{
    public class Servico
    {
        private Repositorio repositorio { get; }

        public Servico(Repositorio repositorio)
        {
            this.repositorio = repositorio;
        }

        public void RegistrarSaldo(dto.Ponto ponto)
        {
            repositorio.Registrar(ponto);
        }
    }
}
