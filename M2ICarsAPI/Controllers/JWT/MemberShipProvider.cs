using M2ICarsDAO;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Claims;
using System.Web;

namespace M2ICarsAPI.Controllers.JWT
{
    public class MemberShipProvider
    {
        private DB db = new DB();
        public enum Role
        {
            ALL,
            ADMIN,
            DRIVER,
            USER
        }

        public List<Claim> GetUserClaims(Role role)
        {
            List<Claim> claims = new List<Claim>();
            claims.Add(new Claim(ClaimTypes.Role, role.ToString()));
            return claims;
        }

        public bool VerifyAdminPassword(string mail, string password)
        {
            try
            {
                Admin admin = db.Admins.First(u => u.Mail == mail);

                if (admin.Password == password)
                    return true;
            }
            catch
            {
                return false;
            }

            return false;
        }

        public bool VerifyDriverPassword(string mail, string password)
        {
            try
            {
                Driver driver = db.Drivers.First(d => d.Email == mail);

                if (driver.Password == password)
                    return true;
            }
            catch
            {
                return false;
            }

            return false;
        }

        public bool VerifyUserPassword(string mail, string password)
        {
            try { 
                User user = db.Users.First(u => u.Email == mail);

                if (user.Password == password)
                    return true;
            } catch
            {
                return false;
            }

            return false;
        }

        public MemberShipProvider()
        {

        }
    }
}