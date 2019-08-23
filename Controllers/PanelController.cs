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


            var kategoriler = context.Kategoris.ToList();

            ViewBag.kategoriler = kategoriler;
            if (id == 0)
            {
                if (ViewBag.Kullanici != null)
                {
                    return View();
                }
                else
                {
                    return RedirectToAction("GirisYap", "Uyelik");
                }
            }
            else
            {
                Musteri mus = context.Musteris.Find(id);
                return View(mus);
            }

        }

        [HttpPost]
        [ValidateInput(false)]
        public ActionResult MusteriEkle(Musteri musteri, string kategoriler, string iban)
        {
            ViewBag.Kullanici = Session["Kullanici"];

            //Kategoriler
            if (musteri.Id == 0)
            {
                string[] kategoris = kategoriler.Split(',');
                foreach (string kategori in kategoris)
                {
                    Kategori kat = context.Kategoris.FirstOrDefault(x => x.Ad.ToLower() == kategori.ToLower().Trim());
                    if (kat == null)
                    {
                        kat = new Kategori();
                        kat.Ad = kategori;
                        context.Kategoris.Add(kat);
                        context.SaveChanges();

                    }
                    musteri.Kategoris.Add(kat);
                    context.SaveChanges();
                }

                //iban
                Iban ib = new Iban();
                ib.IbanNo = iban;
                context.Ibans.Add(ib);

                musteri.KullaniciID = ViewBag.Kullanici.Id;
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
                        kat = new Kategori();
                        kat.Ad = kategori;
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
        public ActionResult MusteriSil()
        {
            return View();
        }

    }
}