using adduo.helper.entries;
using adduo.helper.entries.entry_validations;
using System;

namespace multiplixe.usuarios.usuario
{
    public class ApelidoEntryValidator : BaseEntryValidation<string>, IEntryValidation<string>
    {
        private Guid empresaId { get; }
        private Guid usuarioId { get; }
        private consulta.Servico servico { get; }

        public ApelidoEntryValidator(consulta.Servico servico, Guid empresaId)
        {
            this.empresaId = empresaId;
            this.servico = servico;
        }

        public ApelidoEntryValidator(consulta.Servico servico, Guid empresaId, Guid usuarioId) : this(servico, empresaId)
        {
            this.usuarioId = usuarioId;
        }

        public void Validate()
        {
            if (CanValidate())
            {
                var response = servico.Obter(new multiplixe.comum.dto.filtros.UsuarioFiltro
                {
                    EmpresaId = empresaId,
                    Apelido = this.entry.Value,
                });

                var apelidoJaCadastrado = response.Success;

                if(response.Success)
                {
                    apelidoJaCadastrado = response.Item.Id != usuarioId;
                }

                SetAlreadyStatus(!apelidoJaCadastrado);
            }
        }
    }
}
