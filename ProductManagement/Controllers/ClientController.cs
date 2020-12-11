using ProductManagement.Models;
using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace ProductManagement.Controllers
{
    public class ClientController : Controller
    {
        // GET: Client
        public ActionResult Index()
        {
            Client clt = new Client();
            ViewBag.ListClient = clt.List();
            return View();
        }
        public ActionResult AddClt()
        {

            return View();
        }
        public PartialViewResult DeleteClt(int idclt)
        {
            Client client = new Client();
            ViewBag.msg= client.Delete(idclt);
            ViewBag.ListClient = client.List();
            return PartialView("_Listclt");
        }
        public PartialViewResult DetailClt(int idclt)
        {
            Client client = new Client();
            client.Affiche(idclt);
            return PartialView("_Detailclt", client);
        }
        public ActionResult Save(Client client)
        {
            if (client.Id != 0)
            {
                client.Update();
            }
            else
            {
                client.Add();
            }

            return RedirectToAction("Index");
        }
    }
}