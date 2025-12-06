using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Properties.Infraestructure.Models
{
    [Table("PropertyImage")]
    public class PropertyImageModel
    {
        [Key]
        public Guid IdPropertyImage { get; set; }

        // Foreign Key
        [Required]
        public Guid PropertyId { get; set; }

        [Required]
        public string File { get; set; } = default!;

        public bool Enabled { get; set; }

        // Navigation Property
        public PropertyModel Property { get; set; } = default!;
    }
}
