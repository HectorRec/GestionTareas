using GestionTareas.DataAccessLayer;
using System;
using System.Collections.Generic;
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
        [HttpGet]
        public ActionResult Login()
        {
            return View();
        }

        [HttpPost]
        public ActionResult Login(string correo, string contrasena)
        {
            using (var db = new GestionTareasDataContext())
            {
                var usuario = db.Usuarios.FirstOrDefault(u => u.Correo_Electronico == correo && u.Correo_Electronico == contrasena);
                if (usuario != null)
                {
                    Session["UsuarioID"] = usuario.IdUsuario;
                    Session["NombreUsuario"] = usuario.Nombre;
                    return RedirectToAction("Index", "Home");
                }
                else
                {
                    ViewBag.Mensaje = "Correo o contraseña incorrectos.";
                    return View();
                }
            }
        }


    }


}
