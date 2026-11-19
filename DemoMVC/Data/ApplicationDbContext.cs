using DemoMVC.Models;
using Microsoft.EntityFrameworkCore;

namespace DemoMVC.Data;

public class ApplicationDbContext : DbContext
{
    public ApplicationDbContext(
        DbContextOptions<ApplicationDbContext> options)
        : base(options)
    {
    }

    public DbSet<Product> Products { get; set; }
    public DbSet<Khoa> Khoas { get; set; }
    public DbSet<SinhVien> SinhViens { get; set; }

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        base.OnModelCreating(modelBuilder);

        modelBuilder.Entity<Khoa>()
            .HasIndex(k => k.MaKhoa)
            .IsUnique();

        modelBuilder.Entity<SinhVien>()
            .HasIndex(s => s.MaSinhVien)
            .IsUnique();

        modelBuilder.Entity<SinhVien>()
            .HasOne(s => s.Khoa)
            .WithMany(k => k.SinhViens)
            .HasForeignKey(s => s.KhoaId)
            .OnDelete(DeleteBehavior.Restrict);

        modelBuilder.Entity<Khoa>().HasData(
            new Khoa { Id = 1, MaKhoa = "DCT", TenKhoa = "Khoa Công nghệ thông tin", MoTa = "Đào tạo và nghiên cứu lĩnh vực công nghệ thông tin." },
            new Khoa { Id = 2, MaKhoa = "DCK", TenKhoa = "Khoa Cơ - Điện", MoTa = "Đào tạo các ngành cơ khí và điện." },
            new Khoa { Id = 3, MaKhoa = "DCD", TenKhoa = "Khoa Dầu khí", MoTa = "Đào tạo và nghiên cứu lĩnh vực dầu khí." },
            new Khoa { Id = 4, MaKhoa = "DCC", TenKhoa = "Khoa Trắc địa - Bản đồ và Quản lý đất đai", MoTa = "Đào tạo lĩnh vực trắc địa, bản đồ và quản lý đất đai." },
            new Khoa { Id = 5, MaKhoa = "DCKS", TenKhoa = "Khoa Mỏ", MoTa = "Đào tạo các ngành kỹ thuật mỏ." },
            new Khoa { Id = 6, MaKhoa = "DCDL", TenKhoa = "Khoa Địa chất", MoTa = "Đào tạo và nghiên cứu lĩnh vực địa chất." },
            new Khoa { Id = 7, MaKhoa = "DCKT", TenKhoa = "Khoa Kinh tế và Quản trị kinh doanh", MoTa = "Đào tạo lĩnh vực kinh tế và quản trị kinh doanh." },
            new Khoa { Id = 8, MaKhoa = "DMT", TenKhoa = "Khoa Môi trường", MoTa = "Đào tạo và nghiên cứu lĩnh vực môi trường." }
        );
    }
}
