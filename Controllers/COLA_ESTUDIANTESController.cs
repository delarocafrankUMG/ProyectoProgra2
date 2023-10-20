using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Entity;
using System.Globalization;
using System.Linq;
using System.Net;
using System.Runtime.InteropServices;
using System.Web;
using System.Web.Mvc;
using System.Web.UI;
using ProyectoProgra2.Models;

namespace ProyectoProgra2.Controllers
{
    public class COLA_ESTUDIANTESController : Controller
    {
        private Entities db = new Entities();

        // GET: COLA_ESTUDIANTES
        [Authorize][OutputCache(NoStore = true, Location = OutputCacheLocation.None)]
        public ActionResult Index()
        {
            var cOLA_ESTUDIANTES = db.COLA_ESTUDIANTES.Include(c => c.ESTUDIANTES);
            return View(cOLA_ESTUDIANTES.ToList());
        }

        // GET: COLA_ESTUDIANTES/Details/5
        [Authorize][OutputCache(NoStore = true, Location = OutputCacheLocation.None)]
        public ActionResult Details(string CARNET, DateTime FECHA_ADICION)
        {
            if (CARNET == null || FECHA_ADICION == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            COLA_ESTUDIANTES cOLA_ESTUDIANTES = db.COLA_ESTUDIANTES.Find(CARNET, FECHA_ADICION);
            if (cOLA_ESTUDIANTES == null)
            {
                return HttpNotFound();
            }
            return View(cOLA_ESTUDIANTES);
        }

        // GET: COLA_ESTUDIANTES/Create
        [Authorize][OutputCache(NoStore = true, Location = OutputCacheLocation.None)]
        public ActionResult Create()
        {
            ViewBag.CARNET = new SelectList(db.ESTUDIANTES, "CARNET", "NOMBRE");
            return View();
        }

        // POST: COLA_ESTUDIANTES/Create
        // Para protegerse de ataques de publicación excesiva, habilite las propiedades específicas a las que quiere enlazarse. Para obtener 
        // más detalles, vea https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [Authorize][OutputCache(NoStore = true, Location = OutputCacheLocation.None)]
        [ValidateAntiForgeryToken]
        public ActionResult Create([Bind(Include = "CARNET,FECHA_ADICION,PROCESADO")] COLA_ESTUDIANTES cOLA_ESTUDIANTES)
        {
            if (ModelState.IsValid)
            {
                // Verificar si ya existe un registro con el mismo carnet y estado de "procesado" en 0
                bool existeRegistro = db.COLA_ESTUDIANTES.Any(x => x.CARNET == cOLA_ESTUDIANTES.CARNET && x.PROCESADO == false);

                if (existeRegistro)
                {
                    ModelState.AddModelError("CARNET", "Ya existe un registro con el mismo carnet y estado de 'procesado' en Falso.");
                    ViewBag.CARNET = new SelectList(db.ESTUDIANTES, "CARNET", "NOMBRE", cOLA_ESTUDIANTES.CARNET);
                    return View(cOLA_ESTUDIANTES);
                }

                // Si no existe un registro, procede con la inserción
                db.COLA_ESTUDIANTES.Add(cOLA_ESTUDIANTES);
                db.SaveChanges();
                return RedirectToAction("Index");
            }

            ViewBag.CARNET = new SelectList(db.ESTUDIANTES, "CARNET", "NOMBRE", cOLA_ESTUDIANTES.CARNET);
            return View(cOLA_ESTUDIANTES);
        }


        // GET: COLA_ESTUDIANTES/Edit/5
        [Authorize][OutputCache(NoStore = true, Location = OutputCacheLocation.None)]
        public ActionResult Edit(string CARNET, DateTime FECHA_ADICION)
        {
            if (CARNET == null || FECHA_ADICION == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }

            // Cargar COLA_ESTUDIANTES y sus datos relacionados (ESTUDIANTES)
            COLA_ESTUDIANTES cOLA_ESTUDIANTES = db.COLA_ESTUDIANTES
                .Include("ESTUDIANTES") // Asegúrate de que "ESTUDIANTES" sea el nombre de la propiedad de navegación en tu modelo
                .Where(x => x.CARNET == CARNET && x.FECHA_ADICION == FECHA_ADICION)
                .FirstOrDefault();

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
        [Authorize][OutputCache(NoStore = true, Location = OutputCacheLocation.None)]
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include = "CARNET,FECHA_ADICION,PROCESADO")] COLA_ESTUDIANTES cOLA_ESTUDIANTES)
        {
            string fechaString = Request.Form["FECHA_ADICION"];
           

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
        [Authorize][OutputCache(NoStore = true, Location = OutputCacheLocation.None)]
        public ActionResult Delete(string CARNET, DateTime FECHA_ADICION)
        {
            if (CARNET == null || FECHA_ADICION == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            COLA_ESTUDIANTES cOLA_ESTUDIANTES = db.COLA_ESTUDIANTES.Find(CARNET,FECHA_ADICION);
            if (cOLA_ESTUDIANTES == null)
            {
                return HttpNotFound();
            }
            return View(cOLA_ESTUDIANTES);
        }

        // POST: COLA_ESTUDIANTES/Delete/5
        [Authorize][OutputCache(NoStore = true, Location = OutputCacheLocation.None)]
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(string CARNET, DateTime FECHA_ADICION)
        {
            COLA_ESTUDIANTES cOLA_ESTUDIANTES = db.COLA_ESTUDIANTES.Find(CARNET, FECHA_ADICION);
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
