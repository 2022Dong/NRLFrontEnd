using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Entity;
using System.Linq;
using System.Net;
using System.Web;
using System.Web.Mvc;
using NRLFrontEnd.Models;

namespace NRLFrontEnd.Controllers
{
    public class LaddersController : Controller
    {
        private NRLEntities db = new NRLEntities();

        // GET: Ladders
        public ActionResult Index()
        {
            var ladders = db.Ladders.Include(l => l.League).Include(l => l.Team);
            return View(ladders.ToList());
        }

        // GET: Ladders/Details/5
        public ActionResult Details(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Ladder ladder = db.Ladders.Find(id);
            if (ladder == null)
            {
                return HttpNotFound();
            }
            return View(ladder);
        }

        // GET: Ladders/Create
        public ActionResult Create()
        {
            ViewBag.LeagueName = new SelectList(db.Leagues, "LeagueName", "LeagueName");
            ViewBag.TeamName = new SelectList(db.Teams, "TeamName", "Coach");
            return View();
        }

        // POST: Ladders/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create([Bind(Include = "Position,LeagueName,TeamName,Played,Points,Wins,Lost,Byes,For,Against,Diff_")] Ladder ladder)
        {
            if (ModelState.IsValid)
            {
                db.Ladders.Add(ladder);
                db.SaveChanges();
                return RedirectToAction("Index");
            }

            ViewBag.LeagueName = new SelectList(db.Leagues, "LeagueName", "LeagueName", ladder.LeagueName);
            ViewBag.TeamName = new SelectList(db.Teams, "TeamName", "Coach", ladder.TeamName);
            return View(ladder);
        }

        // GET: Ladders/Edit/5
        public ActionResult Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Ladder ladder = db.Ladders.Find(id);
            if (ladder == null)
            {
                return HttpNotFound();
            }
            ViewBag.LeagueName = new SelectList(db.Leagues, "LeagueName", "LeagueName", ladder.LeagueName);
            ViewBag.TeamName = new SelectList(db.Teams, "TeamName", "Coach", ladder.TeamName);
            return View(ladder);
        }

        // POST: Ladders/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include = "Position,LeagueName,TeamName,Played,Points,Wins,Lost,Byes,For,Against,Diff_")] Ladder ladder)
        {
            if (ModelState.IsValid)
            {
                db.Entry(ladder).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            ViewBag.LeagueName = new SelectList(db.Leagues, "LeagueName", "LeagueName", ladder.LeagueName);
            ViewBag.TeamName = new SelectList(db.Teams, "TeamName", "Coach", ladder.TeamName);
            return View(ladder);
        }

        // GET: Ladders/Delete/5
        public ActionResult Delete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Ladder ladder = db.Ladders.Find(id);
            if (ladder == null)
            {
                return HttpNotFound();
            }
            return View(ladder);
        }

        // POST: Ladders/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int id)
        {
            Ladder ladder = db.Ladders.Find(id);
            db.Ladders.Remove(ladder);
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
