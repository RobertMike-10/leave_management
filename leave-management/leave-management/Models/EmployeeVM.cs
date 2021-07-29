using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace leave_management.Models
{
    public class EmployeeVM
    {
        [Key]
        public string Id { get; set; }
        [MaxLength(256)]
        [Display(Name = "User Name")]
        public string UserName { get; set; }
        [MaxLength(256)]
        public string Email { get; set; }
        [Display(Name = "Phone Number")]
        public string PhoneNumber { get; set; }
        [MaxLength(100)]
        [Display(Name = "First Name")]
        public string Firstname { get; set; }
        [MaxLength(100)]
        [Display(Name = "Last Name")]
        public string Lastname { get; set; }
        [MaxLength(100)]
        [Display(Name = "TaxID Number")]
        public string TaxId { get; set; }
        [Display(Name ="Date of Birth")]
        public DateTime DateOfBirth { get; set; }
        [Display(Name = "Date Joined")]
        public DateTime DateJoined { get; set; }

    }
}
