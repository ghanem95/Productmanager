using ProductManagement.Models;
using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace ProductManagement.Controllers
{
    public class UserController : Controller
    {
        // GET: User
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
            Tuple<User, ProductManagement.Models.Typeprofil> tuple = new Tuple<User, ProductManagement.Models.Typeprofil>(new User(), new ProductManagement.Models.Typeprofil());
            return View("Index",tuple);
        }
        public ActionResult AddUser()
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
            Tuple<User, ProductManagement.Models.Typeprofil> tuple = new Tuple<User, ProductManagement.Models.Typeprofil>(new User(), new Typeprofil());
            return View("AddUser",tuple);
        }

        public ActionResult Save([Bind(Prefix = "Item1")] User user)
        {
            if (user.Id != 0)
            {
                user.Update();
            }
            else
            {
                user.Add();
            }

            return RedirectToAction("Index");
        }
        public PartialViewResult DeleteUser(int iduser)
        {
            User user = new User();
            ViewBag.msg = user.Delete(iduser);
            return PartialView("_Listuser");
        }
        public PartialViewResult Detailuser(int id)
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
            User user = new User();
            user.Affiche(id);
            Tuple<User, ProductManagement.Models.Typeprofil> tuple = new Tuple<User, ProductManagement.Models.Typeprofil>(user, new ProductManagement.Models.Typeprofil());
            return PartialView("_Detailuser", tuple);
        }
        public JsonResult GetUsers(int length, int start)
        {
            IList<User> users;
            User user = new User();
            int sortColumn = -1;
            string searchVal = HttpContext.Request.Form["search[value]"];
            string tri = HttpContext.Request.Form["order[0][dir]"];
            string column = HttpContext.Request.Form["order[0][column]"];
            users = user.ListDatatable(length, start, searchVal, tri, column);
            int nbclt = user.CountListDatatable(length, start, searchVal, tri, column);
            var response = new { data = users, recordsFiltered = nbclt, recordsTotal = nbclt };
            return Json(response, JsonRequestBehavior.AllowGet);
        }
    }
}