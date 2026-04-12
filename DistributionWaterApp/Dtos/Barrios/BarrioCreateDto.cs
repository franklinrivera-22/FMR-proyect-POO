using System.ComponentModel.DataAnnotations;

namespace DistributionWaterApp.Dtos.Barrios
{
    public class BarrioCreateDto
    {
        [Display(Name = "Nombre")]
        [Required(ErrorMessage = "El {0} es requerido")]
        [StringLength(100, ErrorMessage = "El {0} debe tener máximo {1} caracteres",
            MinimumLength = 2)]
        public string Nombre { get; set; }

        [Display(Name = "Cantidad de casas")]
        [Required(ErrorMessage = "La {0} es requerida")]
        [Range(1, 10000, ErrorMessage = "La {0} debe estar entre {1} y {2}")]
        public int CantidadCasas { get; set; }
    }
}