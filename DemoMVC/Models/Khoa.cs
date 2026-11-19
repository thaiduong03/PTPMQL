using System.ComponentModel.DataAnnotations;

namespace DemoMVC.Models;

public class Khoa
{
    public int Id { get; set; }

    [Required(ErrorMessage = "Vui lòng nhập mã khoa")]
    [StringLength(20, ErrorMessage = "Mã khoa không vượt quá 20 ký tự")]
    [Display(Name = "Mã khoa")]
    public string MaKhoa { get; set; } = "";

    [Required(ErrorMessage = "Vui lòng nhập tên khoa")]
    [StringLength(150, ErrorMessage = "Tên khoa không vượt quá 150 ký tự")]
    [Display(Name = "Tên khoa")]
    public string TenKhoa { get; set; } = "";

    [StringLength(500, ErrorMessage = "Mô tả không vượt quá 500 ký tự")]
    [Display(Name = "Mô tả")]
    public string? MoTa { get; set; }

    public ICollection<SinhVien> SinhViens { get; set; } = new List<SinhVien>();
}
