using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Entity;
using System.Linq;
using System.Net;
using System.Web;
using System.Web.Mvc;
using Airline.Models;
using System.Web.Helpers;


namespace Airline.Controllers
{
    public class ВходController : Controller
    {
        private AirlineDatabaseEntities db = new AirlineDatabaseEntities();

        // GET: Вход
        public ActionResult Index()
        {
            var вход = db.Вход.Include(в => в.Пользователи);
            return View(вход.ToList());
        }

        // GET: Вход/Details/5
        public ActionResult Details(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Вход вход = db.Вход.Find(id);
            if (вход == null)
            {
                return HttpNotFound();
            }
            return View(вход);
        }

        // GET: Вход/Register
        public ActionResult Register(Вход вход, Пользователи пользователи)
        {
            if (вход.login != null && вход.password != null && пользователи.Имя != null && пользователи.Фамилия != null && пользователи.Отчество != null)
            {
                // Verification.    
                if (ModelState.IsValid)
                {
                    // Initialization.    
                    var regInfo = db.Insert_User(вход.login, вход.password, пользователи.Имя, пользователи.Фамилия, пользователи.Отчество).ToList();
                    // Verification.    
                    if (regInfo != null && Convert.ToInt32(regInfo[0]) != -1)
                    {
                        
                        //SAVING CHANGES TO DATABASE
                        db.SaveChanges();
                        return RedirectToAction("Login", "Вход");

                    }
                    else
                    {
                        ModelState.AddModelError(string.Empty, "Пользователь с таким логином уже существует!");
                    }
                }
            }

            return View(вход);

        }

       

        // POST: Вход/Login
        // Чтобы защититься от атак чрезмерной передачи данных, включите определенные свойства, для которых следует установить привязку. Дополнительные 
        // сведения см. в статье https://go.microsoft.com/fwlink/?LinkId=317598.
        
        public ActionResult Login([Bind(Include = "login,password")] Вход model)
        {
            if (ModelState.IsValid)
            {
                if (model.login != null && model.password != null)
                { 
                    // Initialization.    
                    var loginInfo = this.db.LoginByPassword2(model.login, model.password).ToList();
                    // Verification.    
                    if (loginInfo != null && loginInfo.Count() > 0)
                    {
                        // Initialization.    
                        int id = Convert.ToInt32(loginInfo.First());

                        if (id == 1)
                        {
                            model.Id = id;
                            return this.RedirectToAction("index", "Пользователи");
                        }
                        else {
							model.Id = id;
                            return this.RedirectToAction("DetailsUser", "Пользователи", new { id }); }
					}
					else
                    {
                        ModelState.AddModelError(string.Empty, "Неверный логин или пароль");
                        return View(model);
                    }
                }
            }
            ViewBag.Id = new SelectList(db.Пользователи, "id", "Фамилия", model.Id);
            return View(model);

        }

        // GET: Вход/Edit/5
        public ActionResult Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Вход вход = db.Вход.Find(id);
            if (вход == null)
            {
                return HttpNotFound();
            }
            ViewBag.Id = new SelectList(db.Пользователи, "id", "Фамилия", вход.Id);
            return View(вход);
        }

        // POST: Вход/Edit/5
        // Чтобы защититься от атак чрезмерной передачи данных, включите определенные свойства, для которых следует установить привязку. Дополнительные 
        // сведения см. в статье https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include = "login,password")] Вход вход)
        {
            if (ModelState.IsValid)
            {
                db.Entry(вход).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            ViewBag.Id = new SelectList(db.Пользователи, "id", "Фамилия", вход.Id);
            return View(вход);
        }

        // GET: Вход/Delete/5
        public ActionResult Delete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Вход вход = db.Вход.Find(id);
            if (вход == null)
            {
                return HttpNotFound();
            }
            return View(вход);
        }

        // POST: Вход/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int id)
        {
            Вход вход = db.Вход.Find(id);
            Пользователи users = db.Пользователи.Find(id);
            db.Вход.Remove(вход);
            db.Пользователи.Remove(users);
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
