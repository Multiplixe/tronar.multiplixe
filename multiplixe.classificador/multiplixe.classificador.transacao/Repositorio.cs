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
               .Insert("saldo_registrar_debito");

            return result.Equals(0);
        }
    }
}
