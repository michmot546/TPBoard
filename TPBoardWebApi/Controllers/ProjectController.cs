using Microsoft.AspNetCore.Mvc;

namespace TPBoardWebApi.Controllers
{
    public class ProjectController : Controller
    {
        public IActionResult Index()
        {
            return View();
        }
    }
}
