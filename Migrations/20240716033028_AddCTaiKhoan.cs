using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace QuanLiQuanAn.Migrations
{
    public partial class AddCTaiKhoan : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "DatMonAns",
                columns: table => new
                {
                    MaMA = table.Column<string>(type: "nvarchar(50)", maxLength: 50, nullable: false),
                    TenMA = table.Column<string>(type: "nvarchar(100)", maxLength: 100, nullable: false),
                    Gia = table.Column<double>(type: "float", nullable: false),
                    Url_anh = table.Column<string>(type: "nvarchar(200)", maxLength: 200, nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_DatMonAns", x => x.MaMA);
                });

            migrationBuilder.CreateTable(
                name: "TaiKhoans",
                columns: table => new
                {
                    TenTK = table.Column<string>(type: "nvarchar(50)", maxLength: 50, nullable: false),
                    MatKhau = table.Column<string>(type: "nvarchar(100)", maxLength: 100, nullable: false),
                    HoTen = table.Column<string>(type: "nvarchar(100)", maxLength: 100, nullable: false),
                    NgaySinh = table.Column<DateTime>(type: "datetime2", nullable: false),
                    DiaChi = table.Column<string>(type: "nvarchar(200)", maxLength: 200, nullable: false),
                    SoDT = table.Column<string>(type: "nvarchar(15)", maxLength: 15, nullable: false),
                    Email = table.Column<string>(type: "nvarchar(100)", maxLength: 100, nullable: false),
                    DiemTL = table.Column<int>(type: "int", nullable: false),
                    HangTV = table.Column<string>(type: "nvarchar(50)", maxLength: 50, nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_TaiKhoans", x => x.TenTK);
                });

            migrationBuilder.CreateTable(
                name: "DatBans",
                columns: table => new
                {
                    TenTK = table.Column<string>(type: "nvarchar(50)", nullable: false),
                    ViTriBan = table.Column<int>(type: "int", nullable: false),
                    Ngay = table.Column<DateTime>(type: "datetime2", nullable: false),
                    Gio = table.Column<TimeSpan>(type: "time", nullable: false),
                    SoLuong = table.Column<int>(type: "int", nullable: false),
                    GhiChu = table.Column<string>(type: "nvarchar(max)", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_DatBans", x => x.TenTK);
                    table.ForeignKey(
                        name: "FK_DatBans_TaiKhoans_TenTK",
                        column: x => x.TenTK,
                        principalTable: "TaiKhoans",
                        principalColumn: "TenTK",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "HoaDons",
                columns: table => new
                {
                    SoHD = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    NgHD = table.Column<DateTime>(type: "datetime2", nullable: false),
                    TenTK = table.Column<string>(type: "nvarchar(50)", nullable: false),
                    MaMA = table.Column<string>(type: "nvarchar(50)", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_HoaDons", x => x.SoHD);
                    table.ForeignKey(
                        name: "FK_HoaDons_DatMonAns_MaMA",
                        column: x => x.MaMA,
                        principalTable: "DatMonAns",
                        principalColumn: "MaMA",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_HoaDons_TaiKhoans_TenTK",
                        column: x => x.TenTK,
                        principalTable: "TaiKhoans",
                        principalColumn: "TenTK",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "CCTHDs",
                columns: table => new
                {
                    SoHD = table.Column<int>(type: "int", nullable: false),
                    MaMA = table.Column<string>(type: "nvarchar(50)", nullable: false),
                    SoLuong = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_CCTHDs", x => new { x.SoHD, x.MaMA });
                    table.ForeignKey(
                        name: "FK_CCTHDs_DatMonAns_MaMA",
                        column: x => x.MaMA,
                        principalTable: "DatMonAns",
                        principalColumn: "MaMA",
                        onDelete: ReferentialAction.NoAction); // Changed to NoAction
                    table.ForeignKey(
                        name: "FK_CCTHDs_HoaDons_SoHD",
                        column: x => x.SoHD,
                        principalTable: "HoaDons",
                        principalColumn: "SoHD",
                        onDelete: ReferentialAction.NoAction); // Changed to NoAction
                });

            migrationBuilder.CreateIndex(
                name: "IX_HoaDons_MaMA",
                table: "HoaDons",
                column: "MaMA");

            migrationBuilder.CreateIndex(
                name: "IX_HoaDons_TenTK",
                table: "HoaDons",
                column: "TenTK");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "CCTHDs");

            migrationBuilder.DropTable(
                name: "DatBans");

            migrationBuilder.DropTable(
                name: "HoaDons");

            migrationBuilder.DropTable(
                name: "DatMonAns");

            migrationBuilder.DropTable(
                name: "TaiKhoans");
        }
    }
}
