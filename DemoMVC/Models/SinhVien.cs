using System.ComponentModel.DataAnnotations;

namespace DemoMVC.Models;

public class SinhVien
{
    [Key]
    [StringLength(10, MinimumLength = 10, ErrorMessage = "Mã sinh viên gồm đúng 10 chữ số")]
    [RegularExpression("^[0-9]{10}$", ErrorMessage = "Mã sinh viên chỉ gồm 10 chữ số")]
    [Display(Name = "Mã sinh viên")]
    public string MaSinhVien { get; set; } = "";

    [Required(ErrorMessage = "Vui lòng nhập họ và tên")]
    [StringLength(150)]
    [Display(Name = "Họ và tên")]
    public string HoTen { get; set; } = "";

    [Required(ErrorMessage = "Vui lòng nhập năm nhập học")]
    [Range(2000, 2099, ErrorMessage = "Năm nhập học phải từ 2000 đến 2099")]
    [Display(Name = "Năm nhập học")]
    public int NamNhapHoc { get; set; }

    [Required(ErrorMessage = "Vui lòng nhập mã hệ đào tạo gồm 2 chữ số")]
    [StringLength(2, MinimumLength = 2, ErrorMessage = "Mã hệ đào tạo phải gồm 2 chữ số")]
    [RegularExpression("^[0-9]{2}$", ErrorMessage = "Mã hệ đào tạo phải gồm 2 chữ số")]
    [Display(Name = "Mã hệ đào tạo (2 số)")]
    public string MaHeDaoTao { get; set; } = "";

    [Required(ErrorMessage = "Vui lòng nhập mã ngành gồm 2 chữ số")]
    [StringLength(2, MinimumLength = 2, ErrorMessage = "Mã ngành phải gồm 2 chữ số")]
    [RegularExpression("^[0-9]{2}$", ErrorMessage = "Mã ngành phải gồm 2 chữ số")]
    [Display(Name = "Mã ngành (2 số)")]
    public string MaNganh { get; set; } = "";

    [Required(ErrorMessage = "Vui lòng chọn khoa")]
    [Display(Name = "Khoa")]
    public int KhoaId { get; set; }

    public Khoa? Khoa { get; set; }
}
