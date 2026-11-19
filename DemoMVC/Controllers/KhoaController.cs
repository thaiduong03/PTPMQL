using DemoMVC.Data;
using DemoMVC.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace DemoMVC.Controllers;

public class KhoaController : Controller
{
    private readonly ApplicationDbContext _context;

    public KhoaController(ApplicationDbContext context) => _context = context;

    public async Task<IActionResult> Index() => View(await _context.Khoas.Include(k => k.SinhViens).OrderBy(k => k.MaKhoa).ToListAsync());

    [HttpGet]
    public IActionResult Create() => View();

    [HttpPost, ValidateAntiForgeryToken]
    public async Task<IActionResult> Create(Khoa khoa)
    {
        if (await _context.Khoas.AnyAsync(k => k.MaKhoa == khoa.MaKhoa))
            ModelState.AddModelError(nameof(khoa.MaKhoa), "Mã khoa đã tồn tại.");
        if (!ModelState.IsValid) return View(khoa);
        _context.Khoas.Add(khoa);
        await _context.SaveChangesAsync();
        TempData["SuccessMessage"] = "Đã thêm khoa.";
        return RedirectToAction(nameof(Index));
    }

    [HttpGet]
    public async Task<IActionResult> Edit(int id)
    {
        var khoa = await _context.Khoas.FindAsync(id);
        return khoa == null ? NotFound() : View(khoa);
    }

    [HttpPost, ValidateAntiForgeryToken]
    public async Task<IActionResult> Edit(int id, Khoa khoa)
    {
        if (id != khoa.Id) return NotFound();

        var existingKhoa = await _context.Khoas.FindAsync(id);
        if (existingKhoa == null) return NotFound();

        if (await _context.Khoas.AnyAsync(k => k.Id != id && k.MaKhoa == khoa.MaKhoa))
            ModelState.AddModelError(nameof(khoa.MaKhoa), "Mã khoa đã tồn tại.");
        if (!ModelState.IsValid) return View(khoa);

        existingKhoa.MaKhoa = khoa.MaKhoa;
        existingKhoa.TenKhoa = khoa.TenKhoa;
        existingKhoa.MoTa = khoa.MoTa;
        await _context.SaveChangesAsync();
        TempData["SuccessMessage"] = "Đã cập nhật khoa.";
        return RedirectToAction(nameof(Index));
    }

    [HttpGet]
    public async Task<IActionResult> Delete(int id)
    {
        var khoa = await _context.Khoas.Include(k => k.SinhViens).FirstOrDefaultAsync(k => k.Id == id);
        return khoa == null ? NotFound() : View(khoa);
    }

    [HttpPost, ActionName("Delete"), ValidateAntiForgeryToken]
    public async Task<IActionResult> DeleteConfirmed(int id)
    {
        var khoa = await _context.Khoas.Include(k => k.SinhViens).FirstOrDefaultAsync(k => k.Id == id);
        if (khoa == null) return NotFound();
        if (khoa.SinhViens.Count > 0)
        {
            TempData["ErrorMessage"] = "Không thể xóa khoa đang có sinh viên.";
            return RedirectToAction(nameof(Index));
        }
        _context.Khoas.Remove(khoa);
        await _context.SaveChangesAsync();
        TempData["SuccessMessage"] = "Đã xóa khoa.";
        return RedirectToAction(nameof(Index));
    }
}
