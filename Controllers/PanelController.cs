using System;
using Muhasebe.Models;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using System.Drawing;
using System.IO;
using System.Data.Entity;

namespace Muhasebe.Controllers
{
    public class PanelController : Controller
    {
        // GET: Panel
        MuhasebeModel context = new MuhasebeModel();
        public ActionResult Index()
        {
            ViewBag.Kullanici = Session["Kullanici"];
            if (ViewBag.Kullanici != null)
            {
                return View();
            }
            else
            {
                return RedirectToAction("GirisYap", "Uyelik");
            }
        }

        public ActionResult SidebarGetir()
        {
            ViewBag.Kullanici = Session["Kullanici"];
            return View();
        }

        //Müşteri

        public ActionResult Musteriler()
        {
            ViewBag.Kullanici = Session["Kullanici"];
            if (ViewBag.Kullanici != null)
            {
            return View(context.Musteris.ToList());
            }
            else
            {
                return RedirectToAction("GirisYap", "Uyelik");
            }
            
        }
        public ActionResult MusteriEkle(int id)
        {
            ViewBag.Kullanici = Session["Kullanici"];
            //Tür
            var musteriTurusListe = context.MusteriTurus.ToList();
            ViewBag.musteriTurusListe = musteriTurusListe;

            var acilisIslemTuru = context.AcilisIslemTurus.ToList();
            ViewBag.acilisIslemTuru = acilisIslemTuru;

            var fiyatListesi = context.FiyatListesis.ToList();
            ViewBag.fiyatListesi = fiyatListesi;

            var dovizKuru = context.DovizKurus.ToList();
            ViewBag.dovizKuru = dovizKuru;

            ViewBag.dovizTur = context.DovizTurus.ToList();

            var kategoriler = context.Kategoris.ToList();

            ViewBag.kategoriler = kategoriler;
            if (ViewBag.Kullanici != null)
            {
                if (id == 0)
                {
                    return View();

                }
                else
                {
                    Musteri mus = context.Musteris.Find(id);
                    List<string> stringlist = new List<string>();
                    foreach (var item in mus.Kategoris)
                    {


                        stringlist.Add(item.Ad);
                        string dogCsv = string.Join(", ", stringlist.ToArray());
                        Console.WriteLine(dogCsv);

                        ViewBag.katttt = dogCsv;

                    }
                    
                    return View(mus);
                }
            }
            else
            {
                return RedirectToAction("GirisYap", "Uyelik");
            }

        }

        [HttpPost]
        [ValidateInput(false)]
        public ActionResult MusteriEkle(Musteri musteri, string kategoriler, string[] iban)
        {
            ViewBag.Kullanici = Session["Kullanici"];
            if (kategoriler == null || kategoriler == "")
            {
                kategoriler = "kategorisiz";
            }

            //Kategoriler
            if (musteri.Id == 0)
            {
                string[] kategoris = kategoriler.Split(',');
                foreach (string kategori in kategoris)
                {
                    Kategori kat = context.Kategoris.FirstOrDefault(x => x.Ad.ToLower() == kategori.ToLower().Trim());
                    if (kat == null)
                    {
                        kat = new Kategori
                        {
                            Ad = kategori
                        };
                        context.Kategoris.Add(kat);
                        context.SaveChanges();

                    }
                    musteri.Kategoris.Add(kat);
                    context.SaveChanges();
                }

                //iban
                foreach(var a in iban)
                {
                     Iban ib = new Iban
                     {
                         IbanNo = a
                     };
                     context.Ibans.Add(ib);
                }

                musteri.KullaniciID = ViewBag.Kullanici.Id;
                musteri.Silindi = false;
                context.Musteris.Add(musteri);
                context.SaveChanges();
                return RedirectToAction("MusteriEkle", "Panel");
            }
            else
            {

                Musteri guncellenecek = context.Musteris.FirstOrDefault(x => x.Id == musteri.Id);
                //kategori
                string[] kategoris = kategoriler.Split(',');


                foreach (var item in guncellenecek.Kategoris.ToList())
                {
                    guncellenecek.Kategoris.Remove(item);
                    context.SaveChanges();
                }
                foreach (string kategori in kategoris)
                {
                    Kategori kat = context.Kategoris.FirstOrDefault(x => x.Ad.ToLower() == kategori.ToLower().Trim());
                    if (kat == null)
                    {
                        kat = new Kategori
                        {
                            Ad = kategori
                        };
                        context.Kategoris.Add(kat);
                        context.SaveChanges();

                    }
                    guncellenecek.Kategoris.Add(kat);
                    //context.Entry(guncellenecek).State = EntityState.Modified;
                    context.SaveChanges();

                }
                //iban
                //Iban ib = new Iban();
                //ib.IbanNo = iban;
                //context.Ibans.Add(ib);
                
                guncellenecek.FirmaUnvani = musteri.FirmaUnvani;
                guncellenecek.FirmaKodu = musteri.FirmaKodu;
                guncellenecek.KisaIsim = musteri.KisaIsim;
                guncellenecek.FiyatListesi = musteri.FiyatListesi;
                guncellenecek.Mail = musteri.Mail;
                guncellenecek.TelefonNo = musteri.TelefonNo;
                guncellenecek.FaksNo = musteri.FaksNo;
                guncellenecek.AcıkAdres = musteri.AcıkAdres;
                guncellenecek.YurtDisindami = musteri.YurtDisindami;
                guncellenecek.Ilce = musteri.Ilce;
                guncellenecek.Il = musteri.Il;
                guncellenecek.Turu = musteri.Turu;
                guncellenecek.VknTckn = musteri.VknTckn;
                guncellenecek.VergiDairesi = musteri.VergiDairesi;
                guncellenecek.TcKimlikNo = musteri.TcKimlikNo;
                guncellenecek.DovizKuru = musteri.DovizKuru;
                guncellenecek.AcilisBakiyesi = musteri.AcilisBakiyesi;
                guncellenecek.BakiyeTarih = musteri.BakiyeTarih;
                guncellenecek.Ucret = musteri.Ucret;
                guncellenecek.DovizTuru = musteri.DovizTuru;
                guncellenecek.AcilisIslemTuru = musteri.AcilisIslemTuru;

                context.SaveChanges();
                return RedirectToAction("MusteriEkle", "Panel");
            }
        }

        public ActionResult MusteriSil(int id)
        {
            Musteri guncellenecek = context.Musteris.FirstOrDefault(x => x.Id == id);
            guncellenecek.Silindi = true;
            context.SaveChanges();
            return RedirectToAction("Musteriler", "Panel");
        }

        public ActionResult MusteriDetay(int id)
        {
            ViewBag.Kullanici = Session["Kullanici"];
            return View(context.Musteris.FirstOrDefault(x=>x.Id==id));
        }

        //Hizmet/ürün

        public ActionResult HizmetUrun()
        {
            ViewBag.Kullanici = Session["Kullanici"];
            if (ViewBag.Kullanici != null)
            {
                return View(context.HizmetUruns.ToList());
            }
            else
            {
                return RedirectToAction("GirisYap", "Uyelik");
            }
        }

        public ActionResult HizmetUrunEkle(int id)
        {
            ViewBag.Kullanici = Session["Kullanici"];

            var stokTakibi = context.StokTakibis.ToList();
            ViewBag.stokTakibi = stokTakibi;

            var kdv = context.HizmetUrunKDVs.ToList();
            ViewBag.kdv = kdv;

            var kategoriler = context.Kategoris.ToList();
            ViewBag.kategoriler = kategoriler;

            var dovizTur = context.DovizTurus.ToList();
            ViewBag.dovizTur = dovizTur;

            ViewBag.otvTur = context.OtvTurs.ToList();

            if (ViewBag.Kullanici != null)
            {
                if (id == 0)
                {
                    return View();

                }
                else
                {
                    HizmetUrun Hu = context.HizmetUruns.Find(id);
                    List<string> stringlist = new List<string>();
                    foreach (var item in Hu.Kategoris)
                    {


                        stringlist.Add(item.Ad);
                        string xkat = string.Join(", ", stringlist.ToArray());
                        
                        ViewBag.katttt = xkat;
                    }
                    return View(Hu);
                }
            }
            else
            {
                return RedirectToAction("GirisYap", "Uyelik");
            }

        }

        private string ResimKaydet(HttpPostedFileBase resim)
        {
            Image orj = Image.FromStream(resim.InputStream);
            string dosyaadi = Path.GetFileNameWithoutExtension(resim.FileName) + Guid.NewGuid() + Path.GetExtension(resim.FileName);
            orj.Save(Server.MapPath("~/Content/images/" + dosyaadi));
            return dosyaadi;
        }
        [HttpPost]
        [ValidateInput(false)]
        public ActionResult HizmetUrunEkle(HizmetUrun Hu, HttpPostedFileBase Resim, string kategoriler, bool KritikStokUyarisi)
        {
            ViewBag.Kullanici = Session["Kullanici"];
            //resim
            if (Resim != null)
            {
                string dosyaadi = ResimKaydet(Resim);
                Hu.Resim = "/Content/images/" + dosyaadi;
            }

            //Hu.Resim = "/Content/images/" + dosyaadi;

            //Kategoriler
            if (kategoriler == null || kategoriler == "")
            {
                kategoriler = "kategorisiz";
            }
            if (Hu.Id == 0)
            {
                string[] kategoris = kategoriler.Split(',');
                foreach (string kategori in kategoris)
                {
                    Kategori kat = context.Kategoris.FirstOrDefault(x => x.Ad.ToLower() == kategori.ToLower().Trim());
                    if (kat == null)
                    {
                        kat = new Kategori
                        {
                            Ad = kategori
                        };
                        context.Kategoris.Add(kat);
                        context.SaveChanges();

                    }
                    Hu.Kategoris.Add(kat);
                    context.SaveChanges();
                }
                
                Hu.KullaniciID = ViewBag.Kullanici.Id;
                Hu.Silindi = false;
                
                context.HizmetUruns.Add(Hu);
                context.SaveChanges();
                return RedirectToAction("HizmetUrun", "Panel");
            }
            else
            {
                HizmetUrun guncellenecek = context.HizmetUruns.FirstOrDefault(x => x.Id == Hu.Id);
                //kategori
                foreach (var item in guncellenecek.Kategoris.ToList())
                {
                    guncellenecek.Kategoris.Remove(item);
                    context.SaveChanges();
                }
                string[] kategoris = kategoriler.Split(',');
                foreach (string kategori in kategoris)
                {
                    Kategori kat = context.Kategoris.FirstOrDefault(x => x.Ad.ToLower() == kategori.ToLower().Trim());
                    if (kat == null)
                    {
                        kat = new Kategori
                        {
                            Ad = kategori
                        };
                        context.Kategoris.Add(kat);
                        context.SaveChanges();

                    }
                    guncellenecek.Kategoris.Add(kat);
                    context.SaveChanges();
                }
                

                guncellenecek.Ad = Hu.Ad;
                guncellenecek.UrunKodu = Hu.UrunKodu;
                guncellenecek.BarkodNumarasi = Hu.BarkodNumarasi;
                if (Hu.Resim != null)
                {
                guncellenecek.Resim = Hu.Resim;
                }

                guncellenecek.AlisSatisBirimi = Hu.AlisSatisBirimi;
                guncellenecek.StokTakibi = Hu.StokTakibi;
                guncellenecek.BaslangicStok = Hu.BaslangicStok;
                guncellenecek.KritikStokUyarisi = Hu.KritikStokUyarisi;
                guncellenecek.KritikStokSeviyesi = Hu.KritikStokSeviyesi;
                guncellenecek.VergilerHaricAlis = Hu.VergilerHaricAlis;
                guncellenecek.VergilerHaricAlisTur = Hu.VergilerHaricAlisTur;
                guncellenecek.VergilerHaricSatis = Hu.VergilerHaricSatis;
                guncellenecek.VergilerHaricSatisTur = Hu.VergilerHaricSatisTur;
                guncellenecek.Kdv = Hu.Kdv;
                guncellenecek.Oiv = Hu.Oiv;
                guncellenecek.AlisOtv = Hu.AlisOtv;
                guncellenecek.AlisOtvTur = Hu.AlisOtvTur;
                guncellenecek.SatisOtv = Hu.SatisOtv;
                guncellenecek.SatisOtvTur = Hu.SatisOtvTur;

                
                context.SaveChanges();
                return RedirectToAction("HizmetUrun", "Panel");
            }
        }
        
        public ActionResult HizmetUrunDetay(int id)
        {
            ViewBag.Kullanici = Session["Kullanici"];
            if (ViewBag.Kullanici != null)
            {
                return View(context.HizmetUruns.FirstOrDefault(x => x.Id == id));
            }
            else
            {
                return  RedirectToAction("GirisYap", "Uyelik");
            }
        }

        //Faturalar
        public ActionResult Faturalar()
        {
            ViewBag.Kullanici = Session["Kullanici"];
            if (ViewBag.Kullanici != null)
            {
                return View(context.Faturas.ToList().Where(x=>x.KullaniciID==ViewBag.Kullanici.Id));
            }
            else
            {
                return RedirectToAction("GirisYap", "Uyelik");
            }
        }

        public ActionResult FaturaEkle(int id)
        {
            ViewBag.Kullanici = Session["Kullanici"];

            var musteri = context.Musteris.ToList();
            ViewBag.musteri = musteri;

            var irsaliye = context.Irsaliyes.ToList();
            ViewBag.irsaliye = irsaliye;

            var faturaDoviz = context.DovizTurus.ToList();
            ViewBag.faturaDoviz = faturaDoviz;

            var kdv = context.HizmetUrunKDVs.ToList();
            ViewBag.kdv = kdv;

            var urun = context.HizmetUruns.ToList();
            ViewBag.urun = urun;

            var hizmetUrunFatura = context.HizmetUrunFaturas.ToList().Where(x => x.FaturaID == id);
            ViewBag.huFatura = hizmetUrunFatura;
            
            if (ViewBag.Kullanici != null)
            {
                if (id == 0)
                {
 
                    return View();
                }
                else
                {
                    Fatura fatura = context.Faturas.Find(id);

                    var huFaturaSayısı = context.HizmetUrunFaturas.ToList().Where(x => x.FaturaID == id).Count();
                    if (huFaturaSayısı != 0)
                    {
                        var list = from ft in context.Faturas
                                   join hu in context.HizmetUrunFaturas on ft.Id equals hu.FaturaID into ps
                                   from m in ps.DefaultIfEmpty()
                                   join ur in context.HizmetUruns on m.HizmetUrunID equals ur.Id into ts
                                   from n in ts.DefaultIfEmpty()
                                   where ft.Id == id
                                   select new
    model_fatura_altUrun_liste
                                   {
                                       Id = ft.Id,
                                       Aciklama = ft.Aciklama,
                                       MusteriID = ft.MusteriID,
                                       Irsaliye = ft.Irsaliye,
                                       altId = m.Id,
                                       BirimFiyat = m.BirimFiyat,
                                       DuzenlemeTarih = ft.DuzenlemeTarih,
                                       FaturaDovizi = ft.FaturaDovizi,
                                       FaturaNoSeri = ft.FaturaNoSeri,
                                       FaturaNoSira = ft.FaturaNoSira,
                                       HizmetUrunID = m.HizmetUrunID,
                                       KullaniciID = ft.KullaniciID,
                                       Miktar = m.Miktar,
                                       Silindi = ft.Silindi,
                                       VadeTarihi = ft.VadeTarihi,
                                       Vergi = m.Vergi,
                                       Toplam =m.Toplam,
                                       VergilerHaricSatis=n.VergilerHaricSatis

                                   };
                        
                        ViewBag.list = list;
                    }

                    List<string> stringlist = new List<string>();
                    foreach (var item in fatura.Kategoris)
                    {
                        stringlist.Add(item.Ad);
                        string xkat = string.Join(", ", stringlist.ToArray());

                        ViewBag.katttt = xkat;
                    }
                    return View(fatura);
                }
            }
            else
            {
                return RedirectToAction("GirisYap", "Uyelik");
            }
        }

        [HttpPost]
        [ValidateInput(false)]
        public JsonResult FaturaEkle(Fatura fatura, string kategoriler)
        {
            ViewBag.Kullanici = Session["Kullanici"];
            
            //Kategoriler
            if (kategoriler == null || kategoriler == "")
            {
                kategoriler = "kategorisiz";
            }
            if (fatura.Id == 0)
            {
                string[] kategoris = kategoriler.Split(',');
                foreach (string kategori in kategoris)
                {
                    Kategori kat = context.Kategoris.FirstOrDefault(x => x.Ad.ToLower() == kategori.ToLower().Trim());
                    if (kat == null)
                    {
                        kat = new Kategori
                        {
                            Ad = kategori
                        };
                        context.Kategoris.Add(kat);
                        context.SaveChanges();

                    }
                    fatura.Kategoris.Add(kat);
                    context.SaveChanges();
                }
                
                fatura.KullaniciID = ViewBag.Kullanici.Id;
                fatura.Silindi = false;
                fatura.KalanBorc = fatura.GenelToplam;
                context.Faturas.Add(fatura);
                context.SaveChanges();
                int sonId = context.Faturas.OrderByDescending(m => m.Id).FirstOrDefault().Id;
                return Json(sonId.ToString(),JsonRequestBehavior.AllowGet);
            }
            else
            {
                var rrr = context.Islemlers.ToList().Where(x=>x.FaturaID==fatura.Id);
                double tahsilatToplam = 0;
                foreach(var item in rrr)
                {
                    tahsilatToplam += item.Meblag;
                }
                Fatura guncellenecek = context.Faturas.FirstOrDefault(x => x.Id == fatura.Id);

                //faturanın ürünlerinin düzenlenmesi için ilk önce gerekli hizmetUrunFatura silme kısmı
                var hizmetUrunFaturas = context.HizmetUrunFaturas.ToList().Where(x => x.FaturaID == guncellenecek.Id);
                foreach (var item2 in hizmetUrunFaturas)
                {
                    HizmetUrunFatura huf = item2;
                    context.HizmetUrunFaturas.Remove(item2);
                }

                //kategori
                string[] kategoris = kategoriler.Split(',');
                
                foreach (var item in guncellenecek.Kategoris.ToList())
                {
                    guncellenecek.Kategoris.Remove(item);
                    context.SaveChanges();
                }
                foreach (string kategori in kategoris)
                {
                    Kategori kat = context.Kategoris.FirstOrDefault(x => x.Ad.ToLower() == kategori.ToLower().Trim());
                    if (kat == null)
                    {
                        kat = new Kategori
                        {
                            Ad = kategori
                        };
                        context.Kategoris.Add(kat);
                        context.SaveChanges();
                    }
                    guncellenecek.Kategoris.Add(kat);
                    context.SaveChanges();
                }
                
                guncellenecek.Silindi = fatura.Silindi;
                guncellenecek.Aciklama = fatura.Aciklama;
                guncellenecek.Irsaliye = fatura.Irsaliye;
                guncellenecek.MusteriID = fatura.MusteriID;
                guncellenecek.DuzenlemeTarih = fatura.DuzenlemeTarih;
                guncellenecek.VadeTarihi = fatura.VadeTarihi;
                guncellenecek.FaturaNoSeri = fatura.FaturaNoSeri;
                guncellenecek.FaturaNoSira = fatura.FaturaNoSira;
                guncellenecek.FaturaDovizi = fatura.FaturaDovizi;
                guncellenecek.AraToplam = fatura.AraToplam;
                guncellenecek.KdvToplam = fatura.KdvToplam;
                guncellenecek.GenelToplam = fatura.GenelToplam;
                guncellenecek.KalanBorc = fatura.GenelToplam - tahsilatToplam;


                context.SaveChanges();
                int sonId = guncellenecek.Id;
                return Json(sonId.ToString(), JsonRequestBehavior.AllowGet);
            }
        }

        public ActionResult FaturaSil(int id)
        {
            Fatura guncellenecek = context.Faturas.FirstOrDefault(x => x.Id == id);
            guncellenecek.Silindi = true;
            context.SaveChanges();
            return RedirectToAction("Faturalar", "Panel");
        }

        public ActionResult FaturaDetay(int id)
        {
            ViewBag.Kullanici = Session["Kullanici"];
            ViewBag.Tahsilatlar = context.Islemlers.ToList().Where(x => x.FaturaID == id && x.IslemTuru=="Tahsilat");
            ViewBag.Urunler = context.HizmetUruns.ToList();

            if (ViewBag.Kullanici != null)
            {
                var huFaturaSayısı = context.HizmetUrunFaturas.ToList().Where(x => x.FaturaID == id).Count();
                if (huFaturaSayısı != 0)
                {
                    var list = from ft in context.Faturas
                               join hu in context.HizmetUrunFaturas on ft.Id equals hu.FaturaID into ps
                               from m in ps.DefaultIfEmpty()
                               where ft.Id == id
                               select new
model_fatura_altUrun_liste
                               {
                                   Id = ft.Id,
                                   Aciklama = ft.Aciklama,
                                   MusteriID = ft.MusteriID,
                                   Irsaliye = ft.Irsaliye,
                                   altId = m.Id,
                                   BirimFiyat = m.BirimFiyat,
                                   DuzenlemeTarih = ft.DuzenlemeTarih,
                                   FaturaDovizi = ft.FaturaDovizi,
                                   FaturaNoSeri = ft.FaturaNoSeri,
                                   FaturaNoSira = ft.FaturaNoSira,
                                   HizmetUrunID = m.HizmetUrunID,
                                   KullaniciID = ft.KullaniciID,
                                   Miktar = m.Miktar,
                                   Silindi = ft.Silindi,
                                   VadeTarihi = ft.VadeTarihi,
                                   Vergi = m.Vergi,
                                   Toplam = m.Toplam
                               };
                    ViewBag.list = list;
                }

                return View(context.Faturas.FirstOrDefault(x=>x.Id==id));
            }
            else
            {
                return RedirectToAction("GirisYap", "Uyelik");
            }
        }

        //Hizmet Ürün- Faturalar(faturadaki ürünler)

        [HttpPost]
        [ValidateInput(false)]
        public JsonResult HizmetUrunFaturaEkle(HizmetUrunFatura hizmetUrunFatura)
        {

            try
            {
                if (hizmetUrunFatura.Id == 0)
                {
                    context.HizmetUrunFaturas.Add(hizmetUrunFatura);
                    context.SaveChanges();
                    return Json(true, JsonRequestBehavior.AllowGet);
                }
                else
                {
                    //HizmetUrunFatura guncellenecek = context.HizmetUrunFaturas.FirstOrDefault(x => x.Id == hizmetUrunFatura.Id);
                    //guncellenecek.HizmetUrunID = hizmetUrunFatura.HizmetUrunID;
                    //guncellenecek.Miktar = hizmetUrunFatura.Miktar;
                    //guncellenecek.BirimFiyat = hizmetUrunFatura.BirimFiyat;
                    //guncellenecek.Vergi = hizmetUrunFatura.Vergi;
                    context.HizmetUrunFaturas.Add(hizmetUrunFatura);
                    context.SaveChanges();

                    return Json(true, JsonRequestBehavior.AllowGet);
                }
            }
            catch
            {
                return Json(true, JsonRequestBehavior.AllowGet);
            }

        }

        //Tahsilat
        public ActionResult TahsilatEkle(int id)
        {
            ViewBag.Kullanici = Session["Kullanici"];
            ViewBag.fatID = id;
            ViewBag.Hesaplar = context.KasaBankas.ToList();
            return View();
        }

        [HttpPost]
        [ValidateInput(false)]
        public ActionResult TahsilatEkle(Islemler islemler)
        {
            ViewBag.Kullanici = Session["Kullanici"];
            islemler.KullaniciID = ViewBag.Kullanici.Id;
            //fatura kısmı
            Fatura fat = context.Faturas.FirstOrDefault(x => x.Id == islemler.FaturaID);
            fat.KalanBorc = fat.KalanBorc - islemler.Meblag;

            //hesap kısmı
            KasaBanka kasaBanka = context.KasaBankas.FirstOrDefault(y => y.Id == islemler.HesapID);

            kasaBanka.Bakiye = kasaBanka.Bakiye + islemler.Meblag;

            islemler.IslemTuru = "Tahsilat";

            context.Islemlers.Add(islemler);
            context.SaveChanges();
            return Json(true, JsonRequestBehavior.AllowGet);
        }

        //Kasa ve Bankalar

        public ActionResult KasaBankalar()
        {
            ViewBag.Kullanici = Session["Kullanici"];
            if (ViewBag.Kullanici != null)
            {
                return View(context.KasaBankas.ToList().Where(x=>x.KullaniciID==ViewBag.Kullanici.Id));
            }
            else
            {
                return RedirectToAction("GirisYap", "Uyelik");
            }

        }

            //Kasa
        public ActionResult KasaEkle(int id)
        {
            ViewBag.Kullanici = Session["Kullanici"];

            ViewBag.DovizTur = context.DovizTurus.ToList();
            if (ViewBag.Kullanici != null)
            {
                if (id == 0)
                {
                    return View();
                }
                else
                {
                    KasaBanka kasa = context.KasaBankas.Find(id);
                    return View(kasa);
                }
            }
            else
            {
                return RedirectToAction("GirisYap", "Uyelik");
            }
        }

        [HttpPost]
        [ValidateInput(false)]
        public ActionResult KasaEkle(KasaBanka kasa)
        {
            ViewBag.Kullanici = Session["Kullanici"];
            if (kasa.Id == 0)
            {
                kasa.KullaniciID = ViewBag.Kullanici.Id;
                kasa.HesapTuru = "Kasa";
                kasa.Silindi = false;

                context.KasaBankas.Add(kasa);
                context.SaveChanges();
                return RedirectToAction("KasaBankalar", "Panel");
            }
            else
            {
                KasaBanka guncellenecek = context.KasaBankas.FirstOrDefault(x => x.Id == kasa.Id);
                guncellenecek.HesapIsmi = kasa.HesapIsmi;

                context.SaveChanges();
                return RedirectToAction("KasaBankalar", "Panel");
            }
        }

        public ActionResult KasaDetay(int id)
        {
            ViewBag.Kullanici = Session["Kullanici"];
            ViewBag.tahsilatlar = context.Islemlers.ToList().Where(x => x.HesapID.Value == id);
            var hey = context.KasaBankas.FirstOrDefault(x => x.Id == id);
            if (ViewBag.Kullanici != null)
            {
                return View(hey);
            }
            else
            {
                return RedirectToAction("GirisYap", "Uyelik");
            }
            
        }

            //Banka

        public ActionResult BankaEkle(int id)
        {
            ViewBag.Kullanici = Session["Kullanici"];
            ViewBag.DovizTur = context.DovizTurus.ToList();
            if (ViewBag.Kullanici != null)
            {
                if (id == 0)
                {
                    return View();
                }
                else
                {
                    KasaBanka banka = context.KasaBankas.Find(id);
                    return View(banka);
                }
            }
            else
            {
                return RedirectToAction("GirisYap", "Uyelik");
            }
        }

        [HttpPost]
        [ValidateInput(false)]
        public ActionResult BankaEkle(KasaBanka banka, string hey)
        {
            ViewBag.Kullanici = Session["Kullanici"];
            if (banka.Id == 0)
            {
                banka.KullaniciID = ViewBag.Kullanici.Id;
                banka.HesapTuru = "Banka";
                banka.Silindi = false;
                banka.Bakiye = banka.AcilisBakiyesi;

                context.KasaBankas.Add(banka);
                context.SaveChanges();
                return RedirectToAction("KasaBankalar", "Panel");
            }
            else
            {
                KasaBanka guncellenecek = context.KasaBankas.FirstOrDefault(x => x.Id == banka.Id);
                guncellenecek.HesapIsmi = banka.HesapIsmi;

                context.SaveChanges();
                return RedirectToAction("KasaBankalar", "Panel");
            }
        }

        public ActionResult BankaDetay(int id)
        {
            ViewBag.Kullanici = Session["Kullanici"];
            ViewBag.tahsilatlar = context.Islemlers.OrderByDescending(y=>y.Tarih).ToList().Where(x => x.HesapID == id);
            var hey = context.KasaBankas.FirstOrDefault(x => x.Id == id);
            if (ViewBag.Kullanici != null)
            {
                return View(hey);
            }
            else
            {
                return RedirectToAction("GirisYap", "Uyelik");
            }
            
        }

        public ActionResult KasaBankaSil(int id)
        {
            KasaBanka guncellenecek = context.KasaBankas.FirstOrDefault(x => x.Id == id);
            guncellenecek.Silindi = true;
            context.SaveChanges();

            return RedirectToAction("KasaBankalar", "Panel");
        }

        public ActionResult HesabaParaGirisiEkle(int id)
        {

            return View();
        }

        //FiyatListeleri
        public ActionResult FiyatListe()
        {
            ViewBag.Kullanici = Session["Kullanici"];
            return View(context.FiyatListesis.ToList());
        }
        


        public ActionResult bosislemekle()
        {
            return View();
        }
    }
}


