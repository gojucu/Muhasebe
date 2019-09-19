using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace Muhasebe.Models
{
    public partial class model_fatura_altUrun_liste
    {

        public int Id { get; set; }

        public Guid KullaniciID { get; set; }

        public bool Silindi { get; set; }

        [StringLength(50)]
        public string Aciklama { get; set; }

        public int? MusteriID { get; set; }

        [StringLength(50)]
        public string Irsaliye { get; set; }

        public DateTime? DuzenlemeTarih { get; set; }

        public DateTime? VadeTarihi { get; set; }

        [StringLength(50)]
        public string FaturaNoSeri { get; set; }

        [StringLength(50)]
        public string FaturaNoSira { get; set; }

        [StringLength(50)]
        public string FaturaDovizi { get; set; }

        public int altId { get; set; }

        public int HizmetUrunID { get; set; }

       

        public double? Miktar { get; set; }

        public double? BirimFiyat { get; set; }

        public double? Vergi { get; set; }

     
    }
}