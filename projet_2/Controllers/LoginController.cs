using projet_2.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace projet_2.Controllers
{
    public class LoginController : Controller
    {
        //
        // GET: /Login/

        public ActionResult Index()
        {
            ViewBag.Warning = "";
            if (Session["style"]!=null)
            {
                if (Session["style"].ToString().Equals("on"))
                {
                    ViewBag.style = "";
                }
                else
                {
                    ViewBag.style = "display: none;";

                }
            }
            else
            {
                ViewBag.style = "display: none;";
            }
            
            Session["style"] ="off";
            return View();
        }
        public ActionResult InscriptionForm()
        {
            return View("Inscription");
        }

        [HttpPost]
        public ActionResult FormTraitement(Client clt)
        {
            projetMvcEntities dt = new projetMvcEntities();

            var x = (from c in dt.Clients where c.login == clt.login && c.MotPasse==clt.MotPasse select c).SingleOrDefault();
            if (x == null)
            {
                Session["style"] = "off";
                ViewBag.Warning = "Votre Login et/ou mot de passe est incorrecte !";
                ViewBag.style = "display: none;";
                return View("Index");
            }
            else
            {
                if(x.login=="Admin"&&x.MotPasse=="12345"){
                    Session["IdClient"] = x.NumClient;
                    Session["ClientName"] = clt.login;
                    return Redirect(Url.Action("Index", "Admin"));
                }
                else
                {
                    Session["IdClient"] = x.NumClient;
                    Session["ClientName"] = clt.login;
                    return Redirect(Url.Action("Index", "Client"));
                }
               
            }
        }
        public ActionResult Inscription(Client clt)
        {
            projetMvcEntities dt = new projetMvcEntities();
            var x = (from c in dt.Clients where c.login == clt.login select c).SingleOrDefault();
            if (Request.Form["submitform"] != null)
            {
                if (ModelState.IsValid)
                {
                    dt.Clients.Add(clt);
                    var test=dt.SaveChanges();
                    if (test != 0)
                    {
                        Session["style"] = "on";
                        return JavaScript("window.location = '" + Url.Action("Index", "Login") + "'");
                    }
                    else
                    {
                        ViewBag.logExist = "Erreur de base de donnée !";
                        return PartialView("_Partial1");
                    }
                    
                }
                else
                {
                    return PartialView("_Partial2");
                }
            }
            else
            {
                if (x != null)
                {
                    ViewBag.logExist = "ce login existe déjà !";
                    return PartialView("_Partial1");
                }
                else
                {
                    ViewBag.logExist = "";
                    return PartialView("_Partial1");
                }
                
            }
        }
       

    }
}
