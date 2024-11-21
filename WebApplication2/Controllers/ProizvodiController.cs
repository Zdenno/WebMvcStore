using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Entity;
using System.Linq;
using System.Net;
using System.Web;
using System.Web.Mvc;
using WebApplication2.Models;
using System.IO;
using System.Threading.Tasks;

namespace WebApplication2.Controllers
{
    [AllowAnonymous]
    public class ProizvodiController : Controller
    {
        private MagacinContext db = new MagacinContext();

        public async Task<ActionResult> RenderImage(int id)
        {
            Proizvod p = await db.Proizvods.FindAsync(id);

            byte[] slika = p.Slika;

            return File(slika, "image/png/jpg");
        }
        //public FileContentResult CitajSliku (int id)
        //{
        //    //Proizvod sl = db.Proizvods.Find();
        //    Proizvod sl = db.Proizvods.First(f => f.Kategorije.KategorijaId == id); //takto robi
        //   //Proizvod sl = db.Proizvods.First(f => f.ProizvodId == id);
        //    //Proizvod sl = db.Proizvods.Where(f => f.Slika == id);

        //    //Proizvod sl = db.Proizvods.Find(id);
        //    //Kategorija k = db.Kategorijas.Find(2);
        //    if (sl == null)
        //    {
        //        return null;
        //    }
        //    return File(sl.Slika,sl.Slika.GetType().ToString());
        //}
        // GET: Proizvodi
        [AllowAnonymous]
        public ActionResult Index()
        {
            var proizvods = db.Proizvods.Include(p => p.Kategorije);
            //ViewBag.Kategorije = db.Kategorijas.ToList(); SSSSS
            return View(proizvods.ToList());
        }

        [AllowAnonymous]
        public PartialViewResult _VratiProizvode(string deoNaslova)
        {
            IQueryable<Proizvod> listaProizvoda = db.Proizvods.Select(p => p);
            
            if (!string.IsNullOrWhiteSpace(deoNaslova))
            {
                listaProizvoda = listaProizvoda.Where(p => p.NazivProizvodjaca.Contains(deoNaslova)).Select(p => p);
            }

            return PartialView(listaProizvoda.ToList());
        }
        // GET: Proizvodi/Details/5
        [AllowAnonymous]
        public ActionResult Details(int? id)
        {              

            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Proizvod proizvod = db.Proizvods.Find(id);
            if (proizvod == null)
            {
                return HttpNotFound();
            }
            return View(proizvod);
        }

        // GET: Proizvodi/Create
        [Authorize]
        public ActionResult Create()
        {
            
            
            ViewBag.KategorijaId = db.Kategorijas.ToList(); 
            //ViewBag.KategorijaId = new SelectList(db.Kategorijas,"KategorijaId", "NazivKategorije"); 
            return View();
        }

        // POST: Proizvodi/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [Authorize]
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create([Bind(Include = "ProizvodId,KategorijaId,NazivProizvodjaca,Cena,KolicinaNaLageru,Slika,OglasPostavio")] Proizvod p, HttpPostedFileBase poslataSlika)
        {
            if (poslataSlika != null)
            {
                //p.NazivProizvodjaca = poslataSlika.ContentType; //  
                p.Slika = new byte[poslataSlika.ContentLength];  
                Stream fs = poslataSlika.InputStream;
                fs.Read(p.Slika, 0, poslataSlika.ContentLength);
            }
            
            if (ModelState.IsValid)
            {
                db.Proizvods.Add(p);
                db.SaveChanges();
                return RedirectToAction("Index");
            }
           
            ViewBag.Kategorija = new SelectList(db.Kategorijas, "KategorijaId", "NazivKategorije", p.KategorijaId);  
            return View(p);
        }


        // GET: Proizvodi/Edit/5
        [Authorize]
        public ActionResult Edit(int? id)
        {
            ViewBag.Kategorije = db.Kategorijas.ToList();
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Proizvod proizvod = db.Proizvods.Find(id);
            if (proizvod == null)
            {
                return HttpNotFound();
            }
            ViewBag.KategorijaId = id;
            return View(proizvod);
        }

        
        [Authorize]
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include = "ProizvodId,KategorijaId,NazivProizvodjaca,Cena,KolicinaNaLageru,Slika,OglasPostavio")] Proizvod proizvod,HttpPostedFileBase poslatFajl, int promena)
        {
            //ViewBag.Kategorije = db.Kategorijas.ToList();
             
            if (ModelState.IsValid)
            {

                Proizvod slAtached = db.Proizvods.Find(proizvod.ProizvodId);
                if (promena == 1 && poslatFajl != null)
                {
                    //slAtached.Slika = (byte)poslatFajl.ContentType.ToString();
                    slAtached.Slika = new byte[poslatFajl.ContentLength];
                    Stream s = poslatFajl.InputStream;
                    s.Read(slAtached.Slika, 0, poslatFajl.ContentLength);
                }
                slAtached.KategorijaId = proizvod.KategorijaId;
                //slAtached.ProizvodId = proizvod.ProizvodId;
                slAtached.NazivProizvodjaca = proizvod.NazivProizvodjaca;
                slAtached.Cena = proizvod.Cena;
                slAtached.KolicinaNaLageru = proizvod.KolicinaNaLageru;
                //slAtached.Slika = proizvod.Slika;
                slAtached.OglasPostavio = slAtached.OglasPostavio;                   

                //db.Entry(proizvod).State = EntityState.Modified;  
                db.SaveChanges();
                return RedirectToAction("Index");
            }
           
            return View(proizvod);
        }

        // GET: Proizvodi/Delete/5
        [Authorize]
        public ActionResult Delete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Proizvod proizvod = db.Proizvods.Find(id);
            if (proizvod == null)
            {
                return HttpNotFound();
            }
            return View(proizvod);
        }

        // POST: Proizvodi/Delete/5
        [Authorize]
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int id)
        {
            Proizvod proizvod = db.Proizvods.Find(id);
            db.Proizvods.Remove(proizvod);
            db.SaveChanges();
            return RedirectToAction("Index");
        }

        protected override void Dispose(bool disposing)
        {
            if (disposing)
            {
                db.Dispose();
            }
            base.Dispose(disposing);
        }
    }
}
