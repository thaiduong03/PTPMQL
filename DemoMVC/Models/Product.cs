using System.ComponentModel.DataAnnotations;
using Microsoft.EntityFrameworkCore;

namespace DemoMVC.Models;

public class Product
{
    public int Id { get; set; }

    [Required(ErrorMessage = "Vui lòng nhập tên sản phẩm")]
    [StringLength(100, ErrorMessage = "Tên sản phẩm không được vượt quá 100 ký tự")]
    public string Name { get; set; } = "";

    [Range(0.01, 1000000000000, ErrorMessage = "Giá sản phẩm phải lớn hơn 0")]
    [Precision(18, 2)]
    public decimal Price { get; set; }

    [StringLength(500, ErrorMessage = "Mô tả không được vượt quá 500 ký tự")]
    public string Description { get; set; } = "";
}