using System.ComponentModel.DataAnnotations;

namespace FrontGestionTareas.Models
{
    public class Tarea
    {
        public int ID { get; set; }

        [Required(ErrorMessage = "El título es obligatorio.")]
        public string titulo { get; set; }

        public string descripcion { get; set; }

        [Required(ErrorMessage = "La fecha es obligatoria.")]
        public DateTime fecha_creacion { get; set; }

        public DateTime fecha_limite { get; set; }

        public string estado  { get; set; }

        [Required]
        public int prioridad { get; set; }

        public int id_usuario { get; set; }

        public int id_lista { get; set; }


    }
}
