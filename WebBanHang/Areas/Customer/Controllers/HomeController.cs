﻿using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Threading.Tasks;
using WebBanHang.Models;

namespace WebBanHang.Areas.Customer.Controllers
{
    [Area("Customer")]
    public class HomeController : Controller
    {
        private readonly ILogger<HomeController> _logger;
        private ApplicationDbContext _db;
        

        public HomeController(ILogger<HomeController> logger, ApplicationDbContext db)
        {
            _logger = logger;
            _db = db;
        }

        public IActionResult Index(int page = 1)
        {
            var pageSize = 3;
            var dsSanPham = _db.Products.ToList();
            return View(dsSanPham.Skip((page - 1) * pageSize).Take(pageSize).ToList());
        }

        public IActionResult LoadMore(int page = 1)
        {
            var pageSize = 4;
            var dsSanPham = _db.Products.ToList();
            var products = dsSanPham.Skip((page - 1) * pageSize).Take(pageSize).ToList();
            if (products == null || products.Count == 0)
            {
                return Content("");
            }
            return PartialView("_ProductPartial", products);
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