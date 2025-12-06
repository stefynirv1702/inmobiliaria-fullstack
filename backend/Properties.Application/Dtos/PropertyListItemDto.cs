using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Properties.Application.Dtos
{
    public class PropertyDto
    {
        public string IdOwner { get; set; } = string.Empty;
        public string IdProperty { get; set; } = string.Empty;
        public string Name { get; set; } = string.Empty;
        public string Address { get; set; } = string.Empty;
        public string CodeInternal { get; set; } = string.Empty;
        public decimal Price { get; set; }
        public int Year { get; set; }
        public string Image { get; set; } = string.Empty;
    }

    public class PropertyCreateDto
    {
        public string Name { get; set; } = string.Empty;
        public string Address { get; set; } = string.Empty;
        public int Year { get; set; }
        public decimal Price { get; set; }
        public string Image { get; set; } = string.Empty;
        public string IdOwner { get; set; } = string.Empty;
    }

    public class PropertyDetailDto
    {
        public string IdProperty { get; set; } = string.Empty;
        public string OwnerId { get; set; } = string.Empty;
        public string Name { get; set; } = string.Empty;
        public string Address { get; set; } = string.Empty;
        public string CodeInternal { get; set; } = string.Empty;
        public decimal Price { get; set; }
        public int Year { get; set; }
        public string Image { get; set; } = string.Empty;
        public List<PropertyTraceDto> Trace { get; set; }
    }

    public class PropertyTraceDto
    {
        public string IdTrace { get; set; }
        public DateTime DateSale { get; set; }
        public string Name { get; set; } = default!;
        public decimal Value { get; set; }
        public decimal Tax { get; set; }
    }

    public class PropertyTraceCreateDto
    {
        public string PropertyId { get; set; } = default!;
        public string Name { get; set; } = default!;
        public decimal Value { get; set; }
        public decimal Tax { get; set; }
    }
}
