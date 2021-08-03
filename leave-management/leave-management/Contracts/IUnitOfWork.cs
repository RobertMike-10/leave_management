using leave_management.Data;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace leave_management.Contracts
{
    public interface IUnitOfWork: IDisposable
    {
        IGenericRepository<LeaveType> LeaveTypeRepo { get; }
        IGenericRepository<LeaveHistory> LeaveHistoryRepo { get; }
        IGenericRepository<LeaveAllocation> LeaveAllocationRepo { get; }

        Task<bool> Save();
    }
}
