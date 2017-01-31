using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Threading.Tasks;

namespace eQM
{
    [Table("References")]
    public class References
    {
        [Key]
        public int ReferenceId { get; set; }
        public string Reference { get; set; }
        public int MeasureId { get; set; }

        public virtual Measure Measure { get; set; }
    }
}