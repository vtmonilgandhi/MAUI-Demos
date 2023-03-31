using System;
namespace MAUIUnitTestDemo.Contact
{
    public interface IContactRepository
    {
        ContactModal GetContact(int contactId);
        List<ContactModal> GetContacts();
        List<ContactModal> GetContacts(string firstName, string lastName);
        ContactModal SaveContact(ContactModal contact);
        bool DeleteContact(int contactId);
        bool DeleteContact(ContactModal contact);
        /// Other methods removed for brevity
    }
}

