using adduo.helper.envelopes;
using Firebase.Auth;
using Microsoft.IdentityModel.Tokens;
using multiplixe.comum.dto;
using System;
using System.Collections.Generic;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;
using comum_dto = multiplixe.comum.dto;

namespace multiplixe.usuarios.externo.autenticacao
{
    public class Servico
    {
        private Firebase firebase { get; }
        private Parametros parametros { get; }

        private usuario.consulta.Servico consultaService { get; }

        public Servico(
            Firebase firebase,
            Parametros parametros,
            usuario.consulta.Servico consultaService)
        {
            this.firebase = firebase;
            this.consultaService = consultaService;
            this.parametros = parametros;
        }

        public ResponseEnvelope<comum_dto.externo.AutenticacaoResponse> Autenticar(comum_dto.externo.AutenticacaoRequest request)
        {
            var response = new ResponseEnvelope<comum_dto.externo.AutenticacaoResponse>();

            try
            {
                firebase.Autenticar(request.Email, request.Senha);

                var userResponse = consultaService.Obter(
                    new comum_dto.filtros.UsuarioFiltro
                    {
                        EmpresaId = request.EmpresaId,
                        Email = request.Email
                    });

                response.HttpStatusCode = userResponse.HttpStatusCode;

                if (userResponse.Success)
                {
                    var token = GerarToken(userResponse.Item.Id);

                    response.Item = new comum_dto.externo.AutenticacaoResponse
                    {
                        Token = token,
                        Nome = userResponse.Item.Nome,
                        Apelido = userResponse.Item.Apelido
                    };
                }
            }
            catch (FirebaseAuthException fex)
            {
                var retornoNotFound = new List<AuthErrorReason>
                {
                    AuthErrorReason.UnknownEmailAddress,
                    AuthErrorReason.UserNotFound,
                };

                response.HttpStatusCode = System.Net.HttpStatusCode.BadRequest;

                response.Error.Messages.Add(fex.Reason.ToString());

                if (retornoNotFound.Contains(fex.Reason))
                {
                    response.HttpStatusCode = System.Net.HttpStatusCode.NotFound;
                }
            }
            catch (Exception ex)
            {
                // ## TODO log
                response.HttpStatusCode = System.Net.HttpStatusCode.InternalServerError;
                response.Error.Messages.Add(ex.Message);
            }

            return response;

        }

        public string GerarToken(Guid Id)
        {
            var tokenHandler = new JwtSecurityTokenHandler();
            var key = Encoding.ASCII.GetBytes(this.parametros.external_secret_key);
            var tokenDescriptor = new SecurityTokenDescriptor
            {
                Subject = new ClaimsIdentity(new Claim[]
                {
                    new Claim("user_id", Id.ToString()),
                }),
                Audience = "multiplixe-external",
                Issuer = "multiplixe",
                Expires = DateTime.UtcNow.AddHours(2),
                SigningCredentials = new SigningCredentials(new SymmetricSecurityKey(key), SecurityAlgorithms.HmacSha256Signature)
            };
            var token = tokenHandler.CreateToken(tokenDescriptor);
            return tokenHandler.WriteToken(token);
        }

    }
}
