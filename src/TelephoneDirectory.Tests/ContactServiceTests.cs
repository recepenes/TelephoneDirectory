using FluentAssertions;
using MockQueryable.Moq;
using Moq;
using TelephoneDirectory.DataAccessLayer.Entities;
using TelephoneDirectory.DataAccessLayer.Records;
using TelephoneDirectory.DataAccessLayer.Repository;
using TelephoneDirectory.DataAccessLayer.Services;

namespace TelephoneDirectory.Tests;

public class ContactServiceTests
{
    private readonly Mock<Context> _mockContext;
    private readonly IContactService _service;

    public ContactServiceTests()
    {
        _mockContext = new Mock<Context>();

        _service = new ContactService(new ContactRepository(_mockContext.Object)); ;
    }

    [Fact]
    public async Task GetAll_ReturnsEmptyArray_WhenNoContactsExist()
    {
        // Arrange
        var mock = Array.Empty<Contact>().AsQueryable().BuildMockDbSet();
        _mockContext.Setup(x => x.Contacts).Returns(mock.Object);

        // Act
        var result = await _service.GetAll();

        // Assert
        result.Should().BeEmpty();
    }

    [Fact]
    public async Task GetAll_ReturnsContacts_WhenContactsExist()
    {
        // Arrange
        var contact1 = new Contact { Id = Guid.NewGuid(), Name = "Ali Veli", CreatedAt = DateTime.UtcNow };
        var contact2 = new Contact { Id = Guid.NewGuid(), Name = "Ahmet", CreatedAt = DateTime.UtcNow };

        var mock = new List<Contact> { contact1, contact2 }.AsQueryable().BuildMockDbSet();
        _mockContext.Setup(x => x.Contacts).Returns(mock.Object);

        // Act
        var result = await _service.GetAll();

        // Assert
        result.Should().HaveCount(2);
        result.Should().Contain(x => x.Name == "Ali Veli");
    }

    [Fact]
    public async Task GetReportData_ReturnsEmptyArray_WhenNoLocationInformationExists()
    {
        // Arrange
        var contact1 = new Contact { Id = Guid.NewGuid(), Name = "Ali Veli" };
        var contact2 = new Contact { Id = Guid.NewGuid(), Name = "Ahmet" };

        var contactInformation1 = new ContactInformation
        {
            Contact = contact1,
            ContactInformationType = ContactInformationTypeEnum.PhoneNumber,
            Content = "123-456-7890"
        };
        var contactInformation2 = new ContactInformation
        {
            Contact = contact2,
            ContactInformationType = ContactInformationTypeEnum.PhoneNumber,
            Content = "123-456-7890"
        };

        var mock = new List<ContactInformation>
        {
            contactInformation1,
            contactInformation2
        }.AsQueryable().BuildMockDbSet();
        _mockContext.Setup(x => x.ContactInformation).Returns(mock.Object);

        // Act
        var result = await _service.GetReportData();

        // Assert
        result.Should().BeEmpty();
    }


    [Fact]
    public async Task GetReportData_ReturnsCorrectData_WhenLocationInformationExists()
    {
        // Arrange
        var contact1 = new Contact { Id = Guid.NewGuid(), Name = "Ali Veli" };
        var contact2 = new Contact { Id = Guid.NewGuid(), Name = "Ahmet" };
        var contact3 = new Contact { Id = Guid.NewGuid(), Name = "Mehmet" };

        var contactInformation1 = new ContactInformation
        {
            Id = Guid.NewGuid(),
            ContactId = contact1.Id,
            Contact = contact1,
            ContactInformationType = ContactInformationTypeEnum.Location,
            Content = "İstanbul"
        };
        var contactInformation2 = new ContactInformation
        {
            Id = Guid.NewGuid(),
            ContactId = contact1.Id,
            Contact = contact1,
            ContactInformationType = ContactInformationTypeEnum.PhoneNumber,
            Content = "123-456-7890"
        };
        var contactInformation3 = new ContactInformation
        {
            Id = Guid.NewGuid(),
            ContactId = contact2.Id,
            Contact = contact2,
            ContactInformationType = ContactInformationTypeEnum.Location,
            Content = "İstanbul"
        };
        var contactInformation4 = new ContactInformation
        {
            Id = Guid.NewGuid(),
            ContactId = contact3.Id,
            Contact = contact3,
            ContactInformationType = ContactInformationTypeEnum.Location,
            Content = "Eskişehir"
        };
        var contactInformation5 = new ContactInformation
        {
            Id = Guid.NewGuid(),
            ContactId = contact3.Id,
            Contact = contact3,
            ContactInformationType = ContactInformationTypeEnum.PhoneNumber,
            Content = "123-456-7890"
        };
        var contactInformation6 = new ContactInformation
        {
            Id = Guid.NewGuid(),
            ContactId = contact3.Id,
            Contact = contact3,
            ContactInformationType = ContactInformationTypeEnum.PhoneNumber,
            Content = "123-456-7890"
        };

        var list = new List<ContactInformation>
        {
            contactInformation1,
            contactInformation2,
            contactInformation3,
            contactInformation4,
            contactInformation5,
            contactInformation6
        };

        var mock = list.AsQueryable().BuildMockDbSet();
        _mockContext.Setup(x => x.ContactInformation).Returns(mock.Object);

        // Act
        var result = await _service.GetReportData();

        // Assert
        result.Should().Contain(x =>
            x.Location == "İstanbul" && x.ContactCount == 2 && x.PhoneNumberCount == 1);
        result.Should().Contain(x =>
            x.Location == "Eskişehir" && x.ContactCount == 1 && x.PhoneNumberCount == 2);
    }

    [Fact]
    public async Task Create_CallsSaveChangesAsync_WhenSuccessful()
    {
        // Arrange
        var model = new CreateContact("Ali", "Veli", "Ahmet");

        var mock = Array.Empty<Contact>().AsQueryable().BuildMockDbSet();
        _mockContext.Setup(x => x.Contacts).Returns(mock.Object);

        // Act
        await _service.Create(model);

        // Assert
        _mockContext.Verify(x => x.SaveChangesAsync(It.IsAny<CancellationToken>()), Times.Once);
    }
}