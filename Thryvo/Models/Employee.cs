using Microsoft.AspNetCore.Identity;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace Thryvo.Models
{
    public class Employee : IdentityUser
    {
        public string FirstName { get; set; }

        public string LastName { get; set; }

        public string TaxId { get; set; }

        public DateTime DateOfBirth { get; set; }

        public DateTime DateJoined { get; set; }
    }
}
