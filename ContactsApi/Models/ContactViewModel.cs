using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
    using System.ComponentModel.DataAnnotations;

namespace  ContactsApi.Models
{

    public class ContactViewModel
    {
        [Required]
        public string fullName { get; set; }

        [Required]
        [Phone]
        public string PhoneNumber { get; set; }

        [Required]
        public string CompanyName { get; set; }
    }

}