using GestionTareas.DataAccessLayer;
using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace GestionTareas.Controllers
{
    public class ListaController : Controller
    {
        private GestionTareasDataContext db = new GestionTareasDataContext();

        // GET: Listas/Index
        public ActionResult Index()
        {
            if (Session["UsuarioID"] == null)
                return RedirectToAction("Login", "Usuario");

            int idUsuario = (int)Session["UsuarioID"];
            var listas = db.Listas.Where(l => l.ID_Usuario == idUsuario).ToList();
            return View(listas);
        }

        // GET: Listas/Crear
        public ActionResult Crear()
        {
            return View();
        }

        // POST: Listas/Crear
        [HttpPost]
        public ActionResult Crear(GestionTareas.DataAccessLayer.Listas nuevaLista)
        {
            
            if (Session["UsuarioID"] == null)
                return RedirectToAction("Login", "Usuario");

            if (ModelState.IsValid)
            {
                nuevaLista.ID_Usuario = (int)Session["UsuarioID"];
                db.Listas.InsertOnSubmit(nuevaLista);
                db.SubmitChanges();
                return RedirectToAction("Index");
            }

            return View(nuevaLista);
        }

        // GET: Listas/Editar/5
        public ActionResult Editar(int id)
        {
            var lista = db.Listas.FirstOrDefault(l => l.ID == id);
            if (lista == null || lista.ID_Usuario != (int)Session["UsuarioID"])
                return RedirectToAction("Index");

            return View(lista);
        }

        // POST: Listas/Editar/5
        [HttpPost]
        public ActionResult Editar(GestionTareas.DataAccessLayer.Listas listaEditada)
        {
            if (ModelState.IsValid)
            {
                var lista = db.Listas.FirstOrDefault(l => l.ID == listaEditada.ID);
                if (lista != null && lista.ID_Usuario == (int)Session["UsuarioID"])
                {
                    lista.Nombre = listaEditada.Nombre;
                    lista.Descripción = listaEditada.Descripción;
                    db.SubmitChanges();
                    return RedirectToAction("Index");
                }
            }

            return View(listaEditada);
        }

        // GET: Listas/Eliminar/5
        public ActionResult Eliminar(int id)
        {
            var lista = db.Listas.FirstOrDefault(l => l.ID == id);
            if (lista == null || lista.ID_Usuario != (int)Session["UsuarioID"])
                return RedirectToAction("Index");

            return View(lista);
        }

        // POST: Listas/Eliminar/5
        [HttpPost, ActionName("Eliminar")]
        public ActionResult ConfirmarEliminar(int id)
        {
            var lista = db.Listas.FirstOrDefault(l => l.ID == id);
            if (lista != null && lista.ID_Usuario == (int)Session["UsuarioID"])
            {
                db.Listas.DeleteOnSubmit(lista);
                db.SubmitChanges();
            }

            return RedirectToAction("Index");
        }
    }
}