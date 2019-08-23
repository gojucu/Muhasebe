namespace Muhasebe.Models
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;
    using System.Data.Entity.Spatial;

    [Table("AcilisIslemTuru")]
    public partial class AcilisIslemTuru
    {
        public int Id { get; set; }

        [StringLength(50)]
        public string Ad { get; set; }
    }
}
