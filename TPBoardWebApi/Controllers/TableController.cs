using Microsoft.AspNetCore.Mvc;

namespace TPBoardWebApi.Controllers
{
    public class TableController : Controller
    {
        public IActionResult Index()
        {
            return View();
        }
    }
}
