using M2ICarsAPI.Controllers.JWT;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Threading.Tasks;
using System.Web.Http;

namespace M2ICarsAPI.Controllers
{
    public class AccountController : ApiController
    {
        private AuthService _authService;
        
        // GET: Membership
        [HttpGet]
        [Route("api/Account/Admin/Authenticate")]
        public async Task<string> AuthenticateAdmin(String mail, String password)
        {
            MemberShipProvider m = new MemberShipProvider();
            RSAKeyProvider r = new RSAKeyProvider();
            _authService = new AuthService(m, r);
            string Token = await _authService.GenerateJwtTokenAsync(mail, password, MemberShipProvider.Role.ADMIN);
            return Token;
        }

        // GET: Membership
        [HttpGet]
        [Route("api/Account/Driver/Authenticate")]
        public async Task<string> AuthenticateDriver(String mail, String password)
        {
            MemberShipProvider m = new MemberShipProvider();
            RSAKeyProvider r = new RSAKeyProvider();
            _authService = new AuthService(m, r);
            string Token = await _authService.GenerateJwtTokenAsync(mail, password, MemberShipProvider.Role.DRIVER);
            return Token;
        }

        // GET: Membership
        [HttpGet]
        [Route("api/Account/User/Authenticate")]
        public async Task<string> AuthenticateUser(String mail, String password)
        {
            MemberShipProvider m = new MemberShipProvider();
            RSAKeyProvider r = new RSAKeyProvider();
            _authService = new AuthService(m, r);
            string Token = await _authService.GenerateJwtTokenAsync(mail, password, MemberShipProvider.Role.USER);
            return Token;
        }

        [Route("api/Account/User/Get")]
        [TokenAuthenticate(MemberShipProvider.Role.USER)]
        public string Get(int id)
        {
            return "Vous etes connecté";
        }
    }
}
