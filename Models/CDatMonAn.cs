using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace QuanLiQuanAn.Models
{
    public class CDatMonAn
    {
        [Key]
        [Required]
        [StringLength(50)]
        public string MaMA { get; set; }

        [Required]
        [StringLength(100)]
        public string TenMA { get; set; }

        [Required]
        public double Gia { get; set; }

        [StringLength(200)]
        public string Url_anh { get; set; }

        public ICollection<CHoaDon> HoaDons { get; set; }

        public ICollection<CCTHD> CCTHDs { get; set; }

        public CDatMonAn()
        {
        }

        public CDatMonAn(string maMA, string tenMA, double gia, string url_anh)
        {
            MaMA = maMA;
            TenMA = tenMA;
            Gia = gia;
            Url_anh = url_anh;
        }

        public override bool Equals(object obj)
        {
            return obj is CDatMonAn an &&
                   MaMA == an.MaMA;
        }

        public override int GetHashCode()
        {
            return -874208703 + EqualityComparer<string>.Default.GetHashCode(MaMA);
        }
    }
}
