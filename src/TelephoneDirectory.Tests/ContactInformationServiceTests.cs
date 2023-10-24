using FluentAssertions;
using MockQueryable.Moq;
using Moq;
using TelephoneDirectory.DataAccessLayer.Entities;
using TelephoneDirectory.DataAccessLayer.Records;
using TelephoneDirectory.DataAccessLayer.Services;

namespace TelephoneDirectory.Tests;

public class ContactInformationServiceTests
{
    private readonly Mock<Context> _mockContext;
    private readonly IContactInformationService _service;

    public ContactInformationServiceTests()
    {
        _mockContext = new Mock<Context>();

        _service = new ContactInformationService(_mockContext.Object); ;

        [Fact]
        async Task Create_CallsSaveChangesAsync_WhenSuccessful()
        {
            // Arrange
            var id = Guid.NewGuid();
            var model = new CreateContactInformation(id, ContactInformationTypeEnum.Location, "test");

            var contactMock = new List<Contact> { new() { Id = id } }.AsQueryable().BuildMockDbSet();
            _mockContext.Setup(x => x.Contacts).Returns(contactMock.Object);

            var contactInformationMock = Array.Empty<ContactInformation>().AsQueryable().BuildMockDbSet();
            _mockContext.Setup(x => x.ContactInformation).Returns(contactInformationMock.Object);

            // Act
            await _service.Create(model);

            // Assert
            _mockContext.Verify(x => x.SaveChangesAsync(It.IsAny<CancellationToken>()), Times.Once);
        }

        [Fact]
        async Task Delete_SetsDeletedAt_WhenSuccessful()
        {
            // Arrange
            var id = Guid.NewGuid();
            var contactInformation = new ContactInformation { Id = id };
            var mock = new List<ContactInformation> { contactInformation }.AsQueryable().BuildMockDbSet();
            _mockContext.Setup(x => x.ContactInformation).Returns(mock.Object);

            // Act
            await _service.Delete(id);

            // Assert
            contactInformation.DeletedAt.Should().NotBeNull();
        }

        [Fact]
        async Task Delete_CallsSaveChangesAsync_WhenSuccessful()
        {
            // Arrange
            var id = Guid.NewGuid();
            var contactInformation = new ContactInformation { Id = id };

            var contactInformationList = new List<ContactInformation> { contactInformation };
            var mock = contactInformationList.AsQueryable().BuildMockDbSet();
            _mockContext.Setup(x => x.ContactInformation).Returns(mock.Object);

            // Act
            await _service.Delete(id);

            // Assert
            _mockContext.Verify(x => x.SaveChangesAsync(It.IsAny<CancellationToken>()), Times.Once);
        }
    }
}