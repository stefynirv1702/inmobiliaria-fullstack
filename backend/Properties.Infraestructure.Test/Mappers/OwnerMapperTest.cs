using NUnit.Framework;
using Properties.Domain.Entities;
using Properties.Infraestructure.Models;
using Properties.Infraestructure.Mappers;
using System;

namespace Properties.Tests.Mappers
{
    public class OwnerMapperTests
    {
        [Test]
        public void ToDocument_MapsOwnerToOwnerModel()
        {
            // Arrange
            var owner = new Owner
            {
                IdOwner = Guid.NewGuid(),
                Name = "Alice",
                Address = "123 Street",
                Photo = "photo.jpg",
                Birthday = new DateTime(1990, 1, 1)
            };

            // Act
            var model = owner.ToDocument();

            // Assert
            Assert.AreEqual(owner.IdOwner, model.IdOwner);
            Assert.AreEqual(owner.Name, model.Name);
            Assert.AreEqual(owner.Address, model.Address);
            Assert.AreEqual(owner.Photo, model.Photo);
            Assert.AreEqual(owner.Birthday, model.Birthday);
        }

        [Test]
        public void ToDomain_MapsOwnerModelToOwner()
        {
            // Arrange
            var model = new OwnerModel
            {
                IdOwner = Guid.NewGuid(),
                Name = "Bob",
                Address = "456 Avenue",
                Photo = "photo2.jpg",
                Birthday = new DateTime(1985, 5, 5)
            };

            // Act
            var owner = model.ToDomain();

            // Assert
            Assert.AreEqual(model.IdOwner, owner.IdOwner);
            Assert.AreEqual(model.Name, owner.Name);
            Assert.AreEqual(model.Address, owner.Address);
            Assert.AreEqual(model.Photo, owner.Photo);
            Assert.AreEqual(model.Birthday, owner.Birthday);
        }
    }
}