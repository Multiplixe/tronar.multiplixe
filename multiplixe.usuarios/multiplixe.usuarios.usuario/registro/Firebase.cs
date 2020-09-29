using FirebaseAdmin.Auth;
using System;
using adduohelper = adduo.helper;
using dto = multiplixe.comum.dto;

namespace multiplixe.usuarios.usuario.registro
{
    public class Firebase
    {
        private FirebaseParserError parserError { get; }

        public Firebase(FirebaseParserError parserError)
        {
            this.parserError = parserError;
        }

        public void Registrar(adduohelper.envelopes.RequestEnvelope<dto.entries.UsuarioRegistro> request)
        {
            var usuario = request.Item;

            try
            {
                FirebaseAuth
                    .DefaultInstance
                    .CreateUserAsync(new UserRecordArgs
                    {
                        DisplayName = usuario.Nome.Value,
                        Email = usuario.Email.Value,
                        Password = usuario.Senha.Value,
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
