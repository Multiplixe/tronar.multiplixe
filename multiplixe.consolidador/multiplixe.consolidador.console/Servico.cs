using multiplixe.comum.dto;
using multiplixe.enfileirador.client;
using System;
using System.Collections.Generic;
using System.Text;

namespace multiplixe.consolidador.console
{
    public class Servico
    {
        private eventos.Servico eventosService { get; }
        private pontuacao.Servico pontuacaoService { get; }
        private saldo.Servico saldoService { get; }
        private Repositorio repositorio { get; }
        private EnfileiradorClient enfileiradorClient { get; }

        public Servico(
            eventos.Servico eventosService, 
            pontuacao.Servico pontuacaoService, 
            saldo.Servico saldoService,
            Repositorio repositorio,
            EnfileiradorClient enfileiradorClient)
        {
            this.eventosService = eventosService;
            this.pontuacaoService = pontuacaoService;
            this.saldoService = saldoService;
            this.repositorio = repositorio;
            this.enfileiradorClient = enfileiradorClient;
        }

        public void Processar(Ponto ponto)
        {
            eventosService.RegistrarEvento(ponto);
            pontuacaoService.RegistrarPontuacao(ponto);
            saldoService.RegistrarSaldo(ponto);

            var processar = new UsuarioParaProcessar(ponto.UsuarioId);

            enfileiradorClient.EnfileirarParaClassificador(processar);
        }

        public void Rollback(Ponto ponto)
        {
            repositorio.Rollback(ponto);
        }
    }
}
