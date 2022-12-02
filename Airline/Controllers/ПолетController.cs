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
    public class ПолетController : Controller
    {
        private AirlineDatabaseEntities db = new AirlineDatabaseEntities();

        // GET: Полет
        public ActionResult Index()
        {
            var полет = db.Полет.Include(п => п.Рейс);
            return View(полет.ToList());
        }

        public ActionResult GetItems(int id)
        {
            return View(db.Полет.Where(с => с.id == id).ToList());
        }

        // GET: Полет/Details/5
        public ActionResult Details(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Полет полет = db.Полет.Find(id);
            if (полет == null)
            {
                return HttpNotFound();
            }
            return View(полет);
        }


        // GET: Полет/Create
        public ActionResult Create()
        {
            ViewBag.id_рейса = new SelectList(db.Рейс, "id", "Пункт_вылета");
            return View();
        }

        // POST: Полет/Create
        // Чтобы защититься от атак чрезмерной передачи данных, включите определенные свойства, для которых следует установить привязку. Дополнительные 
        // сведения см. в статье https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create([Bind(Include = "id,id_рейса,Дата,Места_эконом,Стоимость_эконом,Места_vip,Стоимость_vip")] Полет полет)
        {
            if (ModelState.IsValid)
            {
                db.Полет.Add(полет);
                db.SaveChanges();
                return RedirectToAction("Index");
            }

            ViewBag.id_рейса = new SelectList(db.Рейс, "id", "Пункт_вылета", полет.id_рейса);
            return View(полет);
        }

        // GET: Полет/Edit/5
        public ActionResult Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Полет полет = db.Полет.Find(id);
            if (полет == null)
            {
                return HttpNotFound();
            }
            ViewBag.id_рейса = new SelectList(db.Рейс, "id", "Пункт_вылета", полет.id_рейса);
            return View(полет);
        }

        // POST: Полет/Edit/5
        // Чтобы защититься от атак чрезмерной передачи данных, включите определенные свойства, для которых следует установить привязку. Дополнительные 
        // сведения см. в статье https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include = "id,id_рейса,Дата,Места_эконом,Стоимость_эконом,Места_vip,Стоимость_vip")] Полет полет)
        {
            if (ModelState.IsValid)
            {
                db.Entry(полет).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            ViewBag.id_рейса = new SelectList(db.Рейс, "id", "Пункт_вылета", полет.id_рейса);
            return View(полет);
        }

        // GET: Полет/Delete/5
        public ActionResult Delete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Полет полет = db.Полет.Find(id);
            if (полет == null)
            {
                return HttpNotFound();
            }
            return View(полет);
        }

        // POST: Полет/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int id)
        {
            Полет полет = db.Полет.Find(id);
            db.Полет.Remove(полет);
            db.SaveChanges();
            return RedirectToAction("Index");
        }

        public ActionResult indexUser(Полет полет)
        {
            return View(полет);
        }

        public ActionResult DetailsUser(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Полет полет = db.Полет.Find(id);
            if (полет == null)
            {
                return HttpNotFound();
            }
            return View(полет);
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
