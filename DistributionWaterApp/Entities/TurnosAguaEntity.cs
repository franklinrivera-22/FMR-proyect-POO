using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Threading.Tasks;

namespace DistributionWaterApp.Entities
{
        [Table("turnos_agua")]
        public class TurnosAguaEntity : BaseEntity
        {
            [Required]
            [Column("zona_id")]
            public string ZonaId { get; set; }

            [ForeignKey(nameof(ZonaId))]
            public virtual ZonaEntity Zona { get; set; }

            [Required]
            [Column("fecha")]
            public DateTime Fecha { get; set; }

            [Required]
            [Column("hora_inicio")]
            public TimeSpan HoraInicio { get; set; }

            [Required]
            [Column("hora_fin")]
            public TimeSpan HoraFin { get; set; }

            [Required]
            [StringLength(20)]
            [Column("estado")]
            public string Estado { get; set; }

            [StringLength(500)]
            [Column("observaciones")]
            public string Observaciones { get; set; }
        }
    }
