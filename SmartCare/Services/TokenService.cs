using Microsoft.IdentityModel.Tokens;
using SmartCare.Models;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;

namespace SmartCare.Services
{
    public class TokenService
    {
        //metodo para gerar o token
        public static object GenerateToken(UsuarioModel user)
        {
            //chama a classe criada Key
            var key = Encoding.ASCII.GetBytes(Key.Secret);
            //usado para configurar o token(oq vai ser armazenado,tempo de expiração e etc)
            var tokenConfig = new SecurityTokenDescriptor
            {
                //faz com que dentro do token salve o ID do funcionario
                Subject = new System.Security.Claims.ClaimsIdentity(new Claim[]
                {
                    new Claim("UsuarioId", user.ID_USUARIO.ToString()),
                }),
                //Define o tempo para expirar
                Expires = DateTime.UtcNow.AddHours(3),
                //Define o tipo de assinatura(qual a chave privada e qual a criptografia usada "HmacSha256Signature")
                SigningCredentials = new SigningCredentials(new SymmetricSecurityKey(key), SecurityAlgorithms.HmacSha256Signature)
            };
            //gera o token
            var tokenHandler = new JwtSecurityTokenHandler();
            //cria variavel de token
            var token = tokenHandler.CreateToken(tokenConfig);
            var tokenString = tokenHandler.WriteToken(token);

            return new
            {
                token = tokenString
            };
        }
        
    }
 }


