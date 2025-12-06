using Properties.Domain.Entities;

namespace Properties.Domain.Interfaces
{
    public interface IPropertyTraceRepository
    {
        Task AddTraceProperty(PropertyTrace property);
        Task<IEnumerable<PropertyTrace>> GetTraceProperties();
        Task<IEnumerable<PropertyTrace>> GetPropertyTraceByPropertyAsync(string idProperty);
    }
}
