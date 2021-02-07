using ProductManagement.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace ProductManagement.Controllers
{
    public class HomeController : Controller
    {
        public ActionResult Index()
        {
            if (Session["name"] != null)
            {
                if (Convert.ToInt32(Session["typeprofil"]) == 1)
                {
                    Commande commande = new Commande();
                    ViewBag.nbcmd = commande.countcmd();
                    Client client = new Client();
                    ViewBag.nbclt = client.countclt();
                    Product product = new Product();
                    ViewBag.nbpdt = product.countpdt();
                    Models.Type type = new Models.Type();
                    ViewBag.nbtype = type.counttype();
                    return View();
                }
                else
                {
                    return RedirectToAction("Index", "Commande");
                }
              
            }
            else
            {
                return RedirectToAction("Login", "Login");
            }
       
        }

        public ActionResult About()
        {
            ViewBag.Message = "Your application description page.";

            return View();
        }

        public ActionResult Contact()
        {
            ViewBag.Message = "Your contact page.";

            return View();
        }
    }
}