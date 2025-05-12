using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using GestionTareas.DataAccessLayer;

namespace GestionTareas.Controllers
{


    public class CalendarioController : Controller
    {

        GestionTareasDataContext DB = new GestionTareasDataContext();


        // NUEVA ACCIÓN PARA EL CALENDARIO
        public ActionResult Calendario()
        {
            ViewBag.Title = "Calendario de Tareas"; // Título de la página
            return View(); // Esto buscará una vista llamada Calendario.cshtml en Views/Home/
        }

        // En TareasController.cs (o donde gestiones tus tareas)
        public JsonResult GetEventosCalendario(DateTime start, DateTime end) // FullCalendar envía 'start' y 'end' de la vista actual
        {
            using (var db = new GestionTareasDataContext())
            {
                // Ajusta la consulta según los campos de fecha de tu tabla Tareas
                // y cómo quieres que se representen en el calendario.
                var eventos = db.Tareas
                                .Where(t => t.fecha_limite >= start && t.fecha_creacion <= end ) // Ejemplo de filtro
                                .Select(t => new {
                                    id = t.ID, // ID del evento
                                    title = t.titulo, // Título del evento
                                    start = t.fecha_creacion.ToString(), // Formato ISO 8601 (yyyy-MM-ddTHH:mm:ss.fffZ)
                                    end = t.fecha_limite.ToString(), // Formato ISO 8601
                                                                            // allDay = (t.FechaInicio.TimeOfDay == TimeSpan.Zero && t.FechaVencimiento.TimeOfDay == TimeSpan.Zero), // Lógica para determinar si es todo el día
                                                                            // color = t.Prioridad == "Alta" ? "red" : (t.Prioridad == "Media" ? "orange" : "green"), // Ejemplo de colores
                                                                            // url = Url.Action("Details", "Tareas", new { id = t.ID }) // Enlace al detalle de la tarea
                                })
                                .ToList();
                return Json(eventos, JsonRequestBehavior.AllowGet);
            }
        }

        //public ActionResult Tareas(string vista = "tareaslista", DateTime? fecha = null)
        //{
        //    fecha ??= DateTime.Today;

        //    IQueryable<Tarea> tareasQuery = DB.Tareas;

        //    DateTime inicio, fin;

        //    switch (vista.ToLower())
        //    {
        //        case "dia":
        //            inicio = fecha.Value.Date;
        //            fin = inicio.AddDays(1);
        //            break;
        //        case "semana":
        //            int diff = (7 + (fecha.Value.DayOfWeek - DayOfWeek.Monday)) % 7;
        //            inicio = fecha.Value.AddDays(-diff).Date;
        //            fin = inicio.AddDays(7);
        //            break;
        //        default: // mes
        //            inicio = new DateTime(fecha.Value.Year, fecha.Value.Month, 1);
        //            fin = inicio.AddMonths(1);
        //            break;
        //    }

        //    var tareas = tareasQuery
        //                 .Where(t => t.Fecha >= inicio && t.Fecha < fin)
        //                 .ToList();

        //    ViewBag.Vista = vista;
        //    ViewBag.Fecha = fecha.Value;

        //    return View(tareas);
        //}
        //}
    }



}