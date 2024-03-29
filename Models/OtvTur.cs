namespace Muhasebe.Models
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;
    using System.Data.Entity.Spatial;

    [Table("OtvTur")]
    public partial class OtvTur
    {
        public int Id { get; set; }

        [Required]
        [StringLength(10)]
        public string Ad { get; set; }
    }
}
