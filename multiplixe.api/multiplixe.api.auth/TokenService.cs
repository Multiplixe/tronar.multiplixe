//using multiplixe.proxy.auth.comum.dto.settings;
//using Microsoft.IdentityModel.Tokens;
//using System;
//using System.IdentityModel.Tokens.Jwt;
//using System.Security.Claims;
//using System.Text;

//namespace multiplixe.api.auth
//{
//    public class TokenService
//    {
//        private AuthSettings authSettings { get; }

//        public TokenService(
//            AuthSettings authSettings )
//        {
//            this.authSettings = authSettings;


//            if (this.authSettings == null)
//            {
//                throw new Exception("authSettings vazio");
//            }


//            if (this.authSettings.SecretKey == null)
//            {
//                throw new Exception("SecretKey vazio");
//            }
//        }

//        public string GerarToken(Guid Id)
//        {
//            var tokenHandler = new JwtSecurityTokenHandler();
//            var key = Encoding.ASCII.GetBytes(this.authSettings.SecretKey);
//            var tokenDescriptor = new SecurityTokenDescriptor
//            {
//                Subject = new ClaimsIdentity(new Claim[]
//                {
//                    new Claim(ClaimTypes.NameIdentifier, Id.ToString()),
//                }),
//                Expires = DateTime.UtcNow.AddHours(2),
//                SigningCredentials = new SigningCredentials(new SymmetricSecurityKey(key), SecurityAlgorithms.HmacSha256Signature)
//            };
//            var token = tokenHandler.CreateToken(tokenDescriptor);
//            return tokenHandler.WriteToken(token);
//        }
//    }
//}
