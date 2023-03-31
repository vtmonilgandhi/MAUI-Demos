using System;
using System.Diagnostics.CodeAnalysis;
using static Microsoft.Maui.ApplicationModel.Permissions;

namespace MAUIUnitTestDemo
{
    [ExcludeFromCodeCoverage]
    public class ContactModal
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

    
}

