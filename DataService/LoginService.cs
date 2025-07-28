using Microsoft.AspNetCore.Authentication.Cookies;
using System.Security.Claims;

namespace MentorMVCTemplate.DataService
{
    public class LoginService
    {
        public LoginService()
        {
            // load data
        }

        public bool IsValidUser(string username, string password)
        {
            return GetUserClaim(username,password) != null;
        }

        public List<Claim>? GetUserClaim(string username, string password)
        {
            if (username == "admin" && password == "password123")
            {
                var c = new List<Claim>();
                c.Add(new Claim(ClaimTypes.Name, username));
                c.Add(new Claim(ClaimTypes.Role, "Admin"));

                // custom claim
                c.Add(new Claim("allow_delete", "1"));

                return c;
            }

            if (username == "user" && password == "password123")
            {
                var c = new List<Claim>();
                c.Add(new Claim(ClaimTypes.Name, username));
                c.Add(new Claim(ClaimTypes.Role, "User"));

                // custom claim
                c.Add(new Claim("email", "user@sample.com"));

                return c;
            }

            return null;
        }

        public ClaimsIdentity? GetClaimsIdentity(string username, string password)
        {
            var c = GetUserClaim(username, password);
            if (c == null)
            {
                return null;
            }

            return new ClaimsIdentity(c, CookieAuthenticationDefaults.AuthenticationScheme);
        }

    }
}
