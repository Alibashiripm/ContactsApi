using Google.Apis.Auth.OAuth2;
using Google.Apis.PeopleService.v1;
using Google.Apis.PeopleService.v1.Data;
using Google.Apis.Services;
using ContactsApi.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace  ContactsApi.Services
{
    public class GooglePeopleApiHelper
    {
        private const string AppName = "";
        public static UserCredential Credential { get; set; }
        public static bool AddContact(ContactViewModel contact)
        {
            try
            {
                var service = new PeopleServiceService(new BaseClientService.Initializer
                {
                    ApplicationName = AppName,
                    HttpClientInitializer = Credential
                });
                var newContact = new Person
                {
                    Names = new List<Name> { new Name { GivenName = contact.fullName } },
                    PhoneNumbers = new List<PhoneNumber> { new PhoneNumber { Value = contact.PhoneNumber, Type = "mobile" } },
                    Organizations = new List<Organization> { new Organization { Name = contact.CompanyName, Type = "work" } }

                };
                var respons = service.People.CreateContact(newContact).Execute();
                if (respons != null && respons.ResourceName != null)
                {
                    return true;
                }
                return false;
            }
            catch (Exception)
            {

                throw;
            }
        }
    }
}