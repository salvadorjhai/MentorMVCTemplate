using MentorMVCTemplate.DataService;
using MentorMVCTemplate.Models;
using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Mvc;
using System.Security.Claims;

namespace MentorMVCTemplate.Controllers
{
    public class AuthenticationController : Controller
    {
        private LoginService _la = new();
        
        [HttpGet("/authentication/")]
        public IActionResult Index()
        {
            return RedirectToAction("Login");
        }

        [HttpGet("/authentication/login")]
        public IActionResult Login()
        {
            if (HttpContext.User.Identity != null && HttpContext.User.Identity.IsAuthenticated)
            {
                return RedirectToAction("index", "home");
            }
            return View("login");
        }

        [HttpGet("/authentication/access-denied")]
        public IActionResult AccessDenied()
        {
            return View("accessdenied");
        }

        [HttpPost("/authentication/login")]
        public async Task<IActionResult> ValidateLogin([FromBody] LoginViewModel model)
        {
            if (!ModelState.IsValid)
            {
                Response.StatusCode = 401;
                return Json(new { errorid = 1, description = "invalid credentials" });
            }

            if (!_la.IsValidUser(model.username, model.password))
            {
                Response.StatusCode = 401;
                return Json(new { errorid = 1, description = "invalid credentials" });
            }

            // login
            var idendity = _la.GetClaimsIdentity(model.username, model.password);
            if (idendity == null)
            {
                Response.StatusCode = 401;
                return Json(new { errorid = 1, description = "invalid credentials" });

            }
            var principal = new ClaimsPrincipal(idendity);
            await HttpContext.SignInAsync(principal);

            // create claims (name, role, etc)
            // create claims identity (new ClaimsIdentity(claims, CookieAuthenticationDefaults.AuthenticationScheme))
            // create claimprincipal  (new ClaimsPrincipal(idendity))
            // HttpContext.SignInAsync 

            // ---------------------

            Response.StatusCode = 200;
            return Json(new { errorid = 0, description = "success!" });
        }

        [HttpGet("/authentication/logout")]
        public async Task<IActionResult> Logout()
        {
            await HttpContext.SignOutAsync();
            return RedirectToAction("Login");
        }

    }
}
