using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace QuanLiQuanAn.Models
{
    public class CHoaDon
    {
        [Key]
        public int SoHD { get; set; } // Primary key

        [Required]
        public DateTime NgHD { get; set; }

        [Required]
        [ForeignKey("CTaiKhoan")]
        public string TenTK { get; set; } // Foreign key

        public CTaiKhoan TaiKhoan { get; set; }

        [Required]
        [ForeignKey("CDatMonAn")]
        public string MaMA { get; set; } // Foreign key

        public CDatMonAn MonAn { get; set; }

        public ICollection<CCTHD> CCTHDs { get; set; }

        public CHoaDon()
        {
        }

        public CHoaDon(int soHD, DateTime ngHD, string tenTK, string maMA)
        {
            SoHD = soHD;
            NgHD = ngHD;
            TenTK = tenTK;
            MaMA = maMA;
        }

        public override bool Equals(object obj)
        {
            return obj is CHoaDon don &&
                   SoHD == don.SoHD;
        }

        public override int GetHashCode()
        {
            return 270757667 + SoHD.GetHashCode();
        }
    }
}
