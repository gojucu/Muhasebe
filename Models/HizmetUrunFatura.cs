namespace Muhasebe.Models
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;
    using System.Data.Entity.Spatial;

    [Table("HizmetUrunFatura")]
    public partial class HizmetUrunFatura
    {
        public int Id { get; set; }

        public int HizmetUrunID { get; set; }

        public int FaturaID { get; set; }

        public double? Miktar { get; set; }

        public double? BirimFiyat { get; set; }

        public double? Vergi { get; set; }

        public virtual Fatura Fatura { get; set; }

        public virtual HizmetUrun HizmetUrun { get; set; }
    }
}
