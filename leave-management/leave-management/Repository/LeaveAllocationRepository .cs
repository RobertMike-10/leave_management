using leave_management.Contracts;
using leave_management.Data;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace leave_management.Repository
{
    public class LeaveAllocationRepository : ILeaveAllocationRepository
    {
        private readonly ApplicationDbContext _db;

        public LeaveAllocationRepository(ApplicationDbContext db)
        {
            _db = db;
        }
        public bool Create(LeaveAllocation entity)
        {
            _db.LeaveAllocations.Add(entity);
            return Save();
        }

        public bool Delete(LeaveAllocation entity)
        {
            _db.LeaveAllocations.Remove(entity);
            return Save();
        }

        public ICollection<LeaveAllocation> FindAll()
        {
            return _db.LeaveAllocations.
                Include(x => x.LeaveType).
                Include(y => y.Employee).
                ToList();
        }

        public LeaveAllocation FindById(int id)
        {
            return _db.LeaveAllocations.
                Include(x => x.LeaveType).
                Include(y => y.Employee).
                FirstOrDefault(z => z.Id== id);
        }

        public bool Save()
        {
            return _db.SaveChanges() > 0;
        }

        public bool Update(LeaveAllocation entity)
        {
            _db.LeaveAllocations.Update(entity);
            return Save();
        }
        public bool IsExists(int id)
        {
            var exists = _db.LeaveAllocations.Any(x => x.Id == id);
            return exists;
        }

        public bool CheckAllocation(int LeaveTypeId, string employeeId)
        {
            var Period = DateTime.Now.Year;
            return FindAll().Where(x =>
            x.EmployeeId == employeeId && x.LeaveTypeId == LeaveTypeId && x.Period == Period).Any();
        }

        public ICollection<LeaveAllocation> GetLeaveAllocationsByEmployee(string employeeId)
        {
            var Period = DateTime.Now.Year;
            return FindAll().Where(x =>
            x.EmployeeId == employeeId && x.Period == Period).ToList();
        }

        public LeaveAllocation GetLeaveAllocationByEmployeeAndType(string employeeId, int leaveTypeId)
        {
            var Period = DateTime.Now.Year;
            return FindAll().FirstOrDefault(x =>
            x.EmployeeId == employeeId && x.Period == Period && x.LeaveTypeId == leaveTypeId);
        }
    }
}
