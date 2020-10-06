using multiplixe.comum.dapper;
using multiplixe.comum.helper;
using System;

namespace multiplixe.classificador.transacao
{
    public class Repositorio
    {
        private DapperHelper dapperHelper { get; }

        public Repositorio(DapperHelper dapperHelper)
        {
            this.dapperHelper = dapperHelper;
        }

 
        public void Processar(Guid usuarioId)
        {
            dapperHelper
               .ResetParameter()
               .AddParameter("_usuarioId", usuarioId)
               .Update("saldo_processar");
        }

        public bool Debitar(Guid id, Guid usuarioId, string descricao, Guid parceiroId, string parceiroTransacaoId, int pontos)
        {
            var result = dapperHelper
               .ResetParameter()
               .AddParameter("_id", id)
               .AddParameter("_usuarioId", usuarioId)
               .AddParameter("_descricao", descricao)
               .AddParameter("_parceiroId", parceiroId)
               .AddParameter("_parceiroTransacaoId", parceiroTransacaoId)
               .AddParameter("_pontos", pontos)
               .AddParameter("_data", DateTimeHelper.Now())
               .Insert("saldo_debitar");

            return result.Equals(0);
        }

        /// <summary>
        /// Gera estorno de uma transação existente.
        /// </summary>
        /// <param name="novaTransacaoId">Id da nova transação de estorno</param>
        /// <param name="transacaoId">Id da transação que será estornada</param>
        /// <param name="parceiroId">Id do parceiro</param>
        /// <returns></returns>
        public bool Estornar(Guid novaTransacaoId, Guid transacaoId, Guid parceiroId)
        {
            var result = dapperHelper
               .ResetParameter()
               .AddParameter("_id", novaTransacaoId)
               .AddParameter("_transacaoId", transacaoId)
               .AddParameter("_parceiroId", parceiroId)
               .AddParameter("_data", DateTimeHelper.Now())
               .Insert("saldo_estornar");

            return result.Equals(0);
        }


        public results.Transacao Obter(Guid id, Guid parceiroId)
        {
            var result = dapperHelper
               .ResetParameter()
               .AddParameter("_parceiroId", parceiroId)
               .AddParameter("_transacaoId", id)
               .ExecuteWithOneResult<results.Transacao>("saldo_transacao_obter");

            return result;
        }

    }
}
