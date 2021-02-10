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
            Design design = new Design();
            design.Id_user =Convert.ToInt32(Session["user"]);
            design.Affiche(1);
            Session["design"] = design.Designs;
            Session["datatable"] = design.Datatable;
            Session["btn"] = design.Btn;
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
        public JsonResult Updatedesign(int id,string design,string datatable, string btn)
        {
            Design designs = new Design();
            designs.Id_user = id;
            designs.Designs = design;
            designs.Datatable = datatable;
            designs.Btn = btn;
            designs.Update();
            Session["design"] = designs.Designs;
            Session["datatable"] = designs.Datatable;
            Session["btn"] = designs.Btn;
            var response = new { data = designs };
            return Json(response, JsonRequestBehavior.AllowGet);
        }
        public ActionResult Contact()
        {
            ViewBag.Message = "Your contact page.";

            return View();
        }

        public JsonResult GetStats()
        {
            IList<Stat> stats;
            Stat stat = new Stat();
            stats = stat.Listproductsaled();
            var response = new { data = stats };
            return Json(response, JsonRequestBehavior.AllowGet);
        }
        public JsonResult Nbprodbytype()
        {
            IList<Stat> stats;
            Stat stat = new Stat();
            stats = stat.Nbproductbytype();
            var response = new { data = stats };
            return Json(response, JsonRequestBehavior.AllowGet);
        }
    }
}