using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Threading.Tasks;

namespace DistributionWaterApp.Entities
{
    [Table("barrios")]
    public class Barrio : BaseEntity
    {
        [Required]
        [StringLength(100)]
        [Column("nombre")]
        public string Nombre { get; set; }

        [Column("cantidad_casas")]
        public int CantidadCasas { get; set; }
        public virtual ICollection<TurnoAguaEntity> TurnosAgua { get; set; }
    }
}