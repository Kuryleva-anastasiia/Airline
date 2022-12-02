using System;
using System.Data.Entity;
using System.Linq;
using System.Net;
using System.Web;
using System.Web.Mvc;
using Airline.Models;
namespace Airline.Controllers
{
    public class ЗаявкаController : Controller
    {
        private AirlineDatabaseEntities db = new AirlineDatabaseEntities();

        // GET: Заявка
        public ActionResult Index()
        {
            var заявка = db.Заявка.Include(з => з.Полет).Include(з => з.Пользователи);
            return View(заявка.ToList());
        }

        // GET: Заявка/Details/5
        public ActionResult Details(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Заявка заявка = db.Заявка.Find(id);
            if (заявка == null)
            {
                return HttpNotFound();
            }
            return View(заявка);
        }

        // GET: Заявка/Create
        public ActionResult Create()
        {
            int selectedIndex = 1;
            //int selectedIndex2 = 1;
            SelectList surname = new SelectList(db.Пользователи, "id", "Фамилия");
            ViewBag.surname = surname;

			SelectList flightIds = new SelectList(db.Полет, "id", "id", selectedIndex);
			ViewBag.flightIds = flightIds;

			//SelectList depatures = new SelectList(db.Рейс, "id", "Пункт_назначения", selectedIndex2);
			//ViewBag.Depatures = depatures;

			//ViewBag.id = id;

			//SelectList dates = new SelectList(db.Полет.Where(c => c.id == selectedIndex && c.id == selectedIndex2), "id", "Дата");
   //         ViewBag.Dates = dates;

            ViewBag.sum = "0";
            ViewBag.points = "0";

            return View();
        }

        

        // POST: Заявка/Create
        // Чтобы защититься от атак чрезмерной передачи данных, включите определенные свойства, для которых следует установить привязку. Дополнительные 
        // сведения см. в статье https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create([Bind(Include = "id,id_клиента,id_полета,Класс,Количество_мест,Сумма,Премиальные_очки")] Заявка заявка)
        {
            if (ModelState.IsValid)
            {
                var flight = db.Полет.Find(заявка.id_полета);
                var user = db.Постоянный_клиент.Find(заявка.id_клиента);

                if (заявка.Класс == "эконом")
                {
                    double sum = Convert.ToDouble(flight.Стоимость_эконом) * заявка.Количество_мест;
                    заявка.Сумма = (decimal)sum;
                    заявка.Премиальные_очки = (int)sum / 100 * 5;
                    flight.Места_эконом -= заявка.Количество_мест;
                    
                }
                else {
                    double sum = Convert.ToDouble(flight.Стоимость_vip) * заявка.Количество_мест;
                    заявка.Сумма = (decimal)sum;
                    заявка.Премиальные_очки = (int)sum / 100 * 5;
                    flight.Места_vip -= заявка.Количество_мест;
                }
                if (user != null)
                {
                    user.Премиальные_очки += (int)заявка.Премиальные_очки;
                    user.Количество_км += flight.Рейс.Количество_км;
                }

                db.Заявка.Add(заявка);
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            
            ViewBag.id_полета = new SelectList(db.Полет, "id", "id", заявка.id_полета);
            ViewBag.id_клиента = new SelectList(db.Пользователи, "id", "Фамилия", заявка.id_клиента);
            return View(заявка);
        }

        //[HttpPost]
        //[ValidateAntiForgeryToken]
        //public ActionResult Create(int seats, Заявка заявка)
        //{
        //    if (ModelState.IsValid)
        //    {
        //        db.Заявка.Add(заявка);
        //        db.SaveChanges();
        //        //тут процедура которой передаётся id полета, она ищет совпадающую последнюю запись в заявках, считает сумму и количество премиальных очков, удаляет эту запись и выводит сумму и очки
        //        int id = ViewBag.id;
        //        ViewBag.items = db.Заявка.Last(c => c.id_полета == id);
        //        foreach (string item in ViewBag.items) {
        //            var items = item.ToList();
        //            ViewBag.seats = item[4];
        //            ViewBag.clas = item[3];
        //            ViewBag.FlightID = item[2];
        //            //вот и параметры ддля процедуры
        //        }
        //        ViewBag.sum = "1000";
        //        ViewBag.points = "10";

        //        return View(заявка);
        //    }

        //    ViewBag.id_полета = new SelectList(db.Полет, "id", "id", заявка.id_полета);
        //    ViewBag.id_клиента = new SelectList(db.Пользователи, "id", "Фамилия", заявка.id_клиента);
        //    return View(заявка);
        //}

        // GET: Заявка/Edit/5
        public ActionResult Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Заявка заявка = db.Заявка.Find(id);
            if (заявка == null)
            {
                return HttpNotFound();
            }
            ViewBag.id_полета = new SelectList(db.Полет, "id", "id", заявка.id_полета);
            ViewBag.id_клиента = new SelectList(db.Пользователи, "id", "Фамилия", заявка.id_клиента);
            return View(заявка);
        }

        // POST: Заявка/Edit/5
        // Чтобы защититься от атак чрезмерной передачи данных, включите определенные свойства, для которых следует установить привязку. Дополнительные 
        // сведения см. в статье https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include = "id,id_клиента,id_полета,Класс,Количество_мест,Сумма,Премиальные_очки")] Заявка заявка)
        {
            if (ModelState.IsValid)
            {
                db.Entry(заявка).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            ViewBag.id_полета = new SelectList(db.Полет, "id", "id", заявка.id_полета);
            ViewBag.id_клиента = new SelectList(db.Пользователи, "id", "Фамилия", заявка.id_клиента);
            return View(заявка);
        }

        // GET: Заявка/Delete/5
        public ActionResult Delete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Заявка заявка = db.Заявка.Find(id);
            if (заявка == null)
            {
                return HttpNotFound();
            }
            return View(заявка);
        }

        // POST: Заявка/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int id)
        {
            Заявка заявка = db.Заявка.Find(id);
            db.Заявка.Remove(заявка);
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
