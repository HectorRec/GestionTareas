using GestionTareas.DataAccessLayer;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations; // Para atributos de validación si usaras un ViewModel
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace GestionTareas.Controllers
{
    public class UsuariosController : Controller
    {
        GestionTareasDataContext bd = new GestionTareasDataContext();
        /// <summary>
        /// ///////////////////registro de usuario
        /// </summary>
        /// <returns></returns>
        [HttpGet]
        public ActionResult Registro()
        {
            return View();
        }

        [HttpPost]
        public ActionResult Registro(Usuarios nuevoUsuario)
        {
            if (ModelState.IsValid)
            {
                using (var db = new GestionTareasDataContext())
                {
                    var existe = db.Usuarios.Any(u => u.Correo_Electronico == nuevoUsuario.Correo_Electronico);
                    if (existe)
                    {
                        ViewBag.Mensaje = "El correo ya está registrado.";
                        return View();
                    }

                    nuevoUsuario.Fecha_Registro = DateTime.Now;
                    db.Usuarios.InsertOnSubmit(nuevoUsuario);
                    db.SubmitChanges();

                    return RedirectToAction("Login");
                }
            }

            return View(nuevoUsuario);
        }




        ////////////////LOGIN/////////////////////
        ///
        // GET: Usuarios/Login (Necesitas una acción GET para mostrar la vista inicialmente)
        public ActionResult Login()
        {
            // Si el usuario ya está autenticado, quizás redirigirlo a Home/Index
            if (Session["UsuarioID"] != null)
            {
                return RedirectToAction("Index", "Home");
            }
            return View();
        }

        [HttpPost]
        [ValidateAntiForgeryToken] // Muy importante para seguridad
        public ActionResult Login(string correo, string contrasena)
        {
            // 1. Validaciones básicas del lado del servidor
            if (string.IsNullOrWhiteSpace(correo))
            {
                ViewBag.Mensaje = "El correo electrónico es requerido.";
                return View(); // Vuelve a mostrar la vista de login
            }

            // Validación de formato de correo simple (puedes usar Regex para algo más robusto)
            if (!correo.Contains("@") || !correo.Contains(".")) // Validación muy básica
            {
                ViewBag.Mensaje = "El formato del correo electrónico no es válido.";
                return View();
            }

            // Eliminar espacios extra
            correo = correo.Trim();

            if (string.IsNullOrWhiteSpace(contrasena))
            {
                ViewBag.Mensaje = "La contraseña es requerida.";
                return View();
            }

            using (var db = new GestionTareasDataContext()) // Asegúrate que este es el nombre correcto de tu DataContext
            {
                var usuario = db.Usuarios.FirstOrDefault(u => u.Correo_Electronico == correo && u.Contrasenia == contrasena);

                if (usuario != null)
                {
                    Session["UsuarioID"] = usuario.ID;
                    Session["NombreUsuario"] = usuario.Nombre;
                    // Considera también almacenar el rol del usuario si lo tienes: Session["RolUsuario"] = usuario.Rol;

                    // Podrías querer registrar el último login
                    // usuario.FechaUltimoLogin = DateTime.Now;
                    // db.SubmitChanges();

                    return RedirectToAction("Index", "Home");
                }
                else
                {
                    ViewBag.Mensaje = "Correo o contraseña incorrectos.";
                    return View(); // Vuelve a mostrar la vista Login.cshtml (asegúrate que existe en Views/Usuarios/Login.cshtml)
                }
            }
        }

        ////////////////LOGOUT/////////////////////
        ///
        public ActionResult Logout()
        {
            Session.Clear();
            return RedirectToAction("Login");
        }

        ////////perfil de usuario/////////////
        ///


        public ActionResult Perfil()
        {
            if (Session["UsuarioID"] == null)
                return RedirectToAction("Login");

            using (var db = new GestionTareasDataContext())
            {
                int id = (int)Session["UsuarioID"];
                var usuario = db.Usuarios.FirstOrDefault(u => u.ID == id);
                return View(usuario);
            }
        }


    }


}
