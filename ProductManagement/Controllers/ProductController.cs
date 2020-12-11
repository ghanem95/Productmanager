using ProductManagement.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace ProductManagement.Controllers
{
    public class ProductController : Controller
    {
        // GET: Product
        public ActionResult Index()
        {
            Product product = new Product();
            ViewBag.Listpdt = product.List();
            Tuple<Product, ProductManagement.Models.Type> tuple = new Tuple<Product, ProductManagement.Models.Type>(product, new ProductManagement.Models.Type());
            return View("Index", tuple);
        }

        public PartialViewResult Deletepdt(int id)
        {
            ProductManagement.Models.Product product = new ProductManagement.Models.Product();
           ViewBag.msg= product.Delete(id);
            ViewBag.Listpdt = product.List();

            return PartialView("_Listpdt");
        }

        public PartialViewResult Detailpdt(int id)
        {
            Product product = new Product();
            product.Affiche(id);
            Tuple<Product, ProductManagement.Models.Type> tuple = new Tuple<Product, ProductManagement.Models.Type>(product, new ProductManagement.Models.Type());
            return PartialView("_Detailpdt", tuple);
        }
        public ActionResult Addproduct()
        {
            Tuple<Product, ProductManagement.Models.Type> tuple = new Tuple<Product, ProductManagement.Models.Type>(new Product(), new ProductManagement.Models.Type());
            return View("Addproduct", tuple);
        }

        public ActionResult Save([Bind(Prefix = "Item1")] Product product)
        {
            if (product.Id != 0)
            {
                product.Update();
            }
            else
            {
                product.Add();
            }

            return RedirectToAction("Index");
        }
    }
}