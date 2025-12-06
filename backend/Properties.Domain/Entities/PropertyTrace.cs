using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Properties.Domain.Entities
{
    public class PropertyTrace
    {
        public Guid IdPropertyTrace { get; set; } = default!;
        public string PropertyId { get; set; } = default!;
        public DateTime DateSale { get; set; }
        public string Name { get; set; } = default!;
        public decimal Value { get; set; }
        public decimal Tax { get; set; }
    }
}
