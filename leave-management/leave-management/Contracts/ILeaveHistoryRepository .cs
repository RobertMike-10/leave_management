using leave_management.Data;
using leave_management.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace leave_management.Contracts
{
    public interface ILeaveHistoryRepository : IRepositoryBase<LeaveHistory>
    {

        Task<ICollection<LeaveHistory>> GetLeaveRequestsByEmployee(string employeeId);
    }
}
