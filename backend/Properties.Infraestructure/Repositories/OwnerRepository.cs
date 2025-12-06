using Microsoft.EntityFrameworkCore;
using Properties.Domain.Entities;
using Properties.Domain.Interfaces;
using Properties.Infraestructure.Data;
using Properties.Infraestructure.Mappers;
using Properties.Infraestructure.Models;

namespace Properties.Infraestructure.Repositories
{
    public class OwnerRepository : SqlRepository<OwnerModel, Guid>, IOwnerRepository
    {
        public OwnerRepository(AppDbContext context)
            : base(context)
        {

        }
        public async Task AddOwner(Owner owner)
        {
            OwnerModel ownerM = owner.ToDocument();
            await AddAsync(ownerM);
        }

        public async Task<IEnumerable<Owner>> GetAllOwner()
        {
            List<Owner> ownerList = new List<Owner>();
            var result = await this.GetAllAsync();
            ownerList = result.Select(owner => owner.ToDomain()).ToList();
            return ownerList;
        }
    }
}
