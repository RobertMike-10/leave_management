using leave_management.Contracts;
using leave_management.Data;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace leave_management.Repository
{
    public class LeaveAllocationRepository: ILeaveAllocationRepository
    {
        private readonly ApplicationDbContext _db;

        public LeaveAllocationRepository(ApplicationDbContext db)
        {
            _db = db;
        }
        public async Task<bool> Create(LeaveAllocation entity)
        {
            await _db.LeaveAllocations.AddAsync(entity);
            return await Save();
        }

        public async Task<bool> Delete(LeaveAllocation entity)
        {
            await Task.Run(()=>_db.LeaveAllocations.Remove(entity));
            return await Save();
        }

        public async Task<ICollection<LeaveAllocation>> FindAll()
        {
            return await _db.LeaveAllocations.
                Include(x => x.LeaveType).
                Include(y => y.Employee).
                ToListAsync();
        }

        public async Task<LeaveAllocation> FindById(int id)
        {
            return await _db.LeaveAllocations.
                Include(x => x.LeaveType).
                Include(y => y.Employee).
                FirstOrDefaultAsync(z => z.Id== id);
        }

        public async Task<bool> Save()
        {
            return await _db.SaveChangesAsync() > 0;
        }

        public async Task<bool> Update(LeaveAllocation entity)
        {
            await Task.Run(()=>_db.LeaveAllocations.Update(entity));
            return await Save();
        }
        public async Task<bool> IsExists(int id)
        {
            var exists = await _db.LeaveAllocations.AnyAsync(x => x.Id == id);
            return exists;
        }

        public async Task<bool> CheckAllocation(int LeaveTypeId, string employeeId)
        {
            var Period = DateTime.Now.Year;
            var allocations = await FindAll();
            return allocations.Where(x =>
            x.EmployeeId == employeeId && x.LeaveTypeId == LeaveTypeId && x.Period == Period).Any();
        }

        public async Task<ICollection<LeaveAllocation>> GetLeaveAllocationsByEmployee(string employeeId)
        {
            var Period = DateTime.Now.Year;
            var allocations = await FindAll();
            return allocations.Where(x =>
            x.EmployeeId == employeeId && x.Period == Period).ToList();
        }

        public async Task<LeaveAllocation> GetLeaveAllocationByEmployeeAndType(string employeeId, int leaveTypeId)
        {
            var Period = DateTime.Now.Year;
            var allocations = await FindAll();
            return allocations.FirstOrDefault(x =>
            x.EmployeeId == employeeId && x.Period == Period && x.LeaveTypeId == leaveTypeId);
        }
    }
}
