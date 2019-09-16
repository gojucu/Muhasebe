using Muhasebe.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using System.Web.Security;

namespace Muhasebe.Controllers
{
    public class UyelikController : Controller
    {
        // GET: Uyelik
        MuhasebeModel context = new MuhasebeModel();
        public ActionResult KayitOl()
        {
            return View();
        }
        [HttpPost]
        public ActionResult KayitOl(Kullanici kullanici, string Parola)
        {
            MembershipUser user = Membership.CreateUser(kullanici.KullaniciAdi, Parola, kullanici.Mail);
            kullanici.Id = (Guid)user.ProviderUserKey;
            Session["Kullanici"] = kullanici;
            kullanici.DogumTarihi = DateTime.Now;

            context.Kullanicis.Add(kullanici);
            context.SaveChanges();

            FormsAuthentication.RedirectFromLoginPage(kullanici.KullaniciAdi, true);
            Session["Kullanici"] = kullanici;
            return RedirectToAction("Index", "Panel");
        }

        public ActionResult GirisYap()
        {
            ViewBag.Kullanici = Session["Kullanici"];
            return View();
        }

        [HttpPost]
        public ActionResult GirisYap(string kullaniciAdi, string Parola)
        {
            if (Membership.ValidateUser(kullaniciAdi, Parola))
            {
                FormsAuthentication.RedirectFromLoginPage(kullaniciAdi, true);
                Kullanici kullanici = context.Kullanicis.FirstOrDefault(x => x.KullaniciAdi == kullaniciAdi);
                Session["Kullanici"] = kullanici;
                Session["KullaniXID"] = kullanici.Id;

                string gor = kullanici.Id.ToString();

               return RedirectToAction("Index", "Panel");
            }
            else
            {
                ViewBag.Mesaj = "Kullanıcı Adı Veya Parola Yanlış";
                return View();
            }

        }
        public ActionResult Cikis()
        {

            Session["Kullanici"] = null;
            return RedirectToAction("GirisYap", "Uyelik");
        }

    }
}