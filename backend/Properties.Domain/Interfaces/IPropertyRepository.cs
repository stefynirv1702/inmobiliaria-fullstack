using Properties.Domain.Entities;

namespace Properties.Domain.Interfaces
{
    public interface IPropertyRepository
    {
        Task<string> AddProperty(Property property);
        Task<Property> GetPropertyById(string id);
        Task<IEnumerable<Property>> GetPropertiesAsync(string? name, string? address, decimal? minPrice, decimal? maxPrice);
    }
}
