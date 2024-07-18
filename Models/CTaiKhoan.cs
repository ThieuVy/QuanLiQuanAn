using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace QuanLiQuanAn.Models
{
    public class CTaiKhoan
    {
        [Key]
        [Required]
        [StringLength(50)]
        public string TenTK { get; set; } = null!;

        [Required]
        [StringLength(100)]
        public string MatKhau { get; set; } = null!;

        [StringLength(100)]
        public string? HoTen { get; set; }

        public DateTime NgaySinh { get; set; }

        [StringLength(200)]
        public string? DiaChi { get; set; }

        [StringLength(15)]
        public string? SoDT { get; set; }

        [EmailAddress]
        [StringLength(100)]
        public string? Email { get; set; }

        public int DiemTL { get; set; }

        [StringLength(50)]
        public string? HangTV { get; set; }

        public ICollection<CDatBan>? DatBans { get; set; }

        public ICollection<CHoaDon>? HoaDons { get; set; }

        public CTaiKhoan()
        {
        }

        public CTaiKhoan(string tenTK, string matKhau, string? hoTen, DateTime ngaySinh, string? diaChi, string? soDT, string? email, int diemTL, string? hangTV)
        {
            TenTK = tenTK;
            MatKhau = matKhau;
            HoTen = hoTen;
            NgaySinh = ngaySinh;
            DiaChi = diaChi;
            SoDT = soDT;
            Email = email;
            DiemTL = diemTL;
            HangTV = hangTV;
        }

        public override bool Equals(object? obj)
        {
            if (obj is CTaiKhoan khoan)
            {
                return TenTK == khoan.TenTK;
            }
            return false;
        }

        public override int GetHashCode()
        {
            return HashCode.Combine(TenTK);
        }
    }
}
