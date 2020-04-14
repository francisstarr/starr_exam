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
    public class BookingsController : Controller
    {
        private starrexamEntities db = new starrexamEntities();

        // GET: Bookings
        public ActionResult Index()
        {
            //var bookings = db.bookings.Include(b => b.room).Include(b => b.user);
            string userName = (string)Session["userName"];
            var bookings = from b in db.bookings 
                           where b.userName.Equals(userName) 
                           select b;
           
            return View(bookings.ToList());
        }

        // GET: Bookings/Details/5
        public ActionResult Details(string id)
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
            return View(booking);
        }

        // GET: Bookings/Create
        public ActionResult Create()
        {
            ViewBag.roomNumber = new SelectList(db.rooms, "roomNumber", "roomNumber");
            ViewBag.userName = new SelectList(db.users, "userName", "userName");
            return View();
        }

        // POST: Bookings/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create([Bind(Include = "bookingId,roomNumber,userName,starting,ending")] booking booking)
        {
            if (booking.starting < DateTime.Now || booking.ending < DateTime.Now) {
                booking.errorMessage = "One or more of your dates is in the past. You must change that if you want to continue";
            }
            else if (booking.starting>booking.ending) {
                booking.errorMessage = "Your starting date cannot occur after your ending date.";
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

        // GET: Bookings/Edit/5
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
            ViewBag.userName = new SelectList(db.users, "userName", "password", booking.userName);
            return View(booking);
        }

        // POST: Bookings/Edit/5
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

        // GET: Bookings/Delete/5
        public ActionResult Delete(string id)
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
            return View(booking);
        }

        // POST: Bookings/Delete/5
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
