using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Properties.Infraestructure.Models
{
    [Table("Property")]
    public class PropertyModel
    {
        [Key]
        public Guid IdProperty { get; set; }

        [Required]
        public string Name { get; set; } = default!;

        [Required]
        public string Address { get; set; } = default!;

        [Column(TypeName = "decimal(18,2)")]
        public decimal Price { get; set; }

        [Required]
        public string CodeInternal { get; set; } = default!;

        public int Year { get; set; }

        [Required]
        public Guid OwnerId { get; set; }

        // Navigation Property
        public OwnerModel Owner { get; set; } = default!;

        // Collection of images
        public ICollection<PropertyImageModel> Images { get; set; } = new List<PropertyImageModel>();
    }
}
