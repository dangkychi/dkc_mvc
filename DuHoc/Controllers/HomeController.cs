﻿using DuHoc.Data;
using DuHoc.Models;
using Microsoft.AspNetCore.Mvc;
using System.Data;
using System.Diagnostics;
using Microsoft.AspNetCore.Authorization;

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
            var countries = _context.Country.ToList();
            return View(countries);
        }

        [Authorize(Roles = "admin")]
        public IActionResult Admin()
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