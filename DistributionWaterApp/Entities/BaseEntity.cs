using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Threading.Tasks;

namespace DistributionWaterApp.Entities
{
 public class BaseEntity
    {
        [Key]
        [Column("id")]
        public string Id { get; set; }

        [Column("created_by_id")]
        public string CreatedById { get; set; }

        [Column("created_date")]
        public string CreatedDate { get; set; }

        [Column("updated_by_id")]
        public string UpdatedById { get; set; }

        [Column("updated_date")]
        public string UpdatedDate { get; set; }
    }
}