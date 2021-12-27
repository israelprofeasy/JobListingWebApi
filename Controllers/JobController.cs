using Microsoft.AspNetCore.Mvc;

namespace JobWebApi.Controllers
{
    public class JobController : Controller
    {
        public IActionResult Index()
        {
            return View();
        }
    }
}
