using DemoMVC.Data;
using DemoMVC.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.Data.SqlClient;
using Microsoft.EntityFrameworkCore;

namespace DemoMVC.Controllers;

public class SinhVienController : Controller
{
    private readonly ApplicationDbContext _context;

    public SinhVienController(ApplicationDbContext context) => _context = context;

    public async Task<IActionResult> Index() => View(await _context.SinhViens.Include(s => s.Khoa).OrderBy(s => s.MaSinhVien).ToListAsync());

    [HttpGet]
    public async Task<IActionResult> Create()
    {
        await LoadKhoas();
        return View(new SinhVien { NamNhapHoc = DateTime.Today.Year });
    }

    [HttpPost, ValidateAntiForgeryToken]
    public async Task<IActionResult> Create(SinhVien sinhVien)
    {
        // The key is generated below and is intentionally absent from the create form.
        ModelState.Remove(nameof(sinhVien.MaSinhVien));

        if (sinhVien.NamNhapHoc > DateTime.Today.Year)
            ModelState.AddModelError(nameof(sinhVien.NamNhapHoc), "Năm nhập học không được lớn hơn năm hiện tại.");

        if (!await _context.Khoas.AnyAsync(k => k.Id == sinhVien.KhoaId))
            ModelState.AddModelError(nameof(sinhVien.KhoaId), "Khoa được chọn không tồn tại.");
        if (ModelState.IsValid)
        {
            var prefix = $"{sinhVien.NamNhapHoc % 100:00}{sinhVien.MaHeDaoTao}{sinhVien.MaNganh}";
            var existingCodes = await _context.SinhViens
                .Where(s => s.MaSinhVien.StartsWith(prefix))
                .Select(s => s.MaSinhVien)
                .ToListAsync();

            var lastSequence = 0;
            foreach (var existingCode in existingCodes)
            {
                if (existingCode.Length == 10 &&
                    existingCode.AsSpan(prefix.Length, 4).ToString().All(char.IsAsciiDigit) &&
                    int.TryParse(existingCode.AsSpan(prefix.Length, 4), out var sequence))
                {
                    lastSequence = Math.Max(lastSequence, sequence);
                }
            }

            var next = lastSequence + 1;
            if (next > 9999)
                ModelState.AddModelError(string.Empty, "Đã hết số thứ tự sinh viên cho tổ hợp năm, hệ đào tạo và ngành này.");
            else
            {
                sinhVien.MaSinhVien = prefix + next.ToString("D4");
                _context.SinhViens.Add(sinhVien);
                try
                {
                    await _context.SaveChangesAsync();
                    TempData["SuccessMessage"] = $"Đã thêm sinh viên với mã {sinhVien.MaSinhVien}.";
                    return RedirectToAction(nameof(Index));
                }
                catch (DbUpdateException exception)
                {
                    var isDuplicateCode = exception.GetBaseException() is SqlException { Number: 2601 or 2627 };
                    var errorMessage = isDuplicateCode
                        ? "Mã sinh viên vừa được cấp đã tồn tại. Vui lòng gửi lại biểu mẫu để cấp mã khác."
                        : "Không thể lưu sinh viên. Vui lòng thử lại.";
                    ModelState.AddModelError(string.Empty, errorMessage);
                }
            }
        }
        await LoadKhoas(sinhVien.KhoaId);
        return View(sinhVien);
    }

    [HttpGet]
    public async Task<IActionResult> Edit(string id)
    {
        var sinhVien = await _context.SinhViens.FindAsync(id);
        if (sinhVien == null) return NotFound();
        await LoadKhoas(sinhVien.KhoaId);
        return View(sinhVien);
    }

    [HttpPost, ValidateAntiForgeryToken]
    public async Task<IActionResult> Edit(string id, SinhVien sinhVien)
    {
        if (id != sinhVien.MaSinhVien) return NotFound();

        var existingSinhVien = await _context.SinhViens.FindAsync(id);
        if (existingSinhVien == null) return NotFound();

        // Immutable student identifiers are always taken from the database.
        sinhVien.MaSinhVien = existingSinhVien.MaSinhVien;
        sinhVien.NamNhapHoc = existingSinhVien.NamNhapHoc;
        sinhVien.MaHeDaoTao = existingSinhVien.MaHeDaoTao;
        sinhVien.MaNganh = existingSinhVien.MaNganh;

        if (!await _context.Khoas.AnyAsync(k => k.Id == sinhVien.KhoaId))
            ModelState.AddModelError(nameof(sinhVien.KhoaId), "Khoa được chọn không tồn tại.");
        if (!ModelState.IsValid)
        {
            await LoadKhoas(sinhVien.KhoaId);
            return View(sinhVien);
        }

        existingSinhVien.HoTen = sinhVien.HoTen;
        existingSinhVien.KhoaId = sinhVien.KhoaId;
        await _context.SaveChangesAsync();
        TempData["SuccessMessage"] = "Đã cập nhật thông tin sinh viên.";
        return RedirectToAction(nameof(Index));
    }

    [HttpGet]
    public async Task<IActionResult> Delete(string id)
    {
        var sinhVien = await _context.SinhViens.Include(s => s.Khoa).FirstOrDefaultAsync(s => s.MaSinhVien == id);
        return sinhVien == null ? NotFound() : View(sinhVien);
    }

    [HttpPost, ActionName("Delete"), ValidateAntiForgeryToken]
    public async Task<IActionResult> DeleteConfirmed(string id)
    {
        var sinhVien = await _context.SinhViens.FindAsync(id);
        if (sinhVien == null) return NotFound();
        _context.SinhViens.Remove(sinhVien);
        await _context.SaveChangesAsync();
        TempData["SuccessMessage"] = "Đã xóa sinh viên.";
        return RedirectToAction(nameof(Index));
    }

    private async Task LoadKhoas(int? selectedId = null) =>
        ViewBag.KhoaId = new SelectList(await _context.Khoas.OrderBy(k => k.TenKhoa).ToListAsync(), "Id", "TenKhoa", selectedId);
}
