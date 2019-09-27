namespace Muhasebe.Models
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;
    using System.Data.Entity.Spatial;

    [Table("Islemler")]
    public partial class Islemler
    {
        public int Id { get; set; }

        public Guid KullaniciID { get; set; }

        public int? FaturaID { get; set; }

        [Required]
        [StringLength(50)]
        public string IslemTuru { get; set; }

        public DateTime Tarih { get; set; }

        public DateTime? VadeTarihi { get; set; }

        public int? HesapID { get; set; }

        public double Meblag { get; set; }

        [StringLength(10)]
        public string Aciklama { get; set; }

        [StringLength(50)]
        public string BankaAdi { get; set; }

        [StringLength(50)]
        public string CekNo { get; set; }

        public virtual Fatura Fatura { get; set; }

        public virtual KasaBanka KasaBanka { get; set; }
    }
}
