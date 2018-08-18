using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Entity;
using System.Linq;
using System.Net;
using System.Web;
using System.Web.Mvc;
using WebApplication2.Models;

namespace WebApplication2.Controllers
{
    [Authorize]
    public class KategorijeController : Controller
    {

        private MagacinContext db = new MagacinContext();

        // GET: Kategorije
        [AllowAnonymous]
        public ActionResult Index()
        {
            return View(db.Kategorijas.ToList());
        }
        [AllowAnonymous]
        public PartialViewResult _VratiKategorije(string deoNaslova)
        {
            IQueryable<Kategorija> listaKat = db.Kategorijas.Select(k => k );           


                if (!string.IsNullOrWhiteSpace(deoNaslova))
                {
                    listaKat = listaKat.Where(k => k.NazivKategorije.Contains(deoNaslova));
                }
            
            return PartialView(listaKat.ToList());

        }

        // GET: Kategorije/Details/5
        [AllowAnonymous]
        public ActionResult Details(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Kategorija kategorija = db.Kategorijas.Find(id);
            if (kategorija == null)
            {
                return HttpNotFound();
            }
            return View(kategorija);
        }


      
       public ActionResult Ubaci()
        {
            Kategorija k = new Kategorija();
            k.DatumKreiranja = DateTime.Now;  
            return View();
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Ubaci([Bind(Include = "KategorijaId, NazivKategorije, OpisKategorije, DatumKreiranja")]Kategorija k)
        {
            if (ModelState.IsValid)
            {
                db.Kategorijas.Add(k);
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            return View(k);
        }


       
        // GET: Kategorije/Edit/5
        public ActionResult Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Kategorija kategorija = db.Kategorijas.Find(id);
            if (kategorija == null)
            {
                return HttpNotFound();
            }
            return View(kategorija);
        }

        // POST: Kategorije/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include = "KategorijaId,NazivKategorije,OpisKategorije,DatumKreiranja")] Kategorija kategorija)
        {
            if (ModelState.IsValid)
            {
                db.Entry(kategorija).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            return View(kategorija);
        }

        // GET: Kategorije/Delete/5
        public ActionResult Delete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Kategorija kategorija = db.Kategorijas.Find(id);
            if (kategorija == null)
            {
                return HttpNotFound();
            }
            return View(kategorija);
        }

        // POST: Kategorije/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int id)
        {
            Kategorija kategorija = db.Kategorijas.Find(id);
            db.Kategorijas.Remove(kategorija);
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
