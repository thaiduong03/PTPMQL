using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace DemoMVC.Migrations
{
    /// <inheritdoc />
    public partial class AddKhoaAndSinhVien : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "Khoas",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    MaKhoa = table.Column<string>(type: "nvarchar(20)", maxLength: 20, nullable: false),
                    TenKhoa = table.Column<string>(type: "nvarchar(150)", maxLength: 150, nullable: false),
                    MoTa = table.Column<string>(type: "nvarchar(500)", maxLength: 500, nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Khoas", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "SinhViens",
                columns: table => new
                {
                    MaSinhVien = table.Column<string>(type: "nvarchar(10)", maxLength: 10, nullable: false),
                    HoTen = table.Column<string>(type: "nvarchar(150)", maxLength: 150, nullable: false),
                    NamNhapHoc = table.Column<int>(type: "int", nullable: false),
                    MaHeDaoTao = table.Column<string>(type: "nvarchar(2)", maxLength: 2, nullable: false),
                    MaNganh = table.Column<string>(type: "nvarchar(2)", maxLength: 2, nullable: false),
                    KhoaId = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_SinhViens", x => x.MaSinhVien);
                    table.ForeignKey(
                        name: "FK_SinhViens_Khoas_KhoaId",
                        column: x => x.KhoaId,
                        principalTable: "Khoas",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.InsertData(
                table: "Khoas",
                columns: new[] { "Id", "MaKhoa", "MoTa", "TenKhoa" },
                values: new object[,]
                {
                    { 1, "DCT", "Đào tạo và nghiên cứu lĩnh vực công nghệ thông tin.", "Khoa Công nghệ thông tin" },
                    { 2, "DCK", "Đào tạo các ngành cơ khí và điện.", "Khoa Cơ - Điện" },
                    { 3, "DCD", "Đào tạo và nghiên cứu lĩnh vực dầu khí.", "Khoa Dầu khí" },
                    { 4, "DCC", "Đào tạo lĩnh vực trắc địa, bản đồ và quản lý đất đai.", "Khoa Trắc địa - Bản đồ và Quản lý đất đai" },
                    { 5, "DCKS", "Đào tạo các ngành kỹ thuật mỏ.", "Khoa Mỏ" },
                    { 6, "DCDL", "Đào tạo và nghiên cứu lĩnh vực địa chất.", "Khoa Địa chất" },
                    { 7, "DCKT", "Đào tạo lĩnh vực kinh tế và quản trị kinh doanh.", "Khoa Kinh tế và Quản trị kinh doanh" },
                    { 8, "DMT", "Đào tạo và nghiên cứu lĩnh vực môi trường.", "Khoa Môi trường" }
                });

            migrationBuilder.CreateIndex(
                name: "IX_Khoas_MaKhoa",
                table: "Khoas",
                column: "MaKhoa",
                unique: true);

            migrationBuilder.CreateIndex(
                name: "IX_SinhViens_KhoaId",
                table: "SinhViens",
                column: "KhoaId");

            migrationBuilder.CreateIndex(
                name: "IX_SinhViens_MaSinhVien",
                table: "SinhViens",
                column: "MaSinhVien",
                unique: true);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(name: "SinhViens");
            migrationBuilder.DropTable(name: "Khoas");
        }
    }
}
