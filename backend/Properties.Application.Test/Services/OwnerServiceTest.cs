using Moq;
using NUnit.Framework;
using Properties.Application.Dtos;
using Properties.Application.Services;
using Properties.Domain.Entities;
using Properties.Domain.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Properties.Tests.Services
{
    public class OwnerServiceTests
    {
        private Mock<IOwnerRepository> _repoMock = null!;
        private OwnerService _service = null!;

        [SetUp]
        public void Setup()
        {
            _repoMock = new Mock<IOwnerRepository>();
            _service = new OwnerService(_repoMock.Object);
        }

        [Test]
        public async Task GetOwnersAsync_ReturnsMappedOwnerDtos()
        {
            // Arrange
            var owners = new List<Owner>
            {
                new Owner
                {
                    IdOwner = Guid.NewGuid(),
                    Name = "Alice",
                    Address = "Street 1",
                    Photo = "photo1.jpg",
                    Birthday = new DateTime(1990,1,1)
                },
                new Owner
                {
                    IdOwner = Guid.NewGuid(),
                    Name = "Bob",
                    Address = "Street 2",
                    Photo = "photo2.jpg",
                    Birthday = new DateTime(1985,5,5)
                }
            };

            _repoMock.Setup(r => r.GetAllOwner()).ReturnsAsync(owners);

            // Act
            var result = await _service.GetOwnersAsync();

            // Assert
            Assert.AreEqual(2, result.Count());
            Assert.AreEqual("Alice", result.First().Name);
            Assert.AreEqual("Bob", result.Last().Name);

            // Verify repository method was called exactly once
            _repoMock.Verify(r => r.GetAllOwner(), Times.Once);
        }

        [Test]
        public async Task AddOwnerAsync_CallsRepositoryAddOwner()
        {
            // Arrange
            var dto = new OwnerDto
            {
                Name = "Charlie",
                Address = "Street 3",
                Photo = "photo3.jpg",
                Birthday = new DateTime(1995, 7, 7)
            };

            // Act
            await _service.AddOwnerAsync(dto);

            // Assert
            _repoMock.Verify(r => r.AddOwner(It.Is<Owner>(o =>
                o.Name == dto.Name &&
                o.Address == dto.Address &&
                o.Photo == dto.Photo &&
                o.Birthday == dto.Birthday
            )), Times.Once);
        }
    }
}