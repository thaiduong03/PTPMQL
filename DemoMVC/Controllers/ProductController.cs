using System.Globalization;
using Microsoft.AspNetCore.Mvc;
using DemoMVC.Models;

namespace DemoMVC.Controllers;

public class ProductController : Controller
{
    public IActionResult Index() => View();

    public IActionResult List() => View();

    // GET: /Product/Create  -> hiển thị form
    [HttpGet]
    public IActionResult Create() => View();

    // POST: /Product/Create
    [HttpPost]
    [ValidateAntiForgeryToken]
    public IActionResult Create(Product product)
    {
        if (!ModelState.IsValid)
        {
            // Trả lại đúng view chứa form, kèm dữ liệu đã nhập
            return View(product);
        }

        TempData["SuccessMessage"] = "Sản phẩm đã được tạo thành công.";
        TempData["ProductName"] = product.Name;
        TempData["ProductPrice"] = product.Price.ToString(CultureInfo.InvariantCulture);
        TempData["ProductDescription"] = product.Description;

        return RedirectToAction(nameof(Details));
    }

    // GET: /Product/Details
    [HttpGet]
    public IActionResult Details()
    {
        // Đọc TempData rồi chuyển sang ViewBag/ViewData để dùng trong view hiện tại
        ViewBag.Message = TempData["SuccessMessage"] as string;
        ViewData["ProductName"] = TempData["ProductName"] as string;

        var product = new Product
        {
            Name = TempData["ProductName"] as string,
            Description = TempData["ProductDescription"] as string,
            Price = decimal.TryParse(
                TempData["ProductPrice"] as string,
                NumberStyles.Any,
                CultureInfo.InvariantCulture,
                out var price) ? price : 0
        };

        return View(product);
    }
}