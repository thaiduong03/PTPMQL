using System.ComponentModel.DataAnnotations;
using Microsoft.EntityFrameworkCore;

namespace DemoMVC.Models;

public class Product
{
    public int Id { get; set; }

    [Required(ErrorMessage = "Vui lòng nhập tên sản phẩm")]
    public string Name { get; set; } = "";

    [Range(0, double.MaxValue, ErrorMessage = "Giá sản phẩm không hợp lệ")]
    [Precision(18, 2)]
    public decimal Price { get; set; }

    public string Description { get; set; } = "";
}