using multiplixe.enfileirador.client;
using System;
using System.Linq;
using dto = multiplixe.comum.dto;

namespace multiplixe.classificador.classificacao
{
    public class Servico
    {
        private Repositorio repositorio { get; }
        private nivel.Servico nivelService { get; }
        private pontuacao.Servico pontuacaoService { get; }
        public EnfileiradorClient enfileirador { get; }

        public Servico(Repositorio repositorio,
            nivel.Servico nivelService,
            pontuacao.Servico pontuacaoService,
            EnfileiradorClient enfileirador)
        {
            this.repositorio = repositorio;
            this.nivelService = nivelService;
            this.pontuacaoService = pontuacaoService;
            this.enfileirador = enfileirador;
        }

        public void Processar(dto.UsuarioParaProcessar usuarioParaProcessar)
        {
            try
            {
                var usuarioId = usuarioParaProcessar.UsuarioId;

                var classificacao = repositorio.Obter(usuarioId);

                pontuacaoService.ProcessarIndividual(usuarioId);

                var total = pontuacaoService.CalcularTotal(usuarioId);

                var novoNivel = nivelService.Calcular(total, classificacao.EmpresaId);

                repositorio.Atualizar(usuarioId, novoNivel.Id, total);

                enfileirador.EnfileirarParaPosClassificador(usuarioParaProcessar);
            }
            catch (Exception ex)
            {
                if (usuarioParaProcessar.Tentativa == 10)
                {
                    //## TODO
                    throw ex;
                }
                else
                {
                    usuarioParaProcessar.Tentativa++;
                    enfileirador.EnfileirarParaClassificador(usuarioParaProcessar);
                }
            }
        }

        public dto.classificacao.Pontuacao ObterPontuacaoTotal(Guid usuarioId)
        {
            var result = repositorio.Obter(usuarioId);

            return new dto.classificacao.Pontuacao
            {
                Valor = result.Pontos
            };
        }

        public dto.classificacao.Classificacao Obter(Guid usuarioId)
        {
            var result = repositorio.Obter(usuarioId);

            var classificacao = new dto.classificacao.Classificacao();
            classificacao.Nivel = new dto.classificacao.Nivel
            {
                Nome = result.Nivel,
                Id = result.NivelId
            };

            classificacao.RedesSociais = pontuacaoService.Obter(usuarioId);

            var total = classificacao.RedesSociais.Sum(s => s.Pontos);

            classificacao.Pontuacao = new dto.classificacao.Pontuacao
            {
                Valor = total
            };

            return classificacao;
        }
    }
}
