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