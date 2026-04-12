using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Threading.Tasks;

namespace DistributionWaterApp.Entities
{
    [Table("turnos_agua")]
    public class TurnoAguaEntity : BaseEntity
    {
        [Required]
        [Column("barrio_id")]
        public string BarrioId { get; set; }

        [ForeignKey(nameof(BarrioId))]
        public virtual Barrio Barrio { get; set; }

        [Required]
        [Column("fecha")]
        public DateTime Fecha { get; set; }

        [Required]
        [StringLength(5)]
        [Column("hora_inicio")]
        public string HoraInicio { get; set; }

        [Required]
        [StringLength(5)]
        [Column("hora_fin")]
        public string HoraFin { get; set; }

        [Required]
        [StringLength(20)]
        [Column("estado")]
        public string Estado { get; set; }

        [StringLength(500)]
        [Column("observaciones")]
        public string Observaciones { get; set; }
    }
}
