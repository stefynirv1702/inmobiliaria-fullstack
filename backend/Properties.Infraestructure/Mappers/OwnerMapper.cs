using Properties.Domain.Entities;
using Properties.Infraestructure.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Properties.Infraestructure.Mappers
{
    public static class OwnerMapper
    {
        public static OwnerModel ToDocument(this Owner owner)
        {
            return new OwnerModel
            {
                IdOwner = owner.IdOwner,
                Name = owner.Name,
                Address = owner.Address,
                Photo = owner.Photo,
                Birthday = owner.Birthday
            };
        }

        public static Owner ToDomain(this OwnerModel doc)
        {
            return new Owner
            {
                IdOwner = doc.IdOwner,
                Name = doc.Name,
                Address = doc.Address,
                Photo = doc.Photo,
                Birthday = doc.Birthday
            };
        }
    }
}
