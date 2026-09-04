using Microsoft.AspNetCore.Mvc;
using DemoMVC.Models;

namespace DemoMVC.Controllers;

public class ProductController : Controller
{
    // GET: /Product/Index
    public IActionResult Index()
    {
        return View();
    }

    // GET: /Product/List
    public IActionResult List()
    {
        return View();
    }

    // GET: /Product/Details
    [HttpGet]
    public IActionResult Details()
    {
        return View();
    }

    // GET: /Product/Create
    [HttpGet]
    public IActionResult Create()
    {
        return View();
    }

    // POST: /Product/Create
    [HttpPost]
    public IActionResult Create(Product product)
    {
        if (!ModelState.IsValid)
        {
            return View(product);
        }

        ViewBag.Message = "Thêm sản phẩm thành công!";
        ViewData["ProductName"] = product.Name;

        // Truyền sản phẩm vừa nhập sang Details
        return View("Details", product);
    }
}