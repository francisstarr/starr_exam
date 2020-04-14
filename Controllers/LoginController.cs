using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using starrexam.Models;
namespace starrexam.Controllers
{
    public class LoginController : Controller
    {
        // GET: Logi
        public ActionResult Index()
        {
            return View();
        }
        [HttpPost]
        public ActionResult Authorize(user x) {
            starrexamEntities dbas = new starrexamEntities();
            var checkRowCount = from u in dbas.users 
                                where u.userName.Equals(x.userName.Trim()) && u.password.Equals(x.password.Trim()) select u;
            int rowCount = checkRowCount.ToList().Count();
            if (rowCount == 0)
            {
                x.errorMessage = "Access denied. User name and password don't match";
                return View("Index", x);
            }
            else if (rowCount == 1)
            {
                Session["userName"] = x.userName.Trim();
                Session["userType"] = checkRowCount.ToList().First().userType;
                return RedirectToAction("Index", "Home");
            }
            else {
                x.errorMessage = "Programming error: there is more than record in the database with this user name and password.";
                return View("Index",x);
            }
        }
    }
}