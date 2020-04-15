using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Entity;
using System.Linq;
using System.Net;
using System.Web;
using System.Web.Mvc;
using starrexam.Models;

namespace starrexam.Controllers
{
    public class UsersController : Controller
    {
        private starrexamEntities db = new starrexamEntities();

        // GET: Users
      
        public ActionResult Index()
        {
            /////////////
            if (Session["userType"]==null || !Session["userType"].Equals("admin")) {
                return RedirectToAction("Index", "Home");
            }
             //////////
            return View(db.users.ToList());
        }

        // GET: Users/Details/5
        
        public ActionResult Details(string id)
        {
            /////////////
            if (Session["userType"] == null || !Session["userType"].Equals("admin"))
            {
                return RedirectToAction("Index", "Home");
            }
            //////////
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            user user = db.users.Find(id);
            if (user == null)
            {
                return HttpNotFound();
            }
            return View(user);
        }

        // GET: Users/Create
        public ActionResult Create()
        {
            /////////////
            if (Session["userType"] == null || !Session["userType"].Equals("admin"))
            {
                return RedirectToAction("Index", "Home");
            }
            //////////
            return View();
        }

        // POST: Users/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create([Bind(Include = "userName,password,userType")] user user)
        {
            /////////////
            if (Session["userType"] == null || !Session["userType"].Equals("admin"))
            {
                return RedirectToAction("Index", "Home");
            }
            //////////
            if (ModelState.IsValid)
            {
                db.users.Add(user);
                db.SaveChanges();
                return RedirectToAction("Index");
            }

            return View(user);
        }

        // GET: Users/Edit/5
        public ActionResult Edit(string id)
        {
            /////////////
            if (Session["userType"] == null || !Session["userType"].Equals("admin"))
            {
                return RedirectToAction("Index", "Home");
            }
            //////////
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            user user = db.users.Find(id);
            if (user == null)
            {
                return HttpNotFound();
            }
            return View(user);
        }

        // POST: Users/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include = "userName,password,userType")] user user)
        {
            /////////////
            if (Session["userType"] == null || !Session["userType"].Equals("admin"))
            {
                return RedirectToAction("Index", "Home");
            }
            //////////
            if (ModelState.IsValid)
            {
                db.Entry(user).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            return View(user);
        }

        // GET: Users/Delete/5
        public ActionResult Delete(string id)
        {
            /////////////
            if (Session["userType"] == null || !Session["userType"].Equals("admin"))
            {
                return RedirectToAction("Index", "Home");
            }
            //////////
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            user user = db.users.Find(id);
            if (user == null)
            {
                return HttpNotFound();
            }
            else if (user.userName.Equals(Session["userName"]))
            {
                user.errorMessage = "Users are not permitted to delete themselves from the application. Therefore the delete button will not work in this instance.";
            }
            return View(user);
        }

        // POST: Users/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(string id)
        {
            /////////////
            if (Session["userType"] == null || !Session["userType"].Equals("admin"))
            {
                return RedirectToAction("Index", "Home");
            }
            //////////
            user user = db.users.Find(id);
            if (user.userName.Equals(Session["userName"]))
            {
                return Delete(id);
            }
            db.users.Remove(user);
            db.SaveChanges();
            return RedirectToAction("Index");
        }

        protected override void Dispose(bool disposing)
        {

            if (disposing)
            {
                db.Dispose();
            }
            base.Dispose(disposing);
        }
    }
}
