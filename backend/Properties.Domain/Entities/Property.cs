using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Properties.Domain.Entities
{
    public class Property
    {
        public Guid IdProperty { get; set; } = default!;
        public string Name { get; set; } = default!;
        public string Address { get; set; } = default!;
        public decimal Price { get; set; } = default!;
        public string CodeInternal { get; set; } = default!;
        public int Year { get; set; }
        public string OwnerId { get; set; } = default!;
    }
}
