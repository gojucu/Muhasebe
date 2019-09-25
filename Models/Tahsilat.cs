namespace Muhasebe.Models
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;
    using System.Data.Entity.Spatial;

    [Table("Tahsilat")]
    public partial class Tahsilat
    {
        public int Id { get; set; }

        public int FaturaID { get; set; }

        public DateTime TahsilatZamani { get; set; }

        public double Tutar { get; set; }

        public int HesapID { get; set; }

        [StringLength(500)]
        public string Aciklama { get; set; }

        public virtual Fatura Fatura { get; set; }

        public virtual KasaBanka KasaBanka { get; set; }
    }
}
