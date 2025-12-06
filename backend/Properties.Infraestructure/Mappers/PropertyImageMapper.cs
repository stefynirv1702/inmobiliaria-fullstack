using Properties.Domain.Entities;
using Properties.Infraestructure.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Properties.Infraestructure.Mappers
{
    public static class PropertyImageMapper
    {
        public static PropertyImageModel ToDocument(this PropertyImage prop)
        {
            return new PropertyImageModel
            {
                IdPropertyImage = prop.IdPropertyImage,
                PropertyId = Guid.Parse(prop.PropertyId),
                File = prop.File,
                Enabled = prop.Enabled
            };
        }

        public static PropertyImage ToDomain(this PropertyImageModel doc)
        {
            if (doc == null)
            {
                return null;

            }

            return new PropertyImage
            {
                IdPropertyImage = doc.IdPropertyImage,
                PropertyId = doc.PropertyId.ToString(),
                File = doc.File,
                Enabled = doc.Enabled
            };
        }
    }
}
