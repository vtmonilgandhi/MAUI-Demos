﻿using System.Text.RegularExpressions;
using MAUIUnitTestDemo;
using MAUIUnitTestDemo.Contact;
using Moq;
using Contact = MAUIUnitTestDemo.Contact;

namespace UnitTestProject;

public class ContactUnitTest
{
    [Fact]
    public void GetContacts_ShouldReturnLists()
    {
        // Arrange
        var mockContactRepository = new Mock<IContactRepository>();
        mockContactRepository.Setup(obj => obj.GetContacts()).Returns(new List<ContactModal>
          {
            new ContactModal { ContactId = 1 },
            new ContactModal { ContactId = 2 }
          });

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
        mockContactRepository.Setup(obj => obj.GetContacts()).Returns(new List<ContactModal>
          {
            new ContactModal { ContactId = 1 },
            new ContactModal { ContactId = 2 }
          }); ;

        var contactManager = new ContactManager(mockContactRepository.Object);

        // Act
        var contacts = contactManager.GetContacts();

        // Assert
        Assert.NotNull(contacts);
        Assert.Equal(3, contacts.Count);
    }

    [Theory]
    [ClassData(typeof(ContactTestDataGenrator))]
    public void CanAddTheoryClassData(List<ContactModal> contactList, int expected)
    {
        // Arrange
        var mockContactRepository = new Mock<IContactRepository>();
        mockContactRepository.Setup(obj => obj.GetContacts()).Returns(contactList);

        var contactManager = new ContactManager(mockContactRepository.Object);

        // Act
        var contacts = contactManager.GetContacts();

        // Assert
        Assert.NotNull(contacts);
        Assert.Equal(expected, contacts.Count);
    }
}
