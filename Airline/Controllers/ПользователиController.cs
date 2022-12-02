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
    public class ПользователиController : Controller
    {
        private AirlineDatabaseEntities db = new AirlineDatabaseEntities();

        // GET: Пользователи
        public ActionResult Index()
        {
            var пользователи = db.Пользователи.Include(п => п.Постоянный_клиент).Include(п => п.Вход);
            return View(пользователи.ToList());
        }

        // GET: Пользователи/Details/5
        public ActionResult Details(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Пользователи пользователи = db.Пользователи.Find(id);
            if (пользователи == null)
            {
                return HttpNotFound();
            }
            return View(пользователи);
        }

        // GET: Пользователи/Create
        public ActionResult Create()
        {
            ViewBag.id = new SelectList(db.Постоянный_клиент, "id_клиента", "id_клиента");
            ViewBag.id = new SelectList(db.Вход, "id_клиента", "Логин");
            return View();
        }

        // POST: Пользователи/Create
        // Чтобы защититься от атак чрезмерной передачи данных, включите определенные свойства, для которых следует установить привязку. Дополнительные 
        // сведения см. в статье https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create([Bind(Include = "id,Фамилия,Имя,Отчество,image")] Пользователи пользователи, HttpPostedFileBase upload)
        {
            if (ModelState.IsValid)
            {
                if (upload != null && upload.ContentLength > 0)
                {
                    using (var reader = new System.IO.BinaryReader(upload.InputStream))
                    {
                        пользователи.image = reader.ReadBytes(upload.ContentLength);
                    }
                }

                db.Пользователи.Add(пользователи);
                db.SaveChanges();
                return RedirectToAction("Index");
            }

            ViewBag.id = new SelectList(db.Постоянный_клиент, "id_клиента", "id_клиента", пользователи.id);
            ViewBag.id = new SelectList(db.Вход, "id_клиента", "Логин", пользователи.id);
            return View(пользователи);
        }

        // GET: Пользователи/Edit/5
        public ActionResult Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Пользователи пользователи = db.Пользователи.Find(id);
            if (пользователи == null)
            {
                return HttpNotFound();
            }
            ViewBag.id = new SelectList(db.Постоянный_клиент, "id_клиента", "id_клиента", пользователи.id);
            ViewBag.id = new SelectList(db.Вход, "id_клиента", "Логин", пользователи.id);
            return View(пользователи);
        }

        // POST: Пользователи/Edit/5
        // Чтобы защититься от атак чрезмерной передачи данных, включите определенные свойства, для которых следует установить привязку. Дополнительные 
        // сведения см. в статье https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include = "id,Фамилия,Имя,Отчество,image")] Пользователи пользователи, HttpPostedFileBase upload)
        {
            if (ModelState.IsValid)
            {
                db.Entry(пользователи).State = EntityState.Modified;

                if (upload != null && upload.ContentLength > 0)
                {
                    using (var reader = new System.IO.BinaryReader(upload.InputStream))
                    {
                        пользователи.image = reader.ReadBytes(upload.ContentLength);
                        db.SaveChanges();
                    }
                }
                else
                {

                    db.Entry(пользователи).Property(m => m.image).IsModified = false;
                    db.SaveChanges();
                }

                return RedirectToAction("Index");
            }
            ViewBag.id = new SelectList(db.Постоянный_клиент, "id_клиента", "id_клиента", пользователи.id);
            ViewBag.id = new SelectList(db.Вход, "id_клиента", "Логин", пользователи.id);
            return View(пользователи);
        }

        //// GET: Пользователи/Delete/5
        //public ActionResult Delete(int? id)
        //{
        //    if (id == null)
        //    {
        //        return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
        //    }
        //    Пользователи пользователи = db.Пользователи.Find(id);
        //    if (пользователи == null)
        //    {
        //        return HttpNotFound();
        //    }
        //    return View(пользователи);
        //}

        //// POST: Пользователи/Delete/5
        //[HttpPost, ActionName("Delete")]
        //[ValidateAntiForgeryToken]
        //public ActionResult DeleteConfirmed(int id)
        //{
        //    Пользователи пользователи = db.Пользователи.Find(id);
        //    db.Пользователи.Remove(пользователи);
        //    db.SaveChanges();
        //    return RedirectToAction("Index");
        //}

        public ActionResult DetailsUser(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Пользователи пользователи = db.Пользователи.Find(id);
            ViewBag.id = id;
            if (пользователи == null)
            {
                return HttpNotFound();
            }
            return View(пользователи);
        }
        public ActionResult EditUser(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Пользователи пользователи = db.Пользователи.Find(id);
            if (пользователи == null)
            {
                return HttpNotFound();
            }
            return View(пользователи);
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
