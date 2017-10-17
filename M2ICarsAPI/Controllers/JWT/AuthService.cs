using Microsoft.IdentityModel.Tokens;
using System;
using System.Collections.Generic;
using System.IdentityModel.Tokens.Jwt;
using System.Linq;
using System.Security.Claims;
using System.Security.Cryptography;
using System.Threading.Tasks;
using System.Web;

namespace M2ICarsAPI.Controllers.JWT
{
    public class AuthService
    {
        private readonly MemberShipProvider _membershipProvider;
        private readonly RSAKeyProvider _rsaProvider;

        public AuthService(MemberShipProvider membershipProvider, RSAKeyProvider rsaProvider)
        {
            _membershipProvider = membershipProvider;
            _rsaProvider = rsaProvider;
        }

        public AuthService()
        {

        }

        public async Task<string> GenerateJwtTokenAsync(string username, string password, MemberShipProvider.Role role)
        {
            switch (role)
            {
                case MemberShipProvider.Role.ADMIN:
                    if (!_membershipProvider.VerifyAdminPassword(username, password))
                        return "TokenError";
                    break;
                case MemberShipProvider.Role.USER:
                    if (!_membershipProvider.VerifyUserPassword(username, password))
                        return "TokenError";
                    break;
                case MemberShipProvider.Role.DRIVER:
                    if (!_membershipProvider.VerifyDriverPassword(username, password))
                        return "TokenError";
                    break;
                default:
                    return "TokenError";
            }

            List<Claim> claims = _membershipProvider.GetUserClaims(role);

            string publicAndPrivateKey = await _rsaProvider.GetPrivateAndPublicKeyAsync();
            if (publicAndPrivateKey == null)
                return "TokenError";

            RSACryptoServiceProvider publicAndPrivate = new RSACryptoServiceProvider();
            publicAndPrivate.FromXmlString(publicAndPrivateKey);

            RsaSecurityKey s = new RsaSecurityKey(publicAndPrivate);
            JwtSecurityToken jwtToken = new JwtSecurityToken
            (
                issuer: "http://localhost",
                audience: "http://localhost",
                claims: claims,
                signingCredentials: new SigningCredentials(s, SecurityAlgorithms.RsaSha256Signature),
                expires: DateTime.Now.AddDays(30)
            );

            JwtSecurityTokenHandler tokenHandler = new JwtSecurityTokenHandler();
            string tokenString = tokenHandler.WriteToken(jwtToken);

            return tokenString;
        }

        public async Task<Boolean> ValidateTokenAsync(string TokenString, MemberShipProvider.Role authorizedRole)
        {
            Boolean result = false;

            try
            {
                SecurityToken securityToken = new JwtSecurityToken(TokenString);
                JwtSecurityTokenHandler securityTokenHandler = new JwtSecurityTokenHandler();
                RSACryptoServiceProvider publicAndPrivate = new RSACryptoServiceProvider();

                string publicAndPrivateKey = await _rsaProvider.GetPrivateAndPublicKeyAsync();
                if (publicAndPrivateKey == null)
                    return result;

                publicAndPrivate.FromXmlString(publicAndPrivateKey);

                TokenValidationParameters validationParameters = new TokenValidationParameters()
                {
                    ValidIssuer = "http://localhost",
                    ValidAudience = "http://localhost",
                    IssuerSigningKey = new RsaSecurityKey(publicAndPrivate)
                };
                
                ClaimsPrincipal claimsPrincipal = securityTokenHandler.ValidateToken(TokenString, validationParameters, out securityToken);
                //On teste si le token contient le rôle spécifié
                if (authorizedRole != MemberShipProvider.Role.ALL)
                    claimsPrincipal.Claims.First(c => c.Value == authorizedRole.ToString());
                result = true;
            }
            catch (Exception ex)
            {
                result = false;
            }

            return result;
        }

        public async Task<ClaimsPrincipal> GetClaimTokenAsync(string TokenString)
        {
            SecurityToken securityToken = new JwtSecurityToken(TokenString);
            JwtSecurityTokenHandler securityTokenHandler = new JwtSecurityTokenHandler();
            RSACryptoServiceProvider publicAndPrivate = new RSACryptoServiceProvider();

            string publicAndPrivateKey = await _rsaProvider.GetPrivateAndPublicKeyAsync();

            publicAndPrivate.FromXmlString(publicAndPrivateKey);

            TokenValidationParameters validationParameters = new TokenValidationParameters()
            {
                ValidIssuer = "http://localhost",
                ValidAudience = "http://localhost",
                IssuerSigningKey = new RsaSecurityKey(publicAndPrivate)
            };

            ClaimsPrincipal claimsPrincipal = securityTokenHandler.ValidateToken(TokenString, validationParameters, out securityToken);

            return claimsPrincipal;
        }
    }
}