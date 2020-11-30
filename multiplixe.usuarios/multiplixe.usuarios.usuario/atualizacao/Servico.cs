using adduo.helper.entries.entry_exceptions;
using System;
using System.Net;
using adduohelper = adduo.helper;
using dto = multiplixe.comum.dto;

namespace multiplixe.usuarios.usuario.atualizacao
{
    public class Servico
    {
        private Validador validador { get; }
        private Repositorio repositorio { get; }
        private Firebase registroFirebase { get; }

        public Servico(
            Validador validador,
            Repositorio repositorio,
            Firebase firebase)

        {
            this.validador = validador;
            this.repositorio = repositorio;
            this.registroFirebase = firebase;
        }

        public adduohelper.envelopes.ResponseEnvelope<dto.entries.UsuarioAtualizacao> Atualizar(adduohelper.envelopes.RequestEnvelope<dto.entries.UsuarioAtualizacao> request)
        {
            var response = request.CreateResponse();

            try
            {
                validador.Validar(request);

                registroFirebase.Atualizar(request);

                var usuario = new dto.Usuario
                {
                    Id = request.Item.UsuarioId,
                    EmpresaId = request.Item.EmpresaId,
                    Nome = request.Item.Nome.Value,
                    Apelido = request.Item.Apelido.Value,
                    Email = request.Item.Email.Value
                };

                repositorio.Atualizar(usuario);
            }
            catch (EntriesException<dto.entries.UsuarioAtualizacao> entriesEx)
            {
                // ## TODO LOG

                response = entriesEx.ResponseEnvelope;
            }
            catch (Exception ex)
            {
                // ## TODO LOG

                response.HttpStatusCode = HttpStatusCode.InternalServerError;
            }

            return response;
        }

        public void UltimoAcesso(dto.UsuarioUltimoAcesso usuarioUltimoAcesso)
        {
            repositorio.UltimoAcesso(usuarioUltimoAcesso);
        }

    }
}
