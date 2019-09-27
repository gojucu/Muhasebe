namespace Muhasebe.Models
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;
    using System.Data.Entity.Spatial;

    public partial class HesapParaGirisCiki
    {
        public int id { get; set; }

        [Required]
        [StringLength(50)]
        public string Tur { get; set; }

        public DateTime Tarih { get; set; }

        public double Meblag { get; set; }

        [StringLength(500)]
        public string Aciklama { get; set; }
    }
}
