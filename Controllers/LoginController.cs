using ProyectoProgra2.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using System.Web.Security;

namespace ProyectoProgra2.Controllers
{
    public class LoginController : Controller
    {
        private Entities db = new Entities();

        // GET: Login
        public ActionResult Index()
        {
            return View();
        }
        [HttpPost]
        public ActionResult Login(USUARIOS model)
        {
            if (ModelState.IsValid)
            {
                // Buscar al usuario en la base de datos por nombre de usuario o correo electrónico
                var user = db.USUARIOS.FirstOrDefault(u => u.USUARIO == model.USUARIO);

                if (user != null && user.CONTRASENA == model.CONTRASENA)
                {
                    // Iniciar una sesión para el usuario
                    FormsAuthentication.SetAuthCookie(user.USUARIO, true);
                    // Redirigir al usuario a la página de inicio
                    return RedirectToAction("Index", "Home");
                }
                else
                {
                    ModelState.AddModelError("", "Nombre de usuario o contraseña incorrectos.");
                }
            }
            return View(model);
        }

        public ActionResult Logout()
        {
            // Cerrar la sesión del usuario
            FormsAuthentication.SignOut();

            // Redirigir al usuario a la página de inicio o a la página de inicio de sesión
            return RedirectToAction("Index", "Home");
        }
    }
}
