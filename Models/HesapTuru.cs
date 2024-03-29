namespace Muhasebe.Models
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;
    using System.Data.Entity.Spatial;

    [Table("HesapTuru")]
    public partial class HesapTuru
    {
        public int Id { get; set; }

        [Required]
        [StringLength(50)]
        public string Ad { get; set; }
    }
}
