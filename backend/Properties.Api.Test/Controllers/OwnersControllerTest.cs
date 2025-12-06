using Microsoft.AspNetCore.Mvc;
using Moq;
using Properties.API.Controllers;
using Properties.Application.Dtos;
using Properties.Application.Services;
using Properties.Domain.Entities;
using Properties.Domain.Interfaces;

[TestFixture]
public class OwnersControllerTests
{
    private Mock<IOwnerRepository> _ownerRepoMock;
    private OwnerService _ownerService;
    private OwnersController _controller;

    [SetUp]
    public void Setup()
    {
        _ownerRepoMock = new Mock<IOwnerRepository>();
        _ownerService = new OwnerService(_ownerRepoMock.Object);
        _controller = new OwnersController(_ownerService);
    }

    [Test]
    public async Task Get_ReturnsAllOwners()
    {
        // Arrange
        var owners = new List<Owner>
        {
            new Owner { Name = "Alice", Address = "Street 1", Photo = "", Birthday = DateTime.Parse("1990-01-01") },
            new Owner { Name = "Bob", Address = "Street 2", Photo = "", Birthday = DateTime.Parse("1985-05-05") }
        };
        _ownerRepoMock.Setup(r => r.GetAllOwner()).ReturnsAsync(owners);

        // Act
        var result = await _controller.Get();

        // Assert
        Assert.IsInstanceOf<OkObjectResult>(result);
        var okResult = result as OkObjectResult;
        var returnedOwners = (okResult!.Value as IEnumerable<OwnerDto>)?.ToList();
        Assert.AreEqual(2, returnedOwners.Count);
    }

    [Test]
    public async Task Post_ValidOwner_ReturnsOk()
    {
        var newOwnerDto = new OwnerDto
        {
            Name = "Charlie",
            Address = "Street 3",
            Photo = "",
            Birthday = Convert.ToDateTime("2000-01-01")
        };

        _ownerRepoMock.Setup(r => r.AddOwner(It.IsAny<Owner>())).Returns(Task.CompletedTask);

        var result = await _controller.Post(newOwnerDto);

        Assert.IsInstanceOf<OkResult>(result);
        _ownerRepoMock.Verify(r => r.AddOwner(It.IsAny<Owner>()), Times.Once);
    }
}