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
    public class ESTUDIANTESController : Controller
    {
        private Entities db = new Entities();

        // GET: ESTUDIANTES
        public ActionResult Index()
        {
            return View(db.ESTUDIANTES.ToList());
        }

        // GET: ESTUDIANTES/Details/5
        public ActionResult Details(string id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            ESTUDIANTES eSTUDIANTES = db.ESTUDIANTES.Find(id);
            if (eSTUDIANTES == null)
            {
                return HttpNotFound();
            }
            return View(eSTUDIANTES);
        }

        // GET: ESTUDIANTES/Create
        public ActionResult Create()
        {
            return View();
        }

        // POST: ESTUDIANTES/Create
        // Para protegerse de ataques de publicación excesiva, habilite las propiedades específicas a las que quiere enlazarse. Para obtener 
        // más detalles, vea https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create([Bind(Include = "CARNET,NOMBRE,FECHA_NACIMIENTO,FECHA_REGISTRO")] ESTUDIANTES eSTUDIANTES)
        {
            if (ModelState.IsValid)
            {
                db.ESTUDIANTES.Add(eSTUDIANTES);
                db.SaveChanges();
                return RedirectToAction("Index");
            }

            return View(eSTUDIANTES);
        }

        // GET: ESTUDIANTES/Edit/5
        public ActionResult Edit(string id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            ESTUDIANTES eSTUDIANTES = db.ESTUDIANTES.Find(id);
            if (eSTUDIANTES == null)
            {
                return HttpNotFound();
            }
            return View(eSTUDIANTES);
        }

        // POST: ESTUDIANTES/Edit/5
        // Para protegerse de ataques de publicación excesiva, habilite las propiedades específicas a las que quiere enlazarse. Para obtener 
        // más detalles, vea https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include = "CARNET,NOMBRE,FECHA_NACIMIENTO,FECHA_REGISTRO")] ESTUDIANTES eSTUDIANTES)
        {
            if (ModelState.IsValid)
            {
                db.Entry(eSTUDIANTES).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            return View(eSTUDIANTES);
        }

        // GET: ESTUDIANTES/Delete/5
        public ActionResult Delete(string id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            ESTUDIANTES eSTUDIANTES = db.ESTUDIANTES.Find(id);
            if (eSTUDIANTES == null)
            {
                return HttpNotFound();
            }
            return View(eSTUDIANTES);
        }

        // POST: ESTUDIANTES/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(string id)
        {
            ESTUDIANTES eSTUDIANTES = db.ESTUDIANTES.Find(id);
            db.ESTUDIANTES.Remove(eSTUDIANTES);
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
