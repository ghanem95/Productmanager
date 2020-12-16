using ProductManagement.Models;
using System;
using System.Collections.Generic;
using System.Configuration;
using System.Data;
using System.Data.SqlClient;
using System.Globalization;
using System.Linq;
using System.Text;
using System.Web;
using System.Web.Mvc;

namespace ProductManagement.Controllers
{
    public class ClientController : Controller
    {
        // GET: Client
        public ActionResult Index()
        {
            List<string> CountryList = new List<string>();
            CultureInfo[] CInfoList = CultureInfo.GetCultures(CultureTypes.SpecificCultures);
            foreach (CultureInfo CInfo in CInfoList)
            {
                RegionInfo R = new RegionInfo(CInfo.LCID);
                if (!(CountryList.Contains(R.EnglishName)))
                {
                    CountryList.Add(R.EnglishName);
                }
            }

            CountryList.Sort();
            ViewBag.CountryList = CountryList;
            Client clt = new Client();
            ViewBag.ListClient = clt.List();
            return View();
        }
        public ActionResult AddClt()
        {
            List<string> CountryList = new List<string>();
            CultureInfo[] CInfoList = CultureInfo.GetCultures(CultureTypes.SpecificCultures);
            foreach (CultureInfo CInfo in CInfoList)
            {
                RegionInfo R = new RegionInfo(CInfo.LCID);
                if (!(CountryList.Contains(R.EnglishName)))
                {
                    CountryList.Add(R.EnglishName);
                }
            }

            CountryList.Sort();
            ViewBag.CountryList = CountryList;
            return View();
        }
        public PartialViewResult DeleteClt(int idclt)
        {
            Client client = new Client();
            ViewBag.msg = client.Delete(idclt);
            ViewBag.ListClient = client.List();
            return PartialView("_Listclt");
        }
        public PartialViewResult DetailClt(int idclt)
        {
            List<string> CountryList = new List<string>();
            CultureInfo[] CInfoList = CultureInfo.GetCultures(CultureTypes.SpecificCultures);
            foreach (CultureInfo CInfo in CInfoList)
            {
                RegionInfo R = new RegionInfo(CInfo.LCID);
                if (!(CountryList.Contains(R.EnglishName)))
                {
                    CountryList.Add(R.EnglishName);
                }
            }

            CountryList.Sort();
            ViewBag.CountryList = CountryList;
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

        public JsonResult GetClients(int length, int start)
        {
            IList<Client> clients;
            Client client = new Client();
            int sortColumn = -1;
            string searchVal = HttpContext.Request.Form["search[value]"];
            string tri = HttpContext.Request.Form["order[0][dir]"];
            string column = HttpContext.Request.Form["order[0][column]"];
            clients = client.ListDatatable(length, start, searchVal,tri,column);
            int nbclt = client.CountListDatatable(length, start, searchVal, tri, column);
            var response = new { data = clients, recordsFiltered = nbclt, recordsTotal = nbclt };
            return Json(response, JsonRequestBehavior.AllowGet);
        }
    }
}