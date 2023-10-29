using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Entity;
using System.Data.Entity.Core.Common.CommandTrees;
using System.Linq;
using System.Net;
using System.Web;
using System.Web.Mvc;
using System.Web.UI;
using ProyectoProgra2.Models;

namespace ProyectoProgra2.Controllers
{
    public class INSCRIPCION_ESTUDIANTESController : Controller
    {
        private Entities db = new Entities();

        // GET: INSCRIPCION_ESTUDIANTES
        [Authorize]
        [OutputCache(NoStore = true, Location = OutputCacheLocation.None)]
        public ActionResult Index()
        {
            var iNSCRIPCION_ESTUDIANTES = db.INSCRIPCION_ESTUDIANTES.Include(i => i.ESTUDIANTES).OrderBy(e => e.FECHA_ADICION);
            return View(iNSCRIPCION_ESTUDIANTES.ToList());
        }

        // GET: INSCRIPCION_ESTUDIANTES/Details/5
        [Authorize]
        [OutputCache(NoStore = true, Location = OutputCacheLocation.None)]
        public ActionResult Details(string CARNET,DateTime FECHA_ADICION)
        {
            if (CARNET == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            INSCRIPCION_ESTUDIANTES iNSCRIPCION_ESTUDIANTES = db.INSCRIPCION_ESTUDIANTES.Find(CARNET, FECHA_ADICION);
            if (iNSCRIPCION_ESTUDIANTES == null)
            {
                return HttpNotFound();
            }
            return View(iNSCRIPCION_ESTUDIANTES);
        }

        // GET: INSCRIPCION_ESTUDIANTES/Create
        [Authorize]
        [OutputCache(NoStore = true, Location = OutputCacheLocation.None)]
        public ActionResult Create()
        {
            var carnetsEstudiantesNoProcesados = db.COLA_ESTUDIANTES
                                        .Where(e => e.PROCESADO == false)
                                        .OrderBy(e => e.FECHA_ADICION)
                                        .FirstOrDefault();
            if(carnetsEstudiantesNoProcesados != null)
            {
                var estudiantes = db.ESTUDIANTES
                    .Where(estudiante => estudiante.CARNET == carnetsEstudiantesNoProcesados.CARNET)
                    .ToList() ?? new List<ESTUDIANTES>();
                    ViewBag.CARNET = new SelectList(estudiantes, "CARNET", "NOMBRE");
            }
            else
            {
                var estudiantes = new List<ESTUDIANTES>();
                ViewBag.CARNET = new SelectList(estudiantes, "CARNET", "NOMBRE");
            }



           

            return View();
        }

        // POST: INSCRIPCION_ESTUDIANTES/Create
        // Para protegerse de ataques de publicación excesiva, habilite las propiedades específicas a las que quiere enlazarse. Para obtener 
        // más detalles, vea https://go.microsoft.com/fwlink/?LinkId=317598.
        [Authorize]
        [OutputCache(NoStore = true, Location = OutputCacheLocation.None)]
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create([Bind(Include = "CARNET,FECHA_ADICION")] INSCRIPCION_ESTUDIANTES iNSCRIPCION_ESTUDIANTES)
        {
            if (ModelState.IsValid)
            {
                var carnet = iNSCRIPCION_ESTUDIANTES.CARNET;
                var estudianteEnCola = db.COLA_ESTUDIANTES
                                        .Where(e => e.PROCESADO == false)
                                        .OrderBy(e => e.FECHA_ADICION)
                                        .FirstOrDefault();

                if (estudianteEnCola != null)
                {
                    estudianteEnCola.PROCESADO = true;
                    db.Entry(estudianteEnCola).State = EntityState.Modified;

                    iNSCRIPCION_ESTUDIANTES.FECHA_ADICION = DateTime.Now;
                    db.INSCRIPCION_ESTUDIANTES.Add(iNSCRIPCION_ESTUDIANTES);

                    // Guarda los cambios en la base de datos
                    db.SaveChanges();
                }

                return RedirectToAction("Index");
            }

            ViewBag.CARNET = new SelectList(db.ESTUDIANTES, "CARNET", "NOMBRE", iNSCRIPCION_ESTUDIANTES.CARNET);
            return View(iNSCRIPCION_ESTUDIANTES);
        }

        // GET: INSCRIPCION_ESTUDIANTES/Edit/5
        [Authorize]
        [OutputCache(NoStore = true, Location = OutputCacheLocation.None)]
        public ActionResult Edit(string CARNET,DateTime FECHA_ADICION)
        {
            if (CARNET == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            INSCRIPCION_ESTUDIANTES iNSCRIPCION_ESTUDIANTES = db.INSCRIPCION_ESTUDIANTES.Find(CARNET,FECHA_ADICION);
            if (iNSCRIPCION_ESTUDIANTES == null)
            {
                return HttpNotFound();
            }
            ViewBag.CARNET = new SelectList(db.ESTUDIANTES, "CARNET", "NOMBRE", iNSCRIPCION_ESTUDIANTES.CARNET);
            return View(iNSCRIPCION_ESTUDIANTES);
        }

        // POST: INSCRIPCION_ESTUDIANTES/Edit/5
        // Para protegerse de ataques de publicación excesiva, habilite las propiedades específicas a las que quiere enlazarse. Para obtener 
        // más detalles, vea https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        [Authorize]
        [OutputCache(NoStore = true, Location = OutputCacheLocation.None)]
        public ActionResult Edit([Bind(Include = "CARNET,FECHA_ADICION")] INSCRIPCION_ESTUDIANTES iNSCRIPCION_ESTUDIANTES)
        {
            if (ModelState.IsValid)
            {
                db.Entry(iNSCRIPCION_ESTUDIANTES).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            ViewBag.CARNET = new SelectList(db.ESTUDIANTES, "CARNET", "NOMBRE", iNSCRIPCION_ESTUDIANTES.CARNET);
            return View(iNSCRIPCION_ESTUDIANTES);
        }

        // GET: INSCRIPCION_ESTUDIANTES/Delete/5
        [Authorize]
        [OutputCache(NoStore = true, Location = OutputCacheLocation.None)]
        public ActionResult Delete(string CARNET, DateTime FECHA_ADICION)
        {
            if (CARNET == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            INSCRIPCION_ESTUDIANTES iNSCRIPCION_ESTUDIANTES = db.INSCRIPCION_ESTUDIANTES.Find(CARNET,FECHA_ADICION);
            if (iNSCRIPCION_ESTUDIANTES == null)
            {
                return HttpNotFound();
            }
            return View(iNSCRIPCION_ESTUDIANTES);
        }

        // POST: INSCRIPCION_ESTUDIANTES/Delete/5
        [Authorize]
        [OutputCache(NoStore = true, Location = OutputCacheLocation.None)]
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(string CARNET, DateTime FECHA_ADICION)
        {
            INSCRIPCION_ESTUDIANTES iNSCRIPCION_ESTUDIANTES = db.INSCRIPCION_ESTUDIANTES.Find(CARNET, FECHA_ADICION);
            db.INSCRIPCION_ESTUDIANTES.Remove(iNSCRIPCION_ESTUDIANTES);
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
