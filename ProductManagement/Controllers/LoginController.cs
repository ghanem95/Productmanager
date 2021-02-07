using ProductManagement.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace ProductManagement.Controllers
{
    public class LoginController : Controller
    {
        // GET: Login
        public ActionResult Index()
        {
            return View("Login");
        }
        public ActionResult Login(User user)
        {
            user.Authentication();
            if(user.Id != 0)
            {
                Session["name"] = user.Firstname;
                Session["typeprofil"] = user.Typeprofil;
              return RedirectToAction("Index","Home");
            }
            else
            {
                return RedirectToAction("Index");
            }
             

        }
        public ActionResult Logout()
        {
            Session.Clear();
            return View("Login");

        }
    }
}