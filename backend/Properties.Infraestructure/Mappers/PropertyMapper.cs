using Properties.Domain.Entities;
using Properties.Infraestructure.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Properties.Infraestructure.Mappers
{
    public static class PropertyMapper
    {
        public static PropertyModel ToDocument(this Property prop)
        {
            return new PropertyModel
            {
                IdProperty = prop.IdProperty,
                Name = prop.Name,
                Address = prop.Address,
                Price = prop.Price,
                Year = prop.Year,
                CodeInternal = prop.CodeInternal,
                OwnerId = Guid.Parse(prop.OwnerId)
            };
        }

        public static Property ToDomain(this PropertyModel doc)
        {
            return new Property
            {
                IdProperty = doc.IdProperty,
                Name = doc.Name,
                Address = doc.Address,
                CodeInternal = doc.CodeInternal,
                Price = doc.Price,
                Year = doc.Year,
                OwnerId = doc.OwnerId.ToString()
            };
        }
    }
}
