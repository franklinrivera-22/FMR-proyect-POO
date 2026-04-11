using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Threading.Tasks;

namespace DistributionWaterApp.Entities
{
    [Table("zonas")]
    public class ZonaEntity : BaseEntity
    {
        [Required]
        [StringLength(50)]
        [Column("nombre_zona")]
        public string Nombre { get; set; }

        [Range(0, int.MaxValue)]
        [Column("cantidad_casas")]
        public int CantidadCasas { get; set; }
       public virtual ICollection<TurnosAguaEntity> TurnosAgua { get; set; } = new List<TurnosAguaEntity>();
    }
}