using System.ComponentModel.DataAnnotations;

namespace DistributionWaterApp.Dtos.TurnosAgua
{
    public class TurnoAguaCreateDto
    {
        [Display(Name = "Barrio")]
        [Required(ErrorMessage = "El {0} es requerido")]
        public string BarrioId { get; set; }

        [Display(Name = "Fecha")]
        [Required(ErrorMessage = "La {0} es requerida")]
        public DateTime Fecha { get; set; }

        [Display(Name = "Hora de inicio")]
        [Required(ErrorMessage = "La {0} es requerida")]
        [StringLength(5, ErrorMessage = "La {0} debe tener formato HH:mm")]
        public string HoraInicio { get; set; }

        [Display(Name = "Hora de fin")]
        [Required(ErrorMessage = "La {0} es requerida")]
        [StringLength(5, ErrorMessage = "La {0} debe tener formato HH:mm")]
        public string HoraFin { get; set; }

        [Display(Name = "Estado")]
        [Required(ErrorMessage = "El {0} es requerido")]
        [StringLength(20)]
        public string Estado { get; set; }

        [Display(Name = "Observaciones")]
        [StringLength(500, ErrorMessage = "Las {0} no pueden superar {1} caracteres")]
        public string Observaciones { get; set; }
    }
}