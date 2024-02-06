using Aj.DataAccess.Repository.IRepository;
using Aj.DataAccess.Service;
using AJ.Models;
using Microsoft.AspNetCore.Mvc;
using System.Diagnostics;
using System.Text;

namespace AJStore.Areas.Customer.Controllers
{
    [Area("Customer")]
    public class HomeController : Controller
    {
        private readonly ILogger<HomeController> _logger;
        private readonly IProductRepository _product;
        public HomeController(ILogger<HomeController> logger, IProductRepository product)
        {
            _logger = logger;
            _product = product;
        }

        public IActionResult Index()
        {
            IEnumerable<Product> products = _product.GetAll();
            return View(products);
        }

        public IActionResult Details(int? produtId)
        {
            Product product = _product.Get(u => u.Id == produtId, includeProperties: "Category");
            return View(product);
        }

        public IActionResult Privacy()
        {
            return View();
        }

        [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
        public IActionResult Error()
        {
            return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
        }
    }
}
