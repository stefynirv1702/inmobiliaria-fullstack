using Properties.Application.Dtos;
using Properties.Domain.Entities;
using Properties.Domain.Interfaces;

namespace Properties.Application.Services
{
    public class TraceService
    {
        private readonly IPropertyTraceRepository _repository;

        public TraceService(IPropertyTraceRepository repository)
        {
            _repository = repository;
        }

        public async Task AddPropertyTraceAsync(PropertyTraceCreateDto dto)
        {
            var trace = new PropertyTrace
            {
                IdPropertyTrace = Guid.NewGuid(),
                PropertyId = dto.PropertyId,
                Name = dto.Name,
                Value = dto.Value,
                Tax = dto.Tax,
                DateSale = DateTime.Now,
            };

            await _repository.AddTraceProperty(trace);

        }

    }
}
