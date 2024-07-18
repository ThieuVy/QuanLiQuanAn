using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using QuanLiQuanAn.Models;

namespace QuanLiQuanAn.Models
{
    public class CDatBan
    {
        [Required]
        public int ViTriBan { get; set; }

        [Required]
        public DateTime Ngay { get; set; }

        [Required]
        public TimeSpan Gio { get; set; }

        [Required]
        public int SoLuong { get; set; }

        public string GhiChu { get; set; }

        [ForeignKey("CTaiKhoan")]
        public string TenTK { get; set; } // Foreign key

        public CTaiKhoan TaiKhoan { get; set; }

        public CDatBan()
        {
            
        }

        public CDatBan(string tenTK, int viTriBan, DateTime ngay, TimeSpan gio, int soLuong, string ghiChu)
        {
            TenTK = tenTK;
            ViTriBan = viTriBan;
            Ngay = ngay;
            Gio = gio;
            SoLuong = soLuong;
            GhiChu = ghiChu;
        }

        public override bool Equals(object obj)
        {
            return obj is CDatBan ban &&
                   EqualityComparer<CTaiKhoan>.Default.Equals(TaiKhoan, ban.TaiKhoan);
        }

        public override int GetHashCode()
        {
            return 202539832 + EqualityComparer<CTaiKhoan>.Default.GetHashCode(TaiKhoan);
        }
    }
}
