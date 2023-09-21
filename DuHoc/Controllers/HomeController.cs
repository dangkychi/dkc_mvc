using DuHoc.Data;
using DuHoc.Models;
using Microsoft.AspNetCore.Mvc;
using System.Diagnostics;

namespace DuHoc.Controllers
{
    public class HomeController : Controller
    {
        private readonly DuHocContext _context;

        private readonly ILogger<HomeController> _logger;

        public HomeController(ILogger<HomeController> logger, DuHocContext context)
        {
            _logger = logger;
            _context = context;
        }

        public IActionResult Index()
        {
            // Fetch the list of countries from your data source (e.g., database)
            var countries = _context.Country.ToList();

            // Pass the list of countries to the view
            return View(countries);
        }


        public IActionResult News()
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