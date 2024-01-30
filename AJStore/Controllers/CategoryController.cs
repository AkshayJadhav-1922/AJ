using Microsoft.AspNetCore.Mvc;

namespace AJStore.Controllers
{
    public class CategoryController : Controller
    {
        public IActionResult Index()
        {
            return View();
        }
    }
}
