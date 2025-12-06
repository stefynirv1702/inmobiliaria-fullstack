using Properties.Application.Dtos;
using Properties.Domain.Entities;
using Properties.Domain.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Properties.Application.Services
{
    public class OwnerService
    {
        private readonly IOwnerRepository _repository;

        public OwnerService(IOwnerRepository repository)
        {
            _repository = repository;
        }

        public async Task<IEnumerable<OwnerDto>> GetOwnersAsync()
        {
            var owners = await _repository.GetAllOwner();

            return owners.Select(o => new OwnerDto
            {
                IdOwner = o.IdOwner,
                Name = o.Name,
                Address = o.Address,
                Photo = o.Photo,
                Birthday = o.Birthday
            });
        }

        public async Task AddOwnerAsync(OwnerDto dto)
        {
            var owner = new Owner
            {
                IdOwner = Guid.NewGuid(),
                Name = dto.Name,
                Address = dto.Address,
                Photo = dto.Photo,
                Birthday = dto.Birthday
            };

            await _repository.AddOwner(owner);
        }
    }
}
