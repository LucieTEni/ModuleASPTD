using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Entity;
using System.Linq;
using System.Net;
using System.Web;
using System.Web.Mvc;
using BO;
using WebApplication2.Data;
using WebApplication2.Models;

namespace WebApplication2.Controllers
{
    public class SamouraisController : Controller
    {
        private WebApplication2Context db = new WebApplication2Context();

        // GET: Samourais
        public ActionResult Index()
        {
            return View(db.Samourais.ToList());
        }

        // GET: Samourais/Details/5
        public ActionResult Details(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Samourai samourai = db.Samourais.Find(id);
            if (samourai == null)
            {
                return HttpNotFound();
            }
            return View(samourai);
        }

        // GET: Samourais/Create
        public ActionResult Create()
        {
            CreateSamouraiView CNewS = new CreateSamouraiView();
            CNewS.ListeArmes = db.Armes.ToList();
            return View(CNewS);
        }

        // POST: Samourais/Create
        // Afin de déjouer les attaques par survalidation, activez les propriétés spécifiques auxquelles vous voulez établir une liaison. Pour 
        // plus de détails, consultez https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create(CreateSamouraiView CNewS)
        {
            
            if (ModelState.IsValid)
            {
                if (CNewS.IdArmes != null)
                {
                    Arme arme = db.Armes.FirstOrDefault(i => i.Id == CNewS.IdArmes);
                    CNewS.Samourai.Arme = arme;
                }
                else
                {
                    CNewS.Samourai.Arme = null;
                }
                db.Samourais.Add(CNewS.Samourai);
                db.SaveChanges();
                return RedirectToAction("Index");
            }

            return View(CNewS);
        }

        // GET: Samourais/Edit/5
        public ActionResult Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Samourai samourai = db.Samourais.Find(id);
            if (samourai == null)
            {
                return HttpNotFound();
            }
            CreateSamouraiView CNewS = new CreateSamouraiView
            {
                Samourai = new Samourai
                {
                    Id = samourai.Id,
                    Nom = samourai.Nom,
                    Force = samourai.Force,
                    Arme = samourai.Arme
                }
            };

            CNewS.ListeArmes = db.Armes.ToList();
            return View(CNewS);
        }

        // POST: Samourais/Edit/5
        // Afin de déjouer les attaques par survalidation, activez les propriétés spécifiques auxquelles vous voulez établir une liaison. Pour 
        // plus de détails, consultez https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit(CreateSamouraiView CEditS)
        {
            if (ModelState.IsValid)
            {
                Samourai samourai = db.Samourais.Find(CEditS.Samourai.Id);
                samourai.Nom = CEditS.Samourai.Nom;
                samourai.Force = CEditS.Samourai.Force;
                if(CEditS.IdArmes != null)
                {
                    Arme arme = db.Armes.FirstOrDefault(i => i.Id == CEditS.IdArmes);
                    samourai.Arme = arme;
                } else
                {
                    samourai.Arme = null;
                }
                
                db.Entry(samourai).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            return View(CEditS);
        }

        // GET: Samourais/Delete/5
        public ActionResult Delete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Samourai samourai = db.Samourais.Find(id);
            if (samourai == null)
            {
                return HttpNotFound();
            }
            return View(samourai);
        }

        // POST: Samourais/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int id)
        {
            Samourai samourai = db.Samourais.Find(id);
            db.Samourais.Remove(samourai);
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
