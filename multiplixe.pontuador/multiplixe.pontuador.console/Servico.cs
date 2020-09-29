using multiplixe.enfileirador.client;
using coredto = multiplixe.comum.dto;

namespace multiplixe.pontuador.console
{
    public class Servico
    {
        private Repositorio repositorio { get; }
        private EnfileiradorClient enfileirador { get; }

        public Servico(Repositorio repositorio, EnfileiradorClient enfileirador)
        {
            this.repositorio = repositorio;
            this.enfileirador = enfileirador;
        }

        public void RegistrarExtrato(coredto.Ponto ponto)
        {
            repositorio.Registrar(ponto);

            var processar = new coredto.UsuarioParaProcessar(ponto.UsuarioId);

            enfileirador.EnfileirarParaClassificador(processar);
        }
    }
}
