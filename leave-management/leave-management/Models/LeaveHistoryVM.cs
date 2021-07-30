using Microsoft.AspNetCore.Mvc.Rendering;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace leave_management.Models
{
    public class LeaveHistoryVM
    {
       [Key]
        public int Id { get; set; }       
        public EmployeeVM RequestingEmployee { get; set; }
        [Display(Name = "Employee")]
        public string RequestingEmployeeId { get; set; }
        [Required]
        [Display(Name = "Start Date")]
        [DataType(DataType.Date)]
        public DateTime StartDate { get; set; }
        [Required]
        [Display(Name = "End Date")]
        [DataType(DataType.Date)]
        public DateTime EndDate { get; set; }     
        public LeaveTypeVM LeaveType { get; set; }
        public int LeaveTypeId { get; set; }
        //public IEnumerable<SelectListItem> LeaveTypes { get; set; }
        [Display(Name = "Date Requested")]
        [DataType(DataType.Date)]
        public DateTime DateRequested { get; set; }
        [Display(Name = "Date Actioned")]
        [DataType(DataType.Date)]
        public DateTime? DateActioned { get; set; }
        [Display(Name = "Approval State")]
        public bool? Approved { get; set; }
       
        public EmployeeVM ApprovedBy { get; set; }
        [Display(Name = "Approver")]
        public string ApprovedById { get; set; }
        [Display(Name = "Employee Comments")]
        [MaxLength(300)]
        public string RequestComments { get; set; }
        public int DaysRequested { get; set; }
        public bool Cancelled { get; set; }
    }

    public class AdminLeaveHistoryVM
    {
        [Display(Name = "Total Number of Request")]
        public int TotalRequest { get; set; }
        [Display(Name = "Approved Request")]
        public int ApprovedRequest { get; set; }
        [Display(Name = "Pending Request")]
        public int PendingRequest { get; set; }
        [Display(Name = "Rejected Request")]
        public int RejectedRequest { get; set; }

        public List<LeaveHistoryVM> LeaveRequests { get; set; }
    }


    public class CreateLeaveRequestVM
    {
        [Key]
        public int Id { get; set; }
        [Display(Name = "Start Date")]
        [Required]
        
        public string StartDate { get; set; }
        [Display(Name = "End Date")]
        [Required]
       
        public string EndDate { get; set; }
        public IEnumerable<SelectListItem> LeaveTypes { get; set; }
        [Display(Name = "Leave Type")]
        public int LeaveTypeId { get; set; }

        [Display(Name = "Comments")]
        [MaxLength(300)]
        public string RequestComments { get; set; }
    }

    public class EmployeeLeaveRequestsVM
    {
        public List<LeaveHistoryVM> LeaveRequests { get; set; }
        public List<LeaveAllocationVM> LeaveAllocations { get; set; }
    }
}
