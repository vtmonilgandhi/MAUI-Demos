using System.Text.RegularExpressions;
using MAUIUnitTestDemo;
using Moq;
using Contact = MAUIUnitTestDemo.Contact;

namespace UnitTestProject;

public class UnitTest1
{
    [Fact]
    public void GetContacts_ShouldReturnLists()
    {
        // Arrange
        var mockContactRepository = new Mock<IContactRepository>();
        mockContactRepository.Setup(obj => obj.GetContacts()).Returns(new List<Contact>
          {
            new Contact { ContactId = 1 },
            new Contact { ContactId = 2 }
          }); ;

        var contactManager = new ContactManager(mockContactRepository.Object);

        // Act
        var contacts = contactManager.GetContacts();

        // Assert
        Assert.NotNull(contacts);
        Assert.Equal(2, contacts.Count);
    }

    [Fact]
    public void GetContacts_ShouldReturnLists_Failed()
    {
        // Arrange
        var mockContactRepository = new Mock<IContactRepository>();
        mockContactRepository.Setup(obj => obj.GetContacts()).Returns(new List<Contact>
          {
            new Contact { ContactId = 1 },
            new Contact { ContactId = 2 }
          }); ;

        var contactManager = new ContactManager(mockContactRepository.Object);

        // Act
        var contacts = contactManager.GetContacts();

        // Assert
        Assert.NotNull(contacts);
        Assert.Equal(3, contacts.Count);
    }
}
