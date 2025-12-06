using Properties.Domain.Entities;

namespace Properties.Domain.Interfaces
{
    public interface IPropertyImageRepository
    {
        Task<PropertyImage?> GetByIdPropertyImage(Guid id);
        Task<string> AddPropertyImage(PropertyImage property);
        Task<PropertyImage> GetPropertyImageByPropertyAsync(string idProperty);
    }
}
