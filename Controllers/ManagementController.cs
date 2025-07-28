using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace MentorMVCTemplate.Controllers
{
    [Authorize(Roles ="Admin,Editor")]
    public class ManagementController : Controller
    {
        public IActionResult Index()
        {
            return ManageArticle();
        }

        [HttpGet("/manage/article")]
        public IActionResult ManageArticle()
        {
            return View("/views/protected/managearticles.cshtml");
        }

    }
}
