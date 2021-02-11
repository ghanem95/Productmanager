using ProductManagement.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace ProductManagement.Controllers
{
    public class CommandeController : Controller
    {
        // GET: Commande
        public ActionResult Index()
        {
            Commande commande = new Commande();
            ViewBag.ListCmd = commande.List();
            Tuple<Commande, Product, Client> tuple = new Tuple<Commande, Product, Client>(commande, new Product(), new Client());
            return View("Index", tuple);
        }

        public PartialViewResult DeleteCmd(int id)
        {
            Commande commande = new Commande();
            ViewBag.msg= commande.Delete(id);
            ViewBag.ListCmd = commande.List();
            return PartialView("_Listcmd");
        }

        public PartialViewResult DetailCmd(int id)
        {
            Commande commande= new Commande();
            commande.Affiche(id);
            Tuple<Commande, Product, Client> tuple = new Tuple<Commande, Product, Client>(commande, new Product(), new Client());
            return PartialView("_Savecmd", tuple);
        }
        public ActionResult Save([Bind(Prefix = "Item1")] Commande commande)
        {
            if(commande.Idclt==0)
            {
                commande.Idclt =Convert.ToInt32(Session["user"]);
            }
            if (commande.Id != 0)
            {
                commande.Update();
            }
            else
            {
                commande.Add();
            }

            return RedirectToAction("Index");
        }
    }
}