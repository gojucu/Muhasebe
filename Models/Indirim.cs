namespace Muhasebe.Models
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;
    using System.Data.Entity.Spatial;

    [Table("Indirim")]
    public partial class Indirim
    {
        public int Id { get; set; }

        [Required]
        [StringLength(50)]
        public string Urun { get; set; }

        public double IndirimMiktari { get; set; }

        public int ListeID { get; set; }

        public virtual FiyatListesi FiyatListesi { get; set; }
    }
}
