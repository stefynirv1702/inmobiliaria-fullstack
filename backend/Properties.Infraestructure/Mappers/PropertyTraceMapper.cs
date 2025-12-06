using Properties.Domain.Entities;
using Properties.Infraestructure.Models;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Properties.Infraestructure.Mappers
{
    public static class PropertyTraceMapper
    {
        public static PropertyTraceModel ToDocument(this PropertyTrace prop)
        {
            return new PropertyTraceModel
            {
                IdPropertyTrace = prop.IdPropertyTrace,
                PropertyId = Guid.Parse(prop.PropertyId),
                Name = prop.Name,
                Value = prop.Value,
                Tax = prop.Tax,
                DateSale = prop.DateSale
            };
        }

        public static PropertyTrace ToDomain(this PropertyTraceModel doc)
        {
            return new PropertyTrace
            {
                IdPropertyTrace = doc.IdPropertyTrace,
                PropertyId = doc.PropertyId.ToString(),
                Name = doc.Name,
                Value = doc.Value,
                Tax = doc.Tax,
                DateSale = doc.DateSale
            };
        }
    }
}
