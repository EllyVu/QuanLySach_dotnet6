using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace QuanLySach.Migrations
{
    /// <inheritdoc />
    public partial class Updatedb : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "LoaiSach",
                columns: table => new
                {
                    MaLoai = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    TenLoai = table.Column<string>(type: "nvarchar(30)", maxLength: 30, nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_LoaiSach", x => x.MaLoai);
                });

            migrationBuilder.CreateTable(
                name: "NhaXuatBan",
                columns: table => new
                {
                    MaXb = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Tenxb = table.Column<string>(type: "nvarchar(30)", maxLength: 30, nullable: true),
                    DiaChi = table.Column<string>(type: "nvarchar(30)", maxLength: 30, nullable: true),
                    GhiChu = table.Column<string>(type: "nvarchar(30)", maxLength: 30, nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK__NhaXuatB__272520CA2C1F079C", x => x.MaXb);
                });

            migrationBuilder.CreateTable(
                name: "Sach",
                columns: table => new
                {
                    Masach = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Maxb = table.Column<int>(type: "int", nullable: true),
                    Maloai = table.Column<int>(type: "int", nullable: true),
                    Tensach = table.Column<string>(type: "nvarchar(30)", maxLength: 30, nullable: true),
                    Tacgia = table.Column<string>(type: "nvarchar(30)", maxLength: 30, nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK__Sach__9F923C88F7AD74A8", x => x.Masach);
                    table.ForeignKey(
                        name: "FK_Sach_LoaiSach_Maloai",
                        column: x => x.Maloai,
                        principalTable: "LoaiSach",
                        principalColumn: "MaLoai");
                    table.ForeignKey(
                        name: "FK_Sach_NhaXuatBan_Maxb",
                        column: x => x.Maxb,
                        principalTable: "NhaXuatBan",
                        principalColumn: "MaXb");
                });

            migrationBuilder.CreateIndex(
                name: "IX_Sach_Maloai",
                table: "Sach",
                column: "Maloai");

            migrationBuilder.CreateIndex(
                name: "IX_Sach_Maxb",
                table: "Sach",
                column: "Maxb");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "Sach");

            migrationBuilder.DropTable(
                name: "LoaiSach");

            migrationBuilder.DropTable(
                name: "NhaXuatBan");
        }
    }
}
