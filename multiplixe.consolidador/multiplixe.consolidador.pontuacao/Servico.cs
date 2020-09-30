using dto = multiplixe.comum.dto;

namespace multiplixe.consolidador.pontuacao
{
    public class Servico
    {
        private Repositorio repositorio { get; }

        public Servico(Repositorio repositorio)
        {
            this.repositorio = repositorio;
        }

        public void RegistrarPontuacao(dto.Ponto ponto)
        {
            repositorio.Registrar(ponto);
        }
    }
}
