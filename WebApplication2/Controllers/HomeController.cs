using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Mail;
using System.Web;
using System.Web.Mvc;
using WebApplication2.Models;

namespace WebApplication2.Controllers
{
    public class HomeController : Controller
    {
        public ActionResult Index()
        {
            return View();
        }

        public ActionResult About()
        {
            ViewBag.Message = "Your application description page.";

            return View();
        }

      

        public ActionResult Uspesno()
        {
            return View();
        }

        public ActionResult Greska()
        {
            return View();
        }




        public ActionResult Kontakt()
        { 
            return View();
        }
             
        [HttpPost]
        public ActionResult PosaljiMail(Kontakt k)
        {
            if (ModelState.IsValid)
            {
                try
                {
                    MailAddress korisnik = new MailAddress(k.Email, k.Ime);
                    MailAddress primaoc = new MailAddress("zdenn1@hotmail.com");
                    MailMessage poruka = new MailMessage();


                    poruka.From = korisnik;
                    poruka.To.Add(primaoc);
                    poruka.Subject = "Poruka od " + k.Email;
                    poruka.Body = k.Poruka;
                    poruka.IsBodyHtml = true;

                    SmtpClient mailClient = new SmtpClient("smtp.gmail.com");
                    mailClient.EnableSsl = true;
                    mailClient.Port = 587;
                    mailClient.Credentials = new NetworkCredential("joanatest90@gmail.com", "Joanatest");
                    mailClient.Send(poruka);
                    return Redirect("Uspesno");

                }
                catch (Exception)
                {
                    return Redirect("Greska");
                }
            }
            else
            {
                return Redirect("PosaljiMail");
            }
        }

    }
}