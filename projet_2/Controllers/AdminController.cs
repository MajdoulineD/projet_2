using projet_2.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace projet_2.Controllers
{
    public class AdminController : Controller
    {
        //
        // GET: /Admin/
        projetMvcEntities dt = new projetMvcEntities();
        public ActionResult Index()
        {
            return View();
        }

    }
}
