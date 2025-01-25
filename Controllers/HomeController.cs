using System.Diagnostics;
using Cake_Shop.Data;
using Cake_Shop.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace Cake_Shop.Controllers
{
    public class HomeController : Controller
    {
        private readonly ILogger<HomeController> _logger;
        private readonly ApplicationDbContext _context;
       

 
        public HomeController(ILogger<HomeController> logger , ApplicationDbContext context)
        {
            _logger = logger;
            _context = context;
        }

        public IActionResult Index()
        {
            return View();
        }



         public IActionResult Products()
        {
            var getdata = _context.products.ToList();

            return View(getdata);

        }

        public IActionResult Details(int id)
        {
            var products = _context.products.SingleOrDefault(p => p.Id == id); // «»ÕÀ ⁄‰ «·„‰ Ã »‰«¡ ⁄·Ï «·„⁄—›
            if (products == null)
            {
                return NotFound(); // ≈–« ·„ Ì „ «·⁄ÀÊ— ⁄·Ï «·„‰ Ã
            }
            return View(products); // ≈—”«· «·„‰ Ã ≈·Ï «·’›Õ…
        }


        public IActionResult Customers()
        {
            var getdata = _context.customers.ToList();

            return View(getdata);

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
