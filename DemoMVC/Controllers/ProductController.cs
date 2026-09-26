using DemoMVC.Data;
using DemoMVC.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace DemoMVC.Controllers;

public class ProductController : Controller
{
    private readonly ApplicationDbContext _context;

    public ProductController(ApplicationDbContext context)
    {
        _context = context;
    }

    // GET: /Product
    public IActionResult Index()
    {
        return View();
    }

    // GET: /Product/List
    public IActionResult List()
    {
        var products = _context.Products.ToList();

        return View(products);
    }

    // GET: /Product/Create
    [HttpGet]
    public IActionResult Create()
    {
        return View();
    }

    // POST: /Product/Create
    [HttpPost]
    [ValidateAntiForgeryToken]
    public IActionResult Create(Product product)
    {
        if (!ModelState.IsValid)
        {
            return View(product);
        }

        // Lưu sản phẩm vào database
        _context.Products.Add(product);
        _context.SaveChanges();

        // SQL Server tự sinh ID
        TempData["SuccessMessage"] =
            "Sản phẩm đã được tạo thành công.";

        return RedirectToAction(
            nameof(Details),
            new { id = product.Id });
    }

    // GET: /Product/Details/1
    [HttpGet]
    public IActionResult Details(int id)
    {
        var product = _context.Products
            .FirstOrDefault(p => p.Id == id);

        if (product == null)
        {
            return NotFound();
        }

        ViewBag.Message = TempData["SuccessMessage"];

        return View(product);
    }

    // GET: /Product/Edit/1
    [HttpGet]
    public IActionResult Edit(int id)
    {
        var product = _context.Products
            .FirstOrDefault(p => p.Id == id);

        if (product == null)
        {
            return NotFound();
        }

        return View(product);
    }

    // POST: /Product/Edit/1
    [HttpPost]
    [ValidateAntiForgeryToken]
    public IActionResult Edit(int id, Product product)
    {
        if (id != product.Id)
        {
            return NotFound();
        }

        if (!ModelState.IsValid)
        {
            return View(product);
        }

        _context.Products.Update(product);
        _context.SaveChanges();

        TempData["SuccessMessage"] =
            "Sản phẩm đã được cập nhật thành công.";

        return RedirectToAction(
            nameof(Details),
            new { id = product.Id });
    }
    // GET: /Product/Delete/1
[HttpGet]
public IActionResult Delete(int id)
{
    var product = _context.Products
        .FirstOrDefault(p => p.Id == id);

    if (product == null)
    {
        return NotFound();
    }

    return View(product);
}

// POST: /Product/Delete/1
[HttpPost, ActionName("Delete")]
[ValidateAntiForgeryToken]
public IActionResult DeleteConfirmed(int id)
{
    var product = _context.Products
        .FirstOrDefault(p => p.Id == id);

    if (product == null)
    {
        return NotFound();
    }

    _context.Products.Remove(product);
    _context.SaveChanges();

    TempData["SuccessMessage"] =
        "Sản phẩm đã được xóa thành công.";

    return RedirectToAction(nameof(List));
}
}