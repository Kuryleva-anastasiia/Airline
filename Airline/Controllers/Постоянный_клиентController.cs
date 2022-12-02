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
    public class Постоянный_клиентController : Controller
    {
        private AirlineDatabaseEntities db = new AirlineDatabaseEntities();

        // GET: Постоянный_клиент
        public ActionResult Index()
        {
            var постоянный_клиент = db.Постоянный_клиент.Include(п => п.Пользователи);
            return View(постоянный_клиент.ToList());
        }

        // GET: Постоянный_клиент/Details/5
        public ActionResult Details(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Постоянный_клиент постоянный_клиент = db.Постоянный_клиент.Find(id);
            if (постоянный_клиент == null)
            {
                return HttpNotFound();
            }
            return View(постоянный_клиент);
        }

        // GET: Постоянный_клиент/Create
        public ActionResult Create()
        {
            ViewBag.id_клиента = new SelectList(db.Пользователи, "id", "Фамилия");
            return View();
        }

        // POST: Постоянный_клиент/Create
        // Чтобы защититься от атак чрезмерной передачи данных, включите определенные свойства, для которых следует установить привязку. Дополнительные 
        // сведения см. в статье https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create([Bind(Include = "id_клиента,Премиальные_очки,Количество_км")] Постоянный_клиент постоянный_клиент)
        {
            if (ModelState.IsValid)
            {
                db.Постоянный_клиент.Add(постоянный_клиент);
                db.SaveChanges();
                return RedirectToAction("Index");
            }

            ViewBag.id_клиента = new SelectList(db.Пользователи, "id", "Фамилия", постоянный_клиент.id_клиента);
            return View(постоянный_клиент);
        }

        // GET: Постоянный_клиент/Edit/5
        public ActionResult Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Постоянный_клиент постоянный_клиент = db.Постоянный_клиент.Find(id);
            if (постоянный_клиент == null)
            {
                return HttpNotFound();
            }
            ViewBag.id_клиента = new SelectList(db.Пользователи, "id", "Фамилия", постоянный_клиент.id_клиента);
            return View(постоянный_клиент);
        }

        // POST: Постоянный_клиент/Edit/5
        // Чтобы защититься от атак чрезмерной передачи данных, включите определенные свойства, для которых следует установить привязку. Дополнительные 
        // сведения см. в статье https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include = "id_клиента,Премиальные_очки,Количество_км")] Постоянный_клиент постоянный_клиент)
        {
            if (ModelState.IsValid)
            {
                db.Entry(постоянный_клиент).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            ViewBag.id_клиента = new SelectList(db.Пользователи, "id", "Фамилия", постоянный_клиент.id_клиента);
            return View(постоянный_клиент);
        }

        // GET: Постоянный_клиент/Delete/5
        public ActionResult Delete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Постоянный_клиент постоянный_клиент = db.Постоянный_клиент.Find(id);
            if (постоянный_клиент == null)
            {
                return HttpNotFound();
            }
            return View(постоянный_клиент);
        }

        // POST: Постоянный_клиент/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int id)
        {
            Постоянный_клиент постоянный_клиент = db.Постоянный_клиент.Find(id);
            db.Постоянный_клиент.Remove(постоянный_клиент);
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
