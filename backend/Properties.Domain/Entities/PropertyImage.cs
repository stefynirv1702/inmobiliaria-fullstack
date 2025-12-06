using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Properties.Domain.Entities
{
    public class PropertyImage
    {
        public Guid IdPropertyImage { get; set; } = default!;
        public string PropertyId { get; set; } = default!;
        public string File { get; set; } = default!;
        public bool Enabled { get; set; }
    }
}
