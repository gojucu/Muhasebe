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
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2214:DoNotCallOverridableMethodsInConstructors")]
        public HizmetUrun()
        {
            HizmetUrunFaturas = new HashSet<HizmetUrunFatura>();
            Kategoris = new HashSet<Kategori>();
            FiyatListesis = new HashSet<FiyatListesi>();
        }

        public int Id { get; set; }

        public Guid KullaniciID { get; set; }

        public bool Silindi { get; set; }

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

        public bool KritikStokUyarisi { get; set; }

        public double? KritikStokSeviyesi { get; set; }

        public double? VergilerHaricAlis { get; set; }

        [StringLength(10)]
        public string VergilerHaricAlisTur { get; set; }

        public double? VergilerHaricSatis { get; set; }

        [StringLength(10)]
        public string VergilerHaricSatisTur { get; set; }

        public double? Kdv { get; set; }

        public double? Oiv { get; set; }

        public double? AlisOtv { get; set; }

        [StringLength(10)]
        public string AlisOtvTur { get; set; }

        public double? SatisOtv { get; set; }

        [StringLength(10)]
        public string SatisOtvTur { get; set; }

        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual ICollection<HizmetUrunFatura> HizmetUrunFaturas { get; set; }

        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual ICollection<Kategori> Kategoris { get; set; }

        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual ICollection<FiyatListesi> FiyatListesis { get; set; }
    }
}
