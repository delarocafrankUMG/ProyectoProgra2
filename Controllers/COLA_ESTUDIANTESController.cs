using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Entity;
using System.Linq;
using System.Net;
using System.Web;
using System.Web.Mvc;
using ProyectoProgra2.Models;

namespace ProyectoProgra2.Controllers
{
    public class COLA_ESTUDIANTESController : Controller
    {
        private Entities db = new Entities();

        // GET: COLA_ESTUDIANTES
        public ActionResult Index()
        {
            var cOLA_ESTUDIANTES = db.COLA_ESTUDIANTES.Include(c => c.ESTUDIANTES);
            return View(cOLA_ESTUDIANTES.ToList());
        }

        // GET: COLA_ESTUDIANTES/Details/5
        public ActionResult Details(string id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            COLA_ESTUDIANTES cOLA_ESTUDIANTES = db.COLA_ESTUDIANTES.Find(id);
            if (cOLA_ESTUDIANTES == null)
            {
                return HttpNotFound();
            }
            return View(cOLA_ESTUDIANTES);
        }

        // GET: COLA_ESTUDIANTES/Create
        public ActionResult Create()
        {
            ViewBag.CARNET = new SelectList(db.ESTUDIANTES, "CARNET", "NOMBRE");
            return View();
        }

        // POST: COLA_ESTUDIANTES/Create
        // Para protegerse de ataques de publicación excesiva, habilite las propiedades específicas a las que quiere enlazarse. Para obtener 
        // más detalles, vea https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create([Bind(Include = "CARNET,FECHA_ADICION,PROCESADO")] COLA_ESTUDIANTES cOLA_ESTUDIANTES)
        {
            if (ModelState.IsValid)
            {
                db.COLA_ESTUDIANTES.Add(cOLA_ESTUDIANTES);
                db.SaveChanges();
                return RedirectToAction("Index");
            }

            ViewBag.CARNET = new SelectList(db.ESTUDIANTES, "CARNET", "NOMBRE", cOLA_ESTUDIANTES.CARNET);
            return View(cOLA_ESTUDIANTES);
        }

        // GET: COLA_ESTUDIANTES/Edit/5
        public ActionResult Edit(string id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            COLA_ESTUDIANTES cOLA_ESTUDIANTES = db.COLA_ESTUDIANTES.Find(id);
            if (cOLA_ESTUDIANTES == null)
            {
                return HttpNotFound();
            }
            ViewBag.CARNET = new SelectList(db.ESTUDIANTES, "CARNET", "NOMBRE", cOLA_ESTUDIANTES.CARNET);
            return View(cOLA_ESTUDIANTES);
        }

        // POST: COLA_ESTUDIANTES/Edit/5
        // Para protegerse de ataques de publicación excesiva, habilite las propiedades específicas a las que quiere enlazarse. Para obtener 
        // más detalles, vea https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include = "CARNET,FECHA_ADICION,PROCESADO")] COLA_ESTUDIANTES cOLA_ESTUDIANTES)
        {
            if (ModelState.IsValid)
            {
                db.Entry(cOLA_ESTUDIANTES).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            ViewBag.CARNET = new SelectList(db.ESTUDIANTES, "CARNET", "NOMBRE", cOLA_ESTUDIANTES.CARNET);
            return View(cOLA_ESTUDIANTES);
        }

        // GET: COLA_ESTUDIANTES/Delete/5
        public ActionResult Delete(string id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            COLA_ESTUDIANTES cOLA_ESTUDIANTES = db.COLA_ESTUDIANTES.Find(id);
            if (cOLA_ESTUDIANTES == null)
            {
                return HttpNotFound();
            }
            return View(cOLA_ESTUDIANTES);
        }

        // POST: COLA_ESTUDIANTES/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(string id)
        {
            COLA_ESTUDIANTES cOLA_ESTUDIANTES = db.COLA_ESTUDIANTES.Find(id);
            db.COLA_ESTUDIANTES.Remove(cOLA_ESTUDIANTES);
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
