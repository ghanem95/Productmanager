using ProductManagement.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace ProductManagement.Controllers
{
    public class TypeproductController : Controller
    {
        // GET: Typeproduct
        public ActionResult Index()
        {
            ProductManagement.Models.Type type = new ProductManagement.Models.Type();
            ViewBag.Listtype = type.List();
            return View();
        }


        public PartialViewResult Deletetype(int id)
        {
            ProductManagement.Models.Type type = new ProductManagement.Models.Type();
            ViewBag.msg= type.Delete(id);
            ViewBag.Listtype = type.List();
            return PartialView("_Listtype");
        }

        public PartialViewResult Detailtype(int id)
        {
            ProductManagement.Models.Type type = new ProductManagement.Models.Type();
            type.Affiche(id);
            return PartialView("_Addtype", type);
        }
        public ActionResult Save(ProductManagement.Models.Type type)
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