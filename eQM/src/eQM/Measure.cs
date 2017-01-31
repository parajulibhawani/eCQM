using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Threading.Tasks;

namespace eQM
{
    [Table("Measures")]
    public class Measure
    {
        [Key]
        public int MeasureId { get; set; }
        public string Title { get; set; }
        public int MeasureNumber { get; set; }

        public string Rationale { get; set; }

        public string ClinicalGuidance { get; set; }
        public virtual ICollection<References> References { get; set; }
    }
}

