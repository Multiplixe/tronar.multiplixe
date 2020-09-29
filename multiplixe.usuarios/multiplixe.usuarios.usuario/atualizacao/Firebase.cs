using FirebaseAdmin.Auth;
using System;
using adduohelper = adduo.helper;
using dto = multiplixe.comum.dto;

namespace multiplixe.usuarios.usuario.atualizacao
{
    public class Firebase
    {
        private FirebaseParserError parserError { get; }

        public Firebase(FirebaseParserError parserError)
        {
            this.parserError = parserError;
        }

        public void Atualizar(adduohelper.envelopes.RequestEnvelope<dto.entries.UsuarioAtualizacao> request)
        {
            var usuario = request.Item;

            try
            {
                FirebaseAuth
                    .DefaultInstance
                    .UpdateUserAsync(new UserRecordArgs
                    {
                        DisplayName = usuario.Nome.Value,
                        Email = usuario.Email.Value,
                        Uid = usuario.UsuarioId.ToString(),
                        Disabled = false
                    }).GetAwaiter()
                    .GetResult();
            }
            catch (Exception e)
            {
                parserError.Throw(e, request);
            }
        }
    }
}
