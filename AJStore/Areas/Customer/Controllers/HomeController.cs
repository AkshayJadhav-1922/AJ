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
        private readonly ITransientService _transientService1;
        private readonly ITransientService _transientService2;
        private readonly IScopedService _scopedService1;
        private readonly IScopedService _scopedService2;
        private readonly ISingletoneService _singletoneService1;
        private readonly ISingletoneService _singletoneService2;
        public HomeController(ILogger<HomeController> logger, IScopedService scopedService1, ISingletoneService singletoneService1, ITransientService transientService1, IScopedService scopedService2, ISingletoneService singletoneService2, ITransientService transientService2)
        {
            _logger = logger;
            _scopedService1 = scopedService1;
            _singletoneService1 = singletoneService1;
            _transientService1 = transientService1;
            _scopedService2 = scopedService2;
            _singletoneService2 = singletoneService2;
            _transientService2 = transientService2;
        }

        public IActionResult Index()
        {
            StringBuilder guid = new StringBuilder();
            //Every time when transient is called, it gives new implementation of that service 
            guid.Append($"Transient1: {_transientService1.GetGuid()}\n");
            guid.Append($"Transient2: {_transientService1.GetGuid()}\n\n");

            //implementation stays same for one request irrespective of how many times service is called in that request 
            guid.Append($"Scoped1: {_scopedService1.GetGuid()}\n");
            guid.Append($"Scoped2: {_scopedService2.GetGuid()}\n\n");

            //implementation remains same throughout application lifecyle
            guid.Append($"Singletone1: {_singletoneService1.GetGuid()}\n");
            guid.Append($"Singletone1: {_singletoneService2.GetGuid()}\n\n");


            //return Ok( guid.ToString() );
            return View();
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
