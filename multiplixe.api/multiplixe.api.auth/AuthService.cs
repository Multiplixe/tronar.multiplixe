//using System;
//using System.Net;
//using adduohelper = adduo.helper.envelopes;
//using comum_dto =  multiplixe.comum.dto;
//using usuario = multiplixe.usuarios.client;

//namespace multiplixe.api.auth
//{
//    public class AuthService
//    {
//        private TokenService tokenService { get; }
//        public usuario.UsuarioClient usuarioClient { get; }

//        public AuthService(
//            TokenService tokenService,
//            usuario.UsuarioClient usuarioClient)
//        {
//            this.tokenService = tokenService;
//            this.usuarioClient = usuarioClient;
//        }

//        public adduohelper.ResponseEnvelope<comum_dto.entries.AuthAPI> Autenticar(adduohelper.RequestEnvelope<comum_dto.entries.AuthAPI> request)
//        {
//            request.Item.Validate();

//            var response = request.CreateResponse();

//            if (request.Item.AllAreValid())
//            {
//                var usuario = ObterUsuario(request.Item);

//                response = Autenticar(request, usuario);
//            }
//            else if (response.Item.AnyIsInvalid())
//            {
//                response.HttpStatusCode = HttpStatusCode.BadRequest;
//            }

//            response.Item.Senha.Value = string.Empty;
//            response.Item.EmpresaId = Guid.Empty;

//            return response;
//        }

//        private dto.Usuario ObterUsuario(comum_dto.entries.AuthAPI authProperty)
//        {
//            var comum_dto =  new dto.Login(authProperty.EmpresaId, authProperty.Email.Value, authProperty.Senha.Hash);

//            var request = new adduohelper.RequestEnvelope<dto.Login>(dto);

//            var response = usuarioClient.Autenticar(request);

//            if (!response.Success)
//            {
//                return null;
//            }

//            return response.Item;
//        }

//        public adduohelper.ResponseEnvelope<comum_dto.entries.AuthAPI> Autenticar(adduohelper.RequestEnvelope<comum_dto.entries.AuthAPI> request, dto.Usuario usuario)
//        {
//            var response = request.CreateResponse();

//            if (usuario != null)
//            {
//                response.Item.Token = tokenService.GerarToken(usuario.Id);
//            }
//            else
//            {
//                response.HttpStatusCode = HttpStatusCode.NotFound;
//            }

//            return response;
//        }


//    }
//}
