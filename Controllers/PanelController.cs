using System;
using Muhasebe.Models;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

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


                    //var kategoriler2 = context.Kategoris.ToList().Where(x=>x.;
                    //ViewBag.kategoriler = kategoriler2;


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
        public ActionResult MusteriEkle(Musteri musteri, string kategoriler, string iban)
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
                Iban ib = new Iban
                {
                    IbanNo = iban
                };
                context.Ibans.Add(ib);

                musteri.KullaniciID = ViewBag.Kullanici.Id;
                musteri.Silindi = false;
                context.Musteris.Add(musteri);
                context.SaveChanges();
                return RedirectToAction("MusteriEkle", "Panel");
            }
            else
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
                //Iban ib = new Iban();
                //ib.IbanNo = iban;
                //context.Ibans.Add(ib);

                Musteri guncellenecek = context.Musteris.FirstOrDefault(x => x.Id == musteri.Id);
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
                    HizmetUrun Hu = context.HizmetUruns.Find(id);
                    List<string> stringlist = new List<string>();
                    //foreach (var item in Hu.Kategoris)
                    //{


                    //    stringlist.Add(item.Ad);
                    //    string dogCsv = string.Join(", ", stringlist.ToArray());
                    //    Console.WriteLine(dogCsv);

                    //    ViewBag.katttt = dogCsv;

                    //}


                    //var kategoriler2 = context.Kategoris.ToList().Where(x=>x.;
                    //ViewBag.kategoriler = kategoriler2;


                    return View(mus);
                }
            }
            else
            {
                return RedirectToAction("GirisYap", "Uyelik");
            }

        }
    }
}