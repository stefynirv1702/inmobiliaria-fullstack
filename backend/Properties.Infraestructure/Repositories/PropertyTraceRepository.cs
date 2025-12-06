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
    public class PropertyTraceRepository : IPropertyTraceRepository
    {
        private readonly AppDbContext _context;
        private readonly DbSet<PropertyTraceModel> _dbSet;

        public PropertyTraceRepository(AppDbContext context)
        {
            _context = context;
            _dbSet = context.Set<PropertyTraceModel>();
        }

        public async Task AddTraceProperty(PropertyTrace propertyt)
        {
            var propertyM = propertyt.ToDocument();
            await _dbSet.AddAsync(propertyM);
            await _context.SaveChangesAsync();
        }

        public async Task<IEnumerable<PropertyTrace>> GetTraceProperties()
        {
            var result = await _dbSet.ToListAsync();
            return result.Select(pt => pt.ToDomain()).ToList();
        }

        public async Task<IEnumerable<PropertyTrace>> GetPropertyTraceByPropertyAsync(string idProperty)
        {
            var modelList = await _dbSet
                .Where(t => t.PropertyId.ToString() == idProperty)
                .ToListAsync();

            return modelList.Select(t => t.ToDomain()).ToList();
        }
    }
}
