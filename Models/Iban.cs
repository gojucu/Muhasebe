namespace Muhasebe.Models
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;
    using System.Data.Entity.Spatial;

    [Table("Iban")]
    public partial class Iban
    {
        public int Id { get; set; }

        [Required]
        [StringLength(50)]
        public string IbanNo { get; set; }

        public int MusteriID { get; set; }

        public virtual Musteri Musteri { get; set; }
    }
}
