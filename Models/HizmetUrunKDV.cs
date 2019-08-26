namespace Muhasebe.Models
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;
    using System.Data.Entity.Spatial;

    [Table("HizmetUrunKDV")]
    public partial class HizmetUrunKDV
    {
        public int Id { get; set; }

        [Required]
        [StringLength(50)]
        public string Miktar { get; set; }
    }
}
