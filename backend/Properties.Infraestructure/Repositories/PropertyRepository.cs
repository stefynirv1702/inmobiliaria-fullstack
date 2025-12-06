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
    public class PropertyRepository : IPropertyRepository
    {
        private readonly AppDbContext _context;
        private readonly DbSet<PropertyModel> _dbSet;

        public PropertyRepository(AppDbContext context)
        {
            _context = context;
            _dbSet = _context.Set<PropertyModel>();
        }

        public async Task<string> AddProperty(Property property)
        {
            var propertyModel = property.ToDocument();

            await _dbSet.AddAsync(propertyModel);
            await _context.SaveChangesAsync();

            return propertyModel.IdProperty.ToString();
        }

        public async Task<Property> GetPropertyById(string id)
        {
            var guid = Guid.Parse(id);

            var model = await _dbSet.FirstOrDefaultAsync(p => p.IdProperty == guid);

            if (model == null)
                throw new Exception("Property not found");

            return model.ToDomain();
        }

        public async Task<IEnumerable<Property>> GetPropertiesAsync(string? name, string? address, decimal? minPrice, decimal? maxPrice)
        {
            IQueryable<PropertyModel> query = _dbSet.AsQueryable();

            if (!string.IsNullOrEmpty(name))
                query = query.Where(p => p.Name == name);

            if (!string.IsNullOrEmpty(address))
                query = query.Where(p => p.Address == address);

            if (minPrice.HasValue)
                query = query.Where(p => p.Price >= minPrice);

            if (maxPrice.HasValue)
                query = query.Where(p => p.Price <= maxPrice);

            var list = await query.ToListAsync();

            return list.Select(x => x.ToDomain());
        }
    }
}
