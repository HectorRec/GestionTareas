using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using GestionTareas.DataAccessLayer;

namespace GestionTareas.Controllers
{

   
    public class HomeController : Controller
    {

        GestionTareasDataContext DB = new GestionTareasDataContext();
        public ActionResult Index()
        {
            return View();
        }

        public ActionResult About()
        {
            ViewBag.Message = "Your application description page.";

            return View();
        }

        public ActionResult Contact()
        {
            ViewBag.Message = "Your contact page.";

            return View();
        }

        public ActionResult Tareas()
        {

            //EJEMPLO DE OBTENER UNA TAREA DE LA BASE DE DATOS.
            var tareas = DB.Tareas.ToList().FirstOrDefault();
            return View(tareas);
        }



    }
}