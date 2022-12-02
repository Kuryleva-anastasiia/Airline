using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Entity;
using System.Linq;
using System.Net;
using System.Web;
using System.Web.Mvc;
using Airline.Models;

namespace Airline.Controllers
{
    public class РейсController : Controller
    {
        private AirlineDatabaseEntities db = new AirlineDatabaseEntities();

        // GET: Рейс
        //Сортировка
        public ActionResult Index(string sortOrder, string searchString)
        {
            ViewBag.DepartureSortParm = String.IsNullOrEmpty(sortOrder) ? "departure_desc" : "";
            ViewBag.TimeSortParm = sortOrder == "Time" ? "Time_desc" : "Time";
            var рейсы = from s in db.Рейс
                        select s;

            if (!String.IsNullOrEmpty(searchString))
            {
                рейсы = рейсы.Where(s => s.Пункт_вылета.Contains(searchString)
                                       || s.Пункт_назначения.Contains(searchString));
            }


            switch (sortOrder)
            {
                case "departure_desc":
                    рейсы = рейсы.OrderByDescending(s => s.Пункт_вылета);
                    break;
                case "Time":
                    рейсы = рейсы.OrderBy(s => s.Время_полета);
                    break;
                case "Time_desc":
                    рейсы = рейсы.OrderByDescending(s => s.Время_полета);
                    break;
                default:
                    рейсы = рейсы.OrderBy(s => s.Пункт_вылета);
                    break;
            }
            return View(рейсы.ToList());
        }

        // GET: Рейс/Details/5
        public ActionResult Details(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Рейс рейс = db.Рейс.Find(id);
            if (рейс == null)
            {
                return HttpNotFound();
            }
            return View(рейс);
        }

        // GET: Рейс/Create
        public ActionResult Create()
        {
            return View();
        }

        // POST: Рейс/Create
        // Чтобы защититься от атак чрезмерной передачи данных, включите определенные свойства, для которых следует установить привязку. Дополнительные 
        // сведения см. в статье https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create([Bind(Include = "id,Пункт_вылета,Пункт_назначения,Время_полета,Количество_км")] Рейс рейс)
        {
            if (ModelState.IsValid)
            {
                db.Рейс.Add(рейс);
                db.SaveChanges();
                return RedirectToAction("Index");
            }

            return View(рейс);
        }

        // GET: Рейс/Edit/5
        public ActionResult Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Рейс рейс = db.Рейс.Find(id);
            if (рейс == null)
            {
                return HttpNotFound();
            }
            return View(рейс);
        }

        // POST: Рейс/Edit/5
        // Чтобы защититься от атак чрезмерной передачи данных, включите определенные свойства, для которых следует установить привязку. Дополнительные 
        // сведения см. в статье https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include = "id,Пункт_вылета,Пункт_назначения,Время_полета,Количество_км")] Рейс рейс)
        {
            if (ModelState.IsValid)
            {
                db.Entry(рейс).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            return View(рейс);
        }

        // GET: Рейс/Delete/5
        public ActionResult Delete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Рейс рейс = db.Рейс.Find(id);
            if (рейс == null)
            {
                return HttpNotFound();
            }
            return View(рейс);
        }

        // POST: Рейс/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int id)
        {
            Рейс рейс = db.Рейс.Find(id);
            db.Рейс.Remove(рейс);
            db.SaveChanges();
            return RedirectToAction("Index");
        }


        public ActionResult indexUser(string sortOrder, string searchString, int? id)
        {
            if (id != null) { 
                Рейс рейс = new Рейс();
            }

            ViewBag.DepartureSortParm = String.IsNullOrEmpty(sortOrder) ? "departure_desc" : "";
            ViewBag.TimeSortParm = sortOrder == "Time" ? "Time_desc" : "Time";
            var рейсы = from s in db.Рейс
                        select s;

            if (!String.IsNullOrEmpty(searchString))
            {
                рейсы = рейсы.Where(s => s.Пункт_вылета.Contains(searchString)
                                       || s.Пункт_назначения.Contains(searchString));
            }


            switch (sortOrder)
            {
                case "departure_desc":
                    рейсы = рейсы.OrderByDescending(s => s.Пункт_вылета);
                    break;
                case "Time":
                    рейсы = рейсы.OrderBy(s => s.Время_полета);
                    break;
                case "Time_desc":
                    рейсы = рейсы.OrderByDescending(s => s.Время_полета);
                    break;
                default:
                    рейсы = рейсы.OrderBy(s => s.Пункт_вылета);
                    break;
            }

            return View(рейсы.ToList());
        }

        public ActionResult DetailsUser(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Рейс рейс = db.Рейс.Find(id);
            if (рейс == null)
            {
                return HttpNotFound();
            }
            return View(рейс);
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
