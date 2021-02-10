using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using ProductManagement.Models;
namespace ProductManagement.Controllers
{
    public class ComplaintController : Controller
    {
        // GET: Complaint
        public ActionResult Index()
        {
            Tuple<Complaint, Complaint_type, Product> tuple = new Tuple<Complaint, Complaint_type, Product>(new Complaint(), new Complaint_type(), new Product());
            return View("Index",tuple);
        }
        public ActionResult Add()
        {
            Tuple<Complaint, Complaint_type, Product> tuple = new Tuple<Complaint, Complaint_type, Product>(new Complaint(), new Complaint_type(), new Product());
            return View("Add", tuple);
        }
        public ActionResult Save([Bind(Prefix = "Item1")] Complaint complaint)
        {
            if (complaint.Id != 0)
            {
                complaint.Update();
            }
            else
            {
                complaint.Add();
            }
            if (Convert.ToInt32(Session["typeprofil"]) == 2)
            {
                return RedirectToAction("Add");
            }
            else
            {
                return RedirectToAction("Index");
            }
            
        }
        public PartialViewResult Detail(int id)
        {
        
            Complaint Complaint = new Complaint();
            Complaint.Affiche(id);
            Tuple<Complaint, Complaint_type, Product> tuple = new Tuple<Complaint, Complaint_type, Product>(Complaint, new Complaint_type(), new Product());
            return PartialView("_Detail", tuple);
        }
        public PartialViewResult Delete(int id)
        {
            Complaint Complaint = new Complaint();
            ViewBag.msg = Complaint.Delete(id);
            return PartialView("_List");
        }
        public JsonResult Get(int length, int start)
        {
            IList<Complaint> Complaints;
            Complaint Complaint = new Complaint();
            int sortColumn = -1;
            string searchVal = HttpContext.Request.Form["search[value]"];
            string tri = HttpContext.Request.Form["order[0][dir]"];
            string column = HttpContext.Request.Form["order[0][column]"];
            Complaints = Complaint.ListDatatable(length, start, searchVal, tri, column);
            int nbclt = Complaint.CountListDatatable(length, start, searchVal, tri, column);
            var response = new { data = Complaints, recordsFiltered = nbclt, recordsTotal = nbclt };
            return Json(response, JsonRequestBehavior.AllowGet);
        }
    }
}