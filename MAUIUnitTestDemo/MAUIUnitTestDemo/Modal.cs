using System;
using System.Diagnostics.CodeAnalysis;
using static Microsoft.Maui.ApplicationModel.Permissions;

namespace MAUIUnitTestDemo
{
    public interface IContactManager
    {
        Contact GetContact(int contactId);
        List<Contact> GetContacts();
        List<Contact> GetContacts(string firstName, string lastName);
        Contact SaveContact(Contact contact);
        bool DeleteContact(int contactId);
        bool DeleteContact(Contact contact);
        /// Other methods removed for brevity
    }

    public interface IContactRepository
    {
        Contact GetContact(int contactId);
        List<Contact> GetContacts();
        List<Contact> GetContacts(string firstName, string lastName);
        Contact SaveContact(Contact contact);
        bool DeleteContact(int contactId);
        bool DeleteContact(Contact contact);
        /// Other methods removed for brevity
    }

    [ExcludeFromCodeCoverage]
    public class Contact
    {
        public int ContactId { get; set; }
        public string FirstName { get; set; }
        public string MiddleName { get; set; }
        public string LastName { get; set; }
        public string EmailAddress { get; set; }
        public DateTime Birthday { get; set; }
        public DateTime? Anniversary { get; set; }
        public string ImageUrl { get; set; }
    }

    public class ContactManager : IContactManager
    {
        private readonly IContactRepository _contactRepository;

        public ContactManager(IContactRepository contactRepository)
        {
            _contactRepository = contactRepository;
        }
        public Contact GetContact(int contactId)
        {
            return _contactRepository.GetContact(contactId);
        }

        public List<Contact> GetContacts()
        {
            return _contactRepository.GetContacts();
        }

        public List<Contact> GetContacts(string firstName, string lastName)
        {
            ValidationForGetContacts(firstName, lastName);

            return _contactRepository.GetContacts(firstName, lastName);
        }

        private static void ValidationForGetContacts(string firstName, string lastName)
        {
            if (string.IsNullOrEmpty(firstName))
            {
                throw new ArgumentNullException(nameof(firstName), "FirstName is a required field");
            }

            if (string.IsNullOrEmpty(lastName))
            {
                throw new ArgumentNullException(nameof(lastName), "LastName is a required field");
            }
        }

        public Contact SaveContact(Contact contact)
        {
            // Data Validation
            ValidationForSaveContact(contact);

            return _contactRepository.SaveContact(contact);
        }

        public bool DeleteContact(int contactId)
        {
            return _contactRepository.DeleteContact(contactId);
        }

        public bool DeleteContact(Contact contact)
        {
            return contact != null && _contactRepository.DeleteContact(contact);
        }

        private static void ValidationForSaveContact(Contact contact)
        {
            // Null Checks
            if (contact == null)
            {
                throw new ArgumentNullException(nameof(contact), "Contact is a required field");
            }

            if (string.IsNullOrEmpty(contact.FirstName))
            {
                throw new ArgumentNullException(nameof(contact.FirstName), "FirstName is a required field");
            }

            if (string.IsNullOrEmpty(contact.LastName))
            {
                throw new ArgumentNullException(nameof(contact.LastName), "LastName is a required field");
            }

            if (string.IsNullOrEmpty(contact.EmailAddress))
            {
                throw new ArgumentNullException(nameof(contact.EmailAddress), "EmailAddress is a required field");
            }

            // Business Rule Validation
            if (contact.Birthday > DateTime.Now)
            {
                throw new ArgumentOutOfRangeException(nameof(contact.Birthday), contact.Birthday,
                    "The birthday can not be in the future");
            }

            if (contact.Anniversary.HasValue && contact.Anniversary > DateTime.Now)
            {
                throw new ArgumentOutOfRangeException(nameof(contact.Anniversary), contact.Anniversary,
                    "The anniversary can not be in the future");
            }

            if (contact.Anniversary.HasValue && contact.Anniversary < contact.Birthday)
            {
                throw new ArgumentOutOfRangeException(nameof(contact.Anniversary), contact.Anniversary,
                    "The anniversary can not be earlier than the birthday.");
            }
        }
    }
}

