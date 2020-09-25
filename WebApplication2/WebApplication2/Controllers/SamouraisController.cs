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

            CreateSamouraiView CNewS = new CreateSamouraiView
            {
                Samourai = new Samourai
                {
                    Id = samourai.Id,
                    Nom = samourai.Nom,
                    Force = samourai.Force,
                    Arme = samourai.Arme,
                    ArtMartials = samourai.ArtMartials,
                    Potentiel = samourai.Potentiel
                }
            };
            return View(CNewS);
        }

        // GET: Samourais/Create
        public ActionResult Create()
        {
            CreateSamouraiView CNewS = new CreateSamouraiView();
            List<int> ListIdArme = db.Samourais.Where(a => a.Arme != null).Select(x => x.Arme.Id).ToList();
            CNewS.ListeArmes = db.Armes.Where(x => !ListIdArme.Contains(x.Id)).ToList();
            CNewS.ArtMartials = db.ArtMartials.ToList();
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
                if(CNewS.IdArtsMartiaux.Count > 0)
                {
                    CNewS.Samourai.ArtMartials = db.ArtMartials.Where(x => CNewS.IdArtsMartiaux.Contains(x.Id)).ToList();
                } else
                {
                    CNewS.Samourai.ArtMartials = null;
                }
                CNewS.Samourai.Potentiel = (CNewS.Samourai.Force + CNewS.Samourai.Arme.Degats) * (CNewS.Samourai.ArtMartials.Count + 1);
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
                    Arme = samourai.Arme,
                    ArtMartials = samourai.ArtMartials
                }
            };

            List<int> ListIdArme = db.Samourais.Where(a => a.Arme != null).Select(x => x.Arme.Id).ToList();
            CNewS.ListeArmes = db.Armes.Where(x => !ListIdArme.Contains(x.Id)).ToList();
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
                if (CEditS.IdArtsMartiaux.Count > 0)
                {
                    CEditS.Samourai.ArtMartials = db.ArtMartials.Where(x => CEditS.IdArtsMartiaux.Contains(x.Id)).ToList();
                }
                else
                {
                    CEditS.Samourai.ArtMartials = null;
                }
                CEditS.Samourai.Potentiel = (CEditS.Samourai.Force + CEditS.Samourai.Arme.Degats) * (CEditS.Samourai.ArtMartials.Count + 1);
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
            return View(CNewS);
        }

        // POST: Samourais/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(CreateSamouraiView delateO)
        {
            Samourai samourai = db.Samourais.Find(delateO.Samourai.Id);
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
