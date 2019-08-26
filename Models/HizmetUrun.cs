namespace Muhasebe.Models
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;
    using System.Data.Entity.Spatial;

    [Table("HizmetUrun")]
    public partial class HizmetUrun
    {
        public int Id { get; set; }

        public Guid KullaniciID { get; set; }

        [Required]
        [StringLength(50)]
        public string Ad { get; set; }

        [Required]
        [StringLength(50)]
        public string UrunKodu { get; set; }

        [StringLength(50)]
        public string BarkodNumarasi { get; set; }

        [StringLength(500)]
        public string Resim { get; set; }

        [StringLength(50)]
        public string AlisSatisBirimi { get; set; }

        [StringLength(50)]
        public string StokTakibi { get; set; }

        public double? BaslangicStok { get; set; }

        public bool? KritikStokUyarisi { get; set; }

        public double? VergilerHaricAlis { get; set; }

        public double? VergilerHaricSatis { get; set; }

        [StringLength(50)]
        public string Kdv { get; set; }

        [StringLength(50)]
        public string Oiv { get; set; }

        [StringLength(50)]
        public string AlisOtv { get; set; }

        [StringLength(50)]
        public string SatisOtv { get; set; }

        public double? VergilerDahilAlis { get; set; }

        public double? VergilerDahilSatis { get; set; }
    }
}
