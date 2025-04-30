using GestionTareas.DataAccessLayer;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading;
using System.Web;
using System.Web.Mvc;

namespace GestionTareas.Controllers
{
    public class TareasController : Controller
    {


        GestionTareasDataContext bd = new GestionTareasDataContext();

        // GET: Tareas/Crear
        public ActionResult Crear()
        {
            ViewBag.IdUsuario = new SelectList(bd.Usuarios, "ID", "Nombre");
            ViewBag.IdLista = new SelectList(bd.Listas, "ID", "Nombre");
            return View();
        }

        // POST: Tareas/Crear
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Crear(Tareas tarea)
        {
            if (ModelState.IsValid)
            {
                try
                {
                    bd.Tareas.InsertOnSubmit(tarea);
                    bd.SubmitChanges();
                    return RedirectToAction("Index");
                }
                catch (Exception ex)
                {
                    ModelState.AddModelError("", "Error al crear la tarea: " + ex.Message);
                }
            }

            ViewBag.IdUsuario = new SelectList(bd.Usuarios, "ID", "Nombre", tarea.id_usuario);
            ViewBag.IdLista = new SelectList(bd.Listas, "ID", "Nombre", tarea.id_lista);
            return View(tarea);
        }

        // GET: Tareas/Editar/5
        public ActionResult Editar(int id)
        {
            var tarea = bd.Tareas.FirstOrDefault(t => t.ID == id);
            if (tarea == null)
            {
                return HttpNotFound();
            }

            ViewBag.IdUsuario = new SelectList(bd.Usuarios, "ID", "Nombre", tarea.id_usuario);
            ViewBag.IdLista = new SelectList(bd.Listas, "ID", "Nombre", tarea.id_lista);
            return View(tarea);
        }

        // POST: Tareas/Editar/5
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Editar(Tareas tarea)
        {
            if (ModelState.IsValid)
            {
                var tareaExistente = bd.Tareas.FirstOrDefault(t => t.ID == tarea.ID);
                if (tareaExistente != null)
                {
                    // Actualizar propiedades
                    tareaExistente.titulo = tarea.titulo;
                    tareaExistente.descripcion = tarea.descripcion;
                    tareaExistente.fecha_limite = tarea.fecha_limite;
                    tareaExistente.estado = tarea.estado;
                    tareaExistente.prioridad = tarea.prioridad;
                    tareaExistente.id_usuario = tarea.id_usuario;
                    tareaExistente.id_lista = tarea.id_lista;

                    try
                    {
                        bd.SubmitChanges();
                        return RedirectToAction("Index");
                    }
                    catch (Exception ex)
                    {
                        ModelState.AddModelError("", "Error al actualizar la tarea: " + ex.Message);
                    }
                }
                else
                {
                    return HttpNotFound();
                }
            }

            ViewBag.IdUsuario = new SelectList(bd.Usuarios, "ID", "Nombre", tarea.id_usuario);
            ViewBag.IdLista = new SelectList(bd.Listas, "ID", "Nombre", tarea.id_lista);
            return View(tarea);
        }

        // GET: Tareas/Eliminar/5
        public ActionResult Eliminar(int id)
        {
            var tarea = bd.Tareas.FirstOrDefault(t => t.ID == id);
            if (tarea == null)
            {
                return HttpNotFound();
            }
            return View(tarea);
        }

        // POST: Tareas/Eliminar/5
        [HttpPost, ActionName("Eliminar")]
        [ValidateAntiForgeryToken]
        public ActionResult EliminarConfirmado(int id)
        {
            var tarea = bd.Tareas.FirstOrDefault(t => t.ID == id);
            if (tarea != null)
            {
                try
                {
                    bd.Tareas.DeleteOnSubmit(tarea);
                    bd.SubmitChanges();
                }
                catch (Exception ex)
                {
                    ModelState.AddModelError("", "Error al eliminar la tarea: " + ex.Message);
                    return View("Eliminar", tarea);
                }
            }
            return RedirectToAction("Index");
        }

        protected override void Dispose(bool disposing)
        {
            if (disposing)
            {
                bd.Dispose();
            }
            base.Dispose(disposing);
        }


        // GET: Tareas
        public ActionResult Index()
        {
            return View();
        }
    }
}