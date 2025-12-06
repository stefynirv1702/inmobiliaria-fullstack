using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Properties.Infraestructure.Models
{
    [Table("Owner")]
    public class OwnerModel
    {
        [Key]
        public Guid IdOwner { get; set; }
        [Required]
        [MaxLength(200)]
        public string Name { get; set; }

        [MaxLength(300)]
        public string Address { get; set; }
        public string Photo { get; set; } = default!;
        public DateTime Birthday { get; set; }
    }
}
