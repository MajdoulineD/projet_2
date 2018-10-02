using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using projet_2.Models;
using System.Web.Script.Serialization;

namespace projet_2.Controllers
{
    public class ClientController : Controller
    {
        //
        // GET: /Client/
        projetMvcEntities dt = new projetMvcEntities();
        public ActionResult Index()
        {
            ViewBag.ClientNom = Session["ClientName"].ToString();
            ViewBag.ClientID = Session["IdClient"];
            return View("Home");
        }
        public ActionResult ConsulterCtlg()
        {
            
            var listCateg = dt.Categories;
            var model=new Categorie();
            model.ListCategorie = listCateg;
            return View(model);
        }
        public PartialViewResult afficherArticles(Categorie categ)
        {
            var x = from c in dt.Articles  where c.RefCat == categ.RefCat select c;
            var model = x.ToList();
            return PartialView("_ArticleList",model);
        }
        [HttpGet]
        public ActionResult AfficherDetailles(int NumArticle)
        {
            var x = (from a in dt.Articles where a.NumArticle == NumArticle select a).SingleOrDefault();
            return View("View1", x);
        }
        public ActionResult SeDeconnecter()
        {
            Session["ClientName"] = null;
            return Redirect(Url.Action("Index", "Login"));
        }
        public ActionResult VoirPannier()
        {
            projetMvcEntities dt = new projetMvcEntities();
            int numClient = Int32.Parse(Session["IdClient"].ToString());
            int montantTotal = 0;
            List<MonPannier> malist=new List<MonPannier>();
            var x = from c in dt.Articles join d in dt.Commandes on c.NumArticle equals d.NumArticle where d.NumClient == numClient select new { Designation = c.Designation, PrixU = c.PrixU, DateCmd = d.DateCmd, QteArticle = d.QteArticle, photo=c.photo };
            if (x != null)
            {
                foreach (var item in x)
                {
                    MonPannier pann = new MonPannier();
                    pann.DateCmd = item.DateCmd;
                    pann.Designation = item.Designation;
                    pann.photo = item.photo;
                    pann.PrixU = item.PrixU;
                    pann.QteArticle = item.QteArticle;
                    montantTotal += Int32.Parse(item.PrixU) * Int32.Parse(item.QteArticle.ToString());
                    malist.Add(pann);
                   
                }
              //  ViewBag.MontantTotal = "Montant total en DH = " + montantTotal;
            }
            ViewBag.MontantTotal = "Montant total en DH = " +montantTotal;
            var model = malist;
            return View(model);
        }
        public ActionResult Lancercommande()
        {
            projetMvcEntities dt = new projetMvcEntities();
           // ViewBag.Listcateg = new SelectList(dt.Categories, "RefCat", "NomCat");
            ViewBag.Listcateg = dt.Categories.ToList();
            return View();
        }
        public JsonResult GetArticleById(int ID)
        {
            dt.Configuration.ProxyCreationEnabled = false;
            return Json(dt.Articles.Where(p => p.RefCat == ID), JsonRequestBehavior.AllowGet);
        }
        public JsonResult GetStockById(int ID)
        {
            Session["numArticle"] = ID;
            dt.Configuration.ProxyCreationEnabled = false;
            var x = (from a in dt.Articles where a.NumArticle == ID select a).SingleOrDefault();
            var stock=x.stock;
            return Json(stock, JsonRequestBehavior.AllowGet);
        }
        [HttpPost]
        public ActionResult TraiterCommande(MaCommande cmd)
        {

            if (cmd.qtt>cmd.RefCat || cmd.qtt<0)
            {
                ViewBag.qtt = cmd.qtt;
                ViewBag.refc = cmd.RefCat;
                ViewBag.Listcateg = dt.Categories.ToList();
                ViewBag.warn = "La quantité ne doit pas dépasser le stock et ne peut pas etre nulle/negative !!";
                return View("Lancercommande");
            }
            else
            {
                Commande commande = new Commande();
                commande.DateCmd = DateTime.Now.ToString("dd/MM/yyyy");
                commande.NumClient = Int32.Parse(Session["IdClient"].ToString());
                commande.NumArticle = Int32.Parse(Session["numArticle"].ToString());
                commande.QteArticle = cmd.qtt;
                dt.Commandes.Add(commande);
                dt.SaveChanges();
                var y = (from a in dt.Articles where a.NumArticle == commande.NumArticle select a).SingleOrDefault();
                int newStk = cmd.RefCat - cmd.qtt;
                y.stock = newStk.ToString();
                dt.SaveChanges();
                ViewBag.Listcateg = dt.Categories.ToList();
                ViewBag.warn = "Votre commande a reussi";
                return View("Lancercommande");
            }
           
        }
       

    }
}
