using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Properties.Infraestructure.Models
{
    [Table("PropertyTrace")]
    public class PropertyTraceModel
    {
        [Key]
        public Guid IdPropertyTrace { get; set; }

        public Guid PropertyId { get; set; }

        public DateTime DateSale { get; set; }

        [MaxLength(200)]
        public string Name { get; set; } = default!;

        [Column(TypeName = "decimal(18,2)")]
        public decimal Value { get; set; }

        [Column(TypeName = "decimal(18,2)")]
        public decimal Tax { get; set; }

    }
}

