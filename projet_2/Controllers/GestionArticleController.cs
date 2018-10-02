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
    public class GestionArticleController : Controller
    {
        private projetMvcEntities db = new projetMvcEntities();

        //
        // GET: /GestionArticle/

        public ActionResult Index()
        {
            var articles = db.Articles.Include(a => a.Categorie);
            return View(articles.ToList());
        }

        //
        // GET: /GestionArticle/Details/5

        public ActionResult Details(int id = 0)
        {
            Article article = db.Articles.Find(id);
            if (article == null)
            {
                return HttpNotFound();
            }
            return View(article);
        }

        //
        // GET: /GestionArticle/Create

        public ActionResult Create()
        {
            ViewBag.RefCat = new SelectList(db.Categories, "RefCat", "NomCat");
            return View();
        }

        //
        // POST: /GestionArticle/Create

        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create(Article article)
        {
            if (ModelState.IsValid)
            {
                db.Articles.Add(article);
                db.SaveChanges();
                return RedirectToAction("Index");
            }

            ViewBag.RefCat = new SelectList(db.Categories, "RefCat", "NomCat", article.RefCat);
            return View(article);
        }

        //
        // GET: /GestionArticle/Edit/5

        public ActionResult Edit(int id = 0)
        {
            Article article = db.Articles.Find(id);
            if (article == null)
            {
                return HttpNotFound();
            }
            ViewBag.RefCat = new SelectList(db.Categories, "RefCat", "NomCat", article.RefCat);
            return View(article);
        }

        //
        // POST: /GestionArticle/Edit/5

        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit(Article article)
        {
            if (ModelState.IsValid)
            {
                db.Entry(article).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            ViewBag.RefCat = new SelectList(db.Categories, "RefCat", "NomCat", article.RefCat);
            return View(article);
        }

        //
        // GET: /GestionArticle/Delete/5

        public ActionResult Delete(int id = 0)
        {
            Article article = db.Articles.Find(id);
            if (article == null)
            {
                return HttpNotFound();
            }
            return View(article);
        }

        //
        // POST: /GestionArticle/Delete/5

        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int id)
        {
            Article article = db.Articles.Find(id);
            db.Articles.Remove(article);
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