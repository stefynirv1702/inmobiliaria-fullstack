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
    public class PropertyService
    {
        private readonly IPropertyRepository _repository;
        private readonly IPropertyImageRepository _imgRepository;
        private readonly IPropertyTraceRepository _tRepository;

        public PropertyService(IPropertyRepository repository, IPropertyImageRepository imgRepository,
            IPropertyTraceRepository tRepository)
        {
            _repository = repository;
            _tRepository = tRepository;
            _imgRepository = imgRepository;
        }

        public async Task<IEnumerable<PropertyDto>> GetPropertiesAsync(string? name, string? address, decimal? minPrice, decimal? maxPrice)
        {
            var properties = await _repository.GetPropertiesAsync(name, address, minPrice, maxPrice);

            var propertyDtoTasks = properties.Select(async p => new PropertyDto
            {
                IdOwner = p.OwnerId,
                IdProperty = p.IdProperty.ToString(),
                Name = p.Name,
                Address = p.Address,
                CodeInternal = p.CodeInternal,
                Price = p.Price,
                Year = p.Year,
            }).ToList();

            var result = await Task.WhenAll(propertyDtoTasks);

            return result;

        }

        public async Task<PropertyDetailDto> GetPropertyByIdAsync(string id)
        {
            var property = await _repository.GetPropertyById(id);
            PropertyDetailDto result = null;
            if (property != null)
            {
                var trace = await GetTrace(id);
                result = new PropertyDetailDto
                {
                    IdProperty = property.IdProperty.ToString(),
                    OwnerId = property.OwnerId,
                    Name = property.Name,
                    Address = property.Address,
                    CodeInternal = property.CodeInternal,
                    Price = property.Price,
                    Year = property.Year,
                    Image = await GetImage(property.IdProperty.ToString()),
                    Trace = trace
                };
            }

            return result;

        }

        public async Task AddPropertyAsync(PropertyCreateDto dto)
        {
            var property = new Property
            {
                IdProperty = Guid.NewGuid(),
                OwnerId = dto.IdOwner,
                Name = dto.Name,
                Address = dto.Address,
                CodeInternal = GenerateRandomLetters(4),
                Price = dto.Price,
                Year = dto.Year
            };

            string propertyId = await _repository.AddProperty(property);

            if (!string.IsNullOrEmpty(dto.Image))
            {
                var image = new PropertyImage
                {
                    IdPropertyImage = Guid.NewGuid(),
                    PropertyId = propertyId,
                    File = dto.Image,
                    Enabled = true
                };

                await _imgRepository.AddPropertyImage(image);
            }

        }

        private async Task<List<PropertyTraceDto>> GetTrace(string Id)
        {
            var result = new List<PropertyTraceDto>();
            var traces = await _tRepository.GetPropertyTraceByPropertyAsync(Id);

            if (traces != null)
            {
                result = traces.Select(t => new PropertyTraceDto
                {
                    IdTrace = t.IdPropertyTrace.ToString(),
                    Name = t.Name,
                    Value = t.Value,
                    Tax = t.Tax,
                    DateSale = t.DateSale
                }).ToList();
            }
            return result;

        }

        private async Task<string> GetImage(string Id)
        {
            var result = String.Empty;
            var Image = await _imgRepository.GetPropertyImageByPropertyAsync(Id);

            if (Image != null)
            {
                result = Image.File;
            }
            return result;

        }

        private static string GenerateRandomLetters(int length)
        {
            const string chars = "ABCDEFGHIJKLMNOPQRSTUVWXYZabcdefghijklmnopqrstuvwxyz";
            Random random = new Random();
            StringBuilder result = new StringBuilder(length);

            for (int i = 0; i < length; i++)
            {
                result.Append(chars[random.Next(chars.Length)]);
            }

            return result.ToString();
        }
    }
}
