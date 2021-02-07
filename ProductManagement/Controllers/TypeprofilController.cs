using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace ProductManagement.Controllers
{
    public class TypeprofilController : Controller
    {
        // GET: Typeprofil
        public ActionResult Index()
        {
            ProductManagement.Models.Typeprofil type = new ProductManagement.Models.Typeprofil();
            ViewBag.Listtype = type.List();
            return View();
        }
        public PartialViewResult Deletetype(int id)
        {
            ProductManagement.Models.Typeprofil type = new ProductManagement.Models.Typeprofil();
            ViewBag.msg = type.Delete(id);
            ViewBag.Listtype = type.List();
            return PartialView("_Listtype");
        }

        public PartialViewResult Detailtype(int id)
        {
            ProductManagement.Models.Typeprofil type = new ProductManagement.Models.Typeprofil();
            type.Affiche(id);
            return PartialView("_Addtype", type);
        }
        public ActionResult Save(ProductManagement.Models.Typeprofil type)
        {
            if (type.Id != 0)
            {
                type.Update();
            }
            else
            {
                type.Add();
            }

            return RedirectToAction("Index");
        }
    }
}