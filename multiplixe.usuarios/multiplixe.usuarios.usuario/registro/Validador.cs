using adduo.helper.entries.entry_exceptions;
using System;
using System.Net;
using adduohelper = adduo.helper;
using dto = multiplixe.comum.dto;

namespace multiplixe.usuarios.usuario.registro
{
    public class Validador
    {
        private consulta.Servico servico { get; }

        public Validador(consulta.Servico servico)
        {
            this.servico = servico;
        }

        public void Validar(adduohelper.envelopes.RequestEnvelope<dto.entries.UsuarioRegistro> request)
        {
            if (request.Item.EmpresaId == Guid.Empty)
            {
                throw new Exception("EmpresaId vazio");
            }

            request.Item.Apelido.AddValidation(new ApelidoEntryValidator(servico, request.Item.EmpresaId));

            request.Item.Validate();

            if (request.Item.AnyIsInvalid())
            {
                var response = request.CreateResponse();

                response.HttpStatusCode = HttpStatusCode.BadRequest;

                throw new EntriesException<dto.entries.UsuarioRegistro>(response);
            }
        }
    }
}
