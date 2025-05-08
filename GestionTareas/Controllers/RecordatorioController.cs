using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using GestionTareas.DataAccessLayer;

namespace GestionTareas.Controllers
{
    public class RecordatorioController : Controller
    {
        GestionTareasDataContext DB = new GestionTareasDataContext();

        //Listar
        public ActionResult Listar(DateTime? fecha = null)
        {
            fecha ??= DateTime.Today;

                                  .Where(r => r.FechaAviso.Date == fecha.Value.Date)
                                  .ToList();

            ViewBag.Fecha = fecha.Value;
            return View(recordatorios);
        }

        //Crear
        public ActionResult Crear()
        {
            ViewBag.Tareas = new SelectList(DB.Tareas, "IdTarea", "titulo");
            return View();
        }

        // Post Crear
        [HttpPost]
        public ActionResult Crear(Recordatorio nuevoRecordatorio)
        {
            if (ModelState.IsValid)
            {
                DB.Recordatorio.InsertOnSubmit(nuevoRecordatorio);
                DB.SubmitChanges();
                return RedirectToAction("Listar");
            }

            ViewBag.Tareas = new SelectList(DB.Tareas, "IdTarea", "Titulo", nuevoRecordatorio.IdTarea);
            return View(nuevoRecordatorio);
        }
    }
}