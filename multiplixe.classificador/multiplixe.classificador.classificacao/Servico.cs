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
        private usuario.Servico usuarioService { get; }
        private transacao.Saldo saldoService { get; }
        public EnfileiradorClient enfileirador { get; }

        public Servico(Repositorio repositorio,
            nivel.Servico nivelService,
            pontuacao.Servico pontuacaoService,
            usuario.Servico usuarioService,
            transacao.Saldo saldoService,
            EnfileiradorClient enfileirador)
        {
            this.repositorio = repositorio;
            this.nivelService = nivelService;
            this.pontuacaoService = pontuacaoService;
            this.usuarioService = usuarioService;
            this.saldoService = saldoService;
            this.enfileirador = enfileirador;
        }

        public void Processar(dto.UsuarioParaProcessar usuarioParaProcessar)
        {
            try
            {
                var usuarioId = usuarioParaProcessar.UsuarioId;

                pontuacaoService.ProcessarIndividual(usuarioId);

                pontuacaoService.ProcessarTotal(usuarioId);

                var classificacao = repositorio.Obter(usuarioId);

                nivelService.Processar(usuarioId, classificacao.Pontos, classificacao.EmpresaId);

                saldoService.Processar(usuarioId);

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

            classificacao.Pontuacao = new dto.classificacao.Pontuacao
            {
                Valor = result.Pontos
            };

            classificacao.Saldo = new dto.classificacao.Saldo
            {
                Valor = result.Saldo
            };

            return classificacao;
        }
    }
}
