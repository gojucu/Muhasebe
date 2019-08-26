namespace Muhasebe.Models
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;
    using System.Data.Entity.Spatial;

    [Table("Musteri")]
    public partial class Musteri
    {
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2214:DoNotCallOverridableMethodsInConstructors")]
        public Musteri()
        {
            Ibans = new HashSet<Iban>();
            Kategoris = new HashSet<Kategori>();
        }

        public int Id { get; set; }

        public Guid KullaniciID { get; set; }

        public bool Silindi { get; set; }

        [Required]
        [StringLength(50)]
        public string FirmaUnvani { get; set; }

        [Required]
        [StringLength(50)]
        public string FirmaKodu { get; set; }

        [StringLength(50)]
        public string KisaIsim { get; set; }

        public int? KategoriID { get; set; }

        [StringLength(50)]
        public string FiyatListesi { get; set; }

        [StringLength(50)]
        public string Mail { get; set; }

        [StringLength(50)]
        public string TelefonNo { get; set; }

        [StringLength(50)]
        public string FaksNo { get; set; }

        [StringLength(500)]
        public string AcıkAdres { get; set; }

        public bool YurtDisindami { get; set; }

        [StringLength(50)]
        public string Ilce { get; set; }

        [StringLength(50)]
        public string Il { get; set; }

        [Required]
        [StringLength(50)]
        public string Turu { get; set; }

        [StringLength(50)]
        public string VknTckn { get; set; }

        [StringLength(50)]
        public string VergiDairesi { get; set; }

        [StringLength(50)]
        public string TcKimlikNo { get; set; }

        [StringLength(10)]
        public string DovizKuru { get; set; }

        public bool AcilisBakiyesi { get; set; }

        public DateTime? BakiyeTarih { get; set; }

        public double? Ucret { get; set; }

        [StringLength(50)]
        public string DovizTuru { get; set; }

        [StringLength(50)]
        public string AcilisIslemTuru { get; set; }

        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual ICollection<Iban> Ibans { get; set; }

        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual ICollection<Kategori> Kategoris { get; set; }
    }
}
