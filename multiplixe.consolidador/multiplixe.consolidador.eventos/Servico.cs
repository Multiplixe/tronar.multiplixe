using dto = multiplixe.comum.dto;

namespace multiplixe.consolidador.eventos
{
    public class Servico
    {
        private Repositorio repositorio { get; }

        public Servico(Repositorio repositorio)
        {
            this.repositorio = repositorio;
        }

        public void RegistrarEvento(dto.Ponto ponto)
        {
            repositorio.Registrar(ponto);
        }
    }
}
