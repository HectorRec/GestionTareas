using FrontGestionTareas.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.AspNetCore.Mvc.Rendering;

namespace FrontGestionTareas.Pages
{ 
public class TareasModel : PageModel
{
    [BindProperty]
    public Tarea NuevaTarea { get; set; }

    [BindProperty]
    public int? TareaIdEnEdicion { get; set; }

        public List<Tarea> ListaTareas { get; set; } = new();

    // Simulamos base de datos en memoria (luego puedes usar EF)
    private static List<Tarea> _tareasDb = new();


        public SelectList EstatusTareas { get; set; }

        public TareasModel()
        {
            EstatusTareas = new SelectList(new[] { "Pendiente", "En progreso", "Completada" });
        }


        public void OnGet()
        {
            // Si no hay tareas previas, se inicializa la fecha por defecto
            NuevaTarea = new Tarea
            {
                fecha_limite = DateTime.Now.AddDays(1) // Por ejemplo, un día después por defecto
            };

            ListaTareas = _tareasDb;
        }

        public IActionResult OnPostAgregar()
        {
            string fechaStr = Request.Form["fecha_limite"];
            string horaStr = Request.Form["hora_limite"];

            if (DateTime.TryParse(fechaStr, out var fecha) &&
                TimeSpan.TryParse(horaStr, out var hora))
            {
                NuevaTarea.fecha_creacion = DateTime.Now;
                NuevaTarea.fecha_limite = fecha.Date.Add(hora);

                NuevaTarea.ID = _tareasDb.Count + 1;
                _tareasDb.Add(NuevaTarea);
            }

            return RedirectToPage();
        }

        [BindProperty]
        public Tarea TareaEditada { get; set; }


        // Método para editar una tarea
        public IActionResult OnPostEditarAsync(DateTime fecha_limite, string hora_limite)
        {
            if (TareaEditada == null)
                return Page();

            // Combinar fecha y hora
            if (TimeSpan.TryParse(hora_limite, out var hora))
            {
                TareaEditada.fecha_limite = fecha_limite.Date + hora;
            }

            // Buscar la tarea y actualizarla
            var tareaExistente = _tareasDb.FirstOrDefault(t => t.ID == TareaEditada.ID);
            if (tareaExistente != null)
            {
                tareaExistente.titulo = TareaEditada.titulo;
                tareaExistente.descripcion = TareaEditada.descripcion;
                tareaExistente.fecha_limite = TareaEditada.fecha_limite;
                tareaExistente.prioridad = TareaEditada.prioridad;
                tareaExistente.estado = TareaEditada.estado;
            }

            // **Recarga la lista** para que la vista la muestre
            ListaTareas = _tareasDb;

            // **Limpia el estado de edición** para que vuelvan las filas normales
            TareaIdEnEdicion = null;

            // Redirige a GET para limpiar el formulario (o puedes usar `return Page();`)
            return RedirectToPage();
        }




        public IActionResult OnPostIniciarEdicion(int id)
        {
            TareaIdEnEdicion = id;

            // Esto es importante para mantener la lista de tareas y mostrar la página de nuevo
            ListaTareas = _tareasDb;
            return Page();
        }



        public IActionResult OnPostEliminar(int id)
    {
        _tareasDb.RemoveAll(t => t.ID == id);
        return RedirectToPage();
    }
}
}
