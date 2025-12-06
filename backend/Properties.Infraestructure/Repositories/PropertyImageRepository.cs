using Microsoft.EntityFrameworkCore;
using Properties.Domain.Entities;
using Properties.Domain.Interfaces;
using Properties.Infraestructure.Data;
using Properties.Infraestructure.Mappers;
using Properties.Infraestructure.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Properties.Infraestructure.Repositories
{
    public class PropertyImageRepository
        : SqlRepository<PropertyImageModel, Guid>, IPropertyImageRepository
    {
        private readonly AppDbContext _context;

        public PropertyImageRepository(AppDbContext context)
            : base(context)
        {
            _context = context;
        }

        public async Task<string> AddPropertyImage(PropertyImage propertyImage)
        {
            PropertyImageModel propertyImageM = propertyImage.ToDocument();
            await this.AddAsync(propertyImageM);
            return propertyImageM.IdPropertyImage.ToString();
        }

        public async Task<PropertyImage?> GetByIdPropertyImage(Guid id)
        {
            var result = await this.GetByIdAsync(id);

            if (result == null)
                return null;

            return result.ToDomain();
        }

        public async Task<PropertyImage?> GetPropertyImageByPropertyAsync(string idProperty)
        {
            // LINQ con EF Core
            var result = await _context.PropertiesImage
                .FirstOrDefaultAsync(p => p.PropertyId.ToString() == idProperty);

            return result?.ToDomain();
        }
    }
}
