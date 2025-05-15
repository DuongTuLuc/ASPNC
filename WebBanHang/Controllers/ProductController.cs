using System.Linq;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using WebBanHang.Models;
using Microsoft.EntityFrameworkCore;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Http;
using System;
using System.IO;

namespace WebBanHang.Controllers
{
    public class ProductController : Controller
    {
        private readonly ApplicationDbContext _db;
        private IWebHostEnvironment _hosting;
        public ProductController(ApplicationDbContext db,IWebHostEnvironment host)
        {
            _db = db;
            _hosting = host;
        }
        //liet ke danh sach san pham
        public IActionResult Index()
        {
            //doc tat ca san pham tu csld
            var dsSanPham = _db.Products.Include(x => x.Category).ToList();
            //tra ve cai view Index.cshtml liet ke san pham
            return View(dsSanPham);
        }
        //tra ve giao dien them moi san pham
        [HttpGet]
        public IActionResult Add()
        {
            //doc tat ca the loai trong csdl va truyen cho view
            ViewBag.TL = _db.Categories.Select(x => new SelectListItem { Value = x.Id.ToString(), Text = x.Name });
            return View();
        }
        //xu ly them moi san pham
        [HttpPost]
        public IActionResult Add(Product p, IFormFile ImageUrl)
        {
            if (ModelState.IsValid)
            {
                //xu ly upload
                if (ImageUrl != null)
                {
                    //code xu ly upload anh san pham
                    p.ImageUrl = SaveImage(ImageUrl);
                }
                //them sp vao csdl
                _db.Products.Add(p);
                _db.SaveChanges();
                TempData["success"] = "Đã thêm thành công 1 sản phẩm";
                //dieu huong ve action Index
                return RedirectToAction("Index");
            }
            ViewBag.TL = _db.Categories.Select(x => new SelectListItem { Value = x.Id.ToString(), Text = x.Name });
            return View();
        }
        //Phuong thuc Xu ly upload
        private string SaveImage(IFormFile image)
        {
            //đặt lại tên file cần lưu
            var filename = Guid.NewGuid().ToString() + Path.GetExtension(image.FileName);
            //lay duong dan luu tru wwwroot tren server
            var path = Path.Combine(_hosting.WebRootPath, @"images/products");
            var saveFile = Path.Combine(path, filename);
            using (var filestream = new FileStream(saveFile, FileMode.Create))
            {
                image.CopyTo(filestream);
            }
            return @"images/products/" + filename;
        }
        //tra ve giao dien cap nhap
        public IActionResult Update()
        {
            //doc tat ca san pham tu csld
            
            return View();
        }
        //xu ly cap nhap san pham
        [HttpPost]
        public IActionResult Update(Product p)
        {
            //doc tat ca san pham tu csld
            return View();
        }
    }
}