using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace QuanLiQuanAn.Models
{
    public class CCTHD
    {
        [Key]
        [Column(Order = 0)]
        public int SoHD { get; set; }

        [ForeignKey("SoHD")]
        public CHoaDon HoaDon { get; set; }

        [Key]
        [Column(Order = 1)]
        public string MaMA { get; set; }

        [ForeignKey("MaMA")]
        public CDatMonAn MonAn { get; set; }

        [Required]
        public int SoLuong { get; set; }

        public CCTHD()
        {
        }

        public CCTHD(int soHD, string maMA, int soLuong)
        {
            SoHD = soHD;
            MaMA = maMA;
            SoLuong = soLuong;
        }

        public override bool Equals(object? obj)
        {
            if (obj is CCTHD cthd)
            {
                return SoHD == cthd.SoHD &&
                   MaMA == cthd.MaMA;
            }
            return false;
        }

        public override int GetHashCode()
        {
            return HashCode.Combine(SoHD, MaMA);
        }

        // public override bool Equals(object obj)
        // {
        //     return obj is CCTHD cCTHD &&
        //            SoHD == cCTHD.SoHD &&
        //            MaMA == cCTHD.MaMA;
        // }

        // public override int GetHashCode()
        // {
        //     int hashCode = -244375940;
        //     hashCode = hashCode * -1521134295 + SoHD.GetHashCode();
        //     hashCode = hashCode * -1521134295 + EqualityComparer<string>.Default.GetHashCode(MaMA);
        //     return hashCode;
        // }
    }
}
