namespace Muhasebe.Models
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;
    using System.Data.Entity.Spatial;

    [Table("Fatura")]
    public partial class Fatura
    {
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2214:DoNotCallOverridableMethodsInConstructors")]
        public Fatura()
        {
            HizmetUrunFaturas = new HashSet<HizmetUrunFatura>();
            Islemlers = new HashSet<Islemler>();
            Tahsilats = new HashSet<Tahsilat>();
            Kategoris = new HashSet<Kategori>();
        }

        public int Id { get; set; }

        public Guid KullaniciID { get; set; }

        public bool Silindi { get; set; }

        [StringLength(50)]
        public string Aciklama { get; set; }

        public int? MusteriID { get; set; }

        [StringLength(50)]
        public string Irsaliye { get; set; }

        public DateTime? DuzenlemeTarih { get; set; }

        public DateTime? VadeTarihi { get; set; }

        [StringLength(50)]
        public string FaturaNoSeri { get; set; }

        [StringLength(50)]
        public string FaturaNoSira { get; set; }

        [StringLength(50)]
        public string FaturaDovizi { get; set; }

        public double? AraToplam { get; set; }

        public double? KdvToplam { get; set; }

        public double? GenelToplam { get; set; }

        public double? KalanBorc { get; set; }

        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual ICollection<HizmetUrunFatura> HizmetUrunFaturas { get; set; }

        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual ICollection<Islemler> Islemlers { get; set; }

        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual ICollection<Tahsilat> Tahsilats { get; set; }

        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual ICollection<Kategori> Kategoris { get; set; }
    }
}
