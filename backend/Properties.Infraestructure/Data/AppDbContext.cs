using Microsoft.EntityFrameworkCore;
using System.Collections.Generic;
using Properties.Domain.Entities;
using Properties.Infraestructure.Models;
namespace Properties.Infraestructure.Data
{
    public class AppDbContext: DbContext
    {
        public AppDbContext(DbContextOptions<AppDbContext> options)
             : base(options) { }
        public DbSet<OwnerModel> Owners { get; set; }
        public DbSet<PropertyModel> Properties { get; set; }
        public DbSet<PropertyImageModel> PropertiesImage { get; set; }
        public DbSet<PropertyTraceModel> PropertiesTrace { get; set; }
        
    }
}
