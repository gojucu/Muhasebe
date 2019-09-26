namespace Muhasebe.Models
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;
    using System.Data.Entity.Spatial;

    [Table("KasaBanka")]
    public partial class KasaBanka
    {
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2214:DoNotCallOverridableMethodsInConstructors")]
        public KasaBanka()
        {
            Tahsilats = new HashSet<Tahsilat>();
        }

        public int Id { get; set; }

        public Guid KullaniciID { get; set; }

        public bool Silindi { get; set; }

        [Required]
        [StringLength(50)]
        public string HesapIsmi { get; set; }

        [Required]
        [StringLength(50)]
        public string HesapTuru { get; set; }

        [StringLength(50)]
        public string DovizCinsi { get; set; }

        public double? AcilisBakiyesi { get; set; }

        public DateTime? AcilisTarihi { get; set; }

        [StringLength(50)]
        public string Banka { get; set; }

        [StringLength(50)]
        public string BankaSubesi { get; set; }

        [StringLength(50)]
        public string HesapNumarasi { get; set; }

        [StringLength(50)]
        public string Iban { get; set; }

        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual ICollection<Tahsilat> Tahsilats { get; set; }
    }
}
