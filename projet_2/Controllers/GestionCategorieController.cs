using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Entity;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using projet_2.Models;

namespace projet_2.Controllers
{
    public class GestionCategorieController : Controller
    {
        private projetMvcEntities db = new projetMvcEntities();

        //
        // GET: /GestionCategorie/

        public ActionResult Index()
        {
            return View(db.Categories.ToList());
        }

        //
        // GET: /GestionCategorie/Details/5

        public ActionResult Details(int id = 0)
        {
            Categorie categorie = db.Categories.Find(id);
            if (categorie == null)
            {
                return HttpNotFound();
            }
            return View(categorie);
        }

        //
        // GET: /GestionCategorie/Create

        public ActionResult Create()
        {
            return View();
        }

        //
        // POST: /GestionCategorie/Create

        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create(Categorie categorie)
        {
            if (ModelState.IsValid)
            {
                db.Categories.Add(categorie);
                db.SaveChanges();
                return RedirectToAction("Index");
            }

            return View(categorie);
        }

        //
        // GET: /GestionCategorie/Edit/5

        public ActionResult Edit(int id = 0)
        {
            Categorie categorie = db.Categories.Find(id);
            if (categorie == null)
            {
                return HttpNotFound();
            }
            return View(categorie);
        }

        //
        // POST: /GestionCategorie/Edit/5

        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit(Categorie categorie)
        {
            if (ModelState.IsValid)
            {
                db.Entry(categorie).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            return View(categorie);
        }

        //
        // GET: /GestionCategorie/Delete/5

        public ActionResult Delete(int id = 0)
        {
            Categorie categorie = db.Categories.Find(id);
            if (categorie == null)
            {
                return HttpNotFound();
            }
            return View(categorie);
        }

        //
        // POST: /GestionCategorie/Delete/5

        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int id)
        {
            Categorie categorie = db.Categories.Find(id);
            db.Categories.Remove(categorie);
            db.SaveChanges();
            return RedirectToAction("Index");
        }

        protected override void Dispose(bool disposing)
        {
            db.Dispose();
            base.Dispose(disposing);
        }
    }
}