using Properties.Domain.Entities;

namespace Properties.Domain.Interfaces
{
    public interface IOwnerRepository
    {
        Task<IEnumerable<Owner>> GetAllOwner();
        Task AddOwner(Owner owner);
    }
}
