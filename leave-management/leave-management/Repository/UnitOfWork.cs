using leave_management.Contracts;
using leave_management.Data;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace leave_management.Repository
{
    public class UnitOfWork : IUnitOfWork
    {
        private readonly ApplicationDbContext _context;
        private  IGenericRepository<LeaveType> _leaveTypeRepo;
        private  IGenericRepository<LeaveHistory> _leaveHistoryRepo;
        private  IGenericRepository<LeaveAllocation> _leaveAllocationRepo;
        public UnitOfWork(ApplicationDbContext context)
         {
            _context = context;
         }
        public IGenericRepository<LeaveType> LeaveTypeRepo {
            get => _leaveTypeRepo??= new GenericRepository<LeaveType>(_context);
             }
        public IGenericRepository<LeaveHistory> LeaveHistoryRepo {
            get => _leaveHistoryRepo ??= new GenericRepository<LeaveHistory>(_context);
        }
        public IGenericRepository<LeaveAllocation> LeaveAllocationRepo {
            get => _leaveAllocationRepo ??= new GenericRepository<LeaveAllocation>(_context);
        }

        public void Dispose()
        {
            Dispose(true);
            GC.SuppressFinalize(this);
        }

        private void Dispose(bool dispose)
        {
            if (dispose)
            {                
                _context.Dispose();
            }
        }

        public async Task<bool> Save()
        {
           return await _context.SaveChangesAsync()> 0;
        }
    }
}
