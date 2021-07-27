using Microsoft.AspNetCore.Identity;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace leave_management.Data
{
    public class Employee: IdentityUser
    {
        [MaxLength(100)]
        public string Firstname { get; set; }
        [MaxLength(100)]
        public string Lastname { get; set; }
        [MaxLength(100)]
        public string TaxId { get; set; }
        public DateTime DateOfBirth { get; set; }
        public DateTime DateJoined { get; set; }
    }
}
