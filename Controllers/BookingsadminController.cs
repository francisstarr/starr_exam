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
    public class BookingsadminController : Controller
    {
        private starrexamEntities db = new starrexamEntities();

        // GET: Bookingsadmin
        public ActionResult Index()
        {
            /////////////
            if (Session["userType"] == null || !Session["userType"].Equals("admin"))
            {
                return RedirectToAction("Index", "Home");
            }
            //////////
            var bookings = db.bookings.Include(b => b.room).Include(b => b.user);
            return View(bookings.ToList());
        }

        // GET: Bookingsadmin/Details/5
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
            booking booking = db.bookings.Find(id);
            if (booking == null)
            {
                return HttpNotFound();
            }
            return View(booking);
        }

        // GET: Bookingsadmin/Create
        public ActionResult Create()
        {
            /////////////
            if (Session["userType"] == null || !Session["userType"].Equals("admin"))
            {
                return RedirectToAction("Index", "Home");
            }
            //////////
            ViewBag.roomNumber = new SelectList(db.rooms, "roomNumber", "roomNumber");
            ViewBag.userName = new SelectList(db.users, "userName", "userName");
            return View();
        }

        // POST: Bookingsadmin/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create([Bind(Include = "bookingId,roomNumber,userName,starting,ending")] booking booking)
        {
            /////////////
            if (Session["userType"] == null || !Session["userType"].Equals("admin"))
            {
                return RedirectToAction("Index", "Home");
            }
            //////////
            var checkRowCount = from b in db.bookings
                                where (b.roomNumber == booking.roomNumber) &&
       ((booking.ending >= b.starting && booking.ending <= b.ending) || (booking.starting >= b.starting && booking.starting <= b.ending))
                                select b;
            int rowCount = checkRowCount.ToList().Count();
            if (rowCount > 0)
            {
                booking.errorMessage = "Sorry but the dates you chose overlap with an existing of this room and we don't don double bookings. Please change the dates if you really want this room";
            }
            else if (booking.starting < DateTime.Today || booking.ending < DateTime.Today)
            {
                booking.errorMessage = "One or more of your dates is in the past. You must change that if you want to continue";
            }
            else if (booking.starting >= booking.ending)
            {
                booking.errorMessage = "Your ending date must be greater than your start date.";
            }
            else if (ModelState.IsValid)
            {
                booking.bookingId = booking.randomString();
                db.bookings.Add(booking);
                db.SaveChanges();
                return RedirectToAction("Index");
            }

            ViewBag.roomNumber = new SelectList(db.rooms, "roomNumber", "roomNumber", booking.roomNumber);
            ViewBag.userName = new SelectList(db.users, "userName", "userName", booking.userName);
            return View(booking);
        }

        // GET: Bookingsadmin/Edit/5
        public ActionResult Edit(string id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            booking booking = db.bookings.Find(id);
            if (booking == null)
            {
                return HttpNotFound();
            }
            ViewBag.roomNumber = new SelectList(db.rooms, "roomNumber", "roomNumber", booking.roomNumber);
            ViewBag.userName = new SelectList(db.users, "userName", "userName", booking.userName);
            return View(booking);
        }

        // POST: Bookingsadmin/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include = "bookingId,roomNumber,userName,starting,ending")] booking booking)
        {
            if (ModelState.IsValid)
            {
                db.Entry(booking).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            ViewBag.roomNumber = new SelectList(db.rooms, "roomNumber", "roomNumber", booking.roomNumber);
            ViewBag.userName = new SelectList(db.users, "userName", "password", booking.userName);
            return View(booking);
        }

        // GET: Bookingsadmin/Delete/5
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
            booking booking = db.bookings.Find(id);
            if (booking == null)
            {
                return HttpNotFound();
            }
            return View(booking);
        }

        // POST: Bookingsadmin/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(string id)
        {
            booking booking = db.bookings.Find(id);
            db.bookings.Remove(booking);
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
