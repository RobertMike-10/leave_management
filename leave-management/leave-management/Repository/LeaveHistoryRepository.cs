using leave_management.Contracts;
using leave_management.Data;
using leave_management.Models;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace leave_management.Repository
{
    public class LeaveHistoryRepository: ILeaveHistoryRepository
    {
        private readonly ApplicationDbContext _db;
        public LeaveHistoryRepository(ApplicationDbContext db)
        {
            _db = db;
        }
        public async Task<bool> Create(LeaveHistory entity)
        {
            await _db.LeaveHistories.AddAsync(entity);
            return await Save();
        }

        public async Task<bool> Delete(LeaveHistory entity)
        {
            await Task.Run (()=>_db.LeaveHistories.Remove(entity));
            return await Save();
        }

        public async Task<ICollection<LeaveHistory>> FindAll()
        {
            return await _db.LeaveHistories.
                Include(x => x.RequestingEmployee).
                Include(x => x.ApprovedBy).
                Include(x => x.LeaveType).
                ToListAsync();
        }

        public async Task<LeaveHistory> FindById(int id)
        {
            return await _db.LeaveHistories.
                Include(x => x.RequestingEmployee).
                Include(x => x.ApprovedBy).
                Include(x => x.LeaveType).
                FirstOrDefaultAsync(x => x.Id== id);
        }

        public async Task<bool> Save()
        {
            return await _db.SaveChangesAsync() > 0;
        }

        public async Task<bool> Update(LeaveHistory entity)            
        {
            await Task.Run (()=>_db.LeaveHistories.Update(entity));
            return await Save ();
        }

        public async Task<bool> IsExists(int id)
        {
            var exists = await _db.LeaveHistories.AnyAsync(x => x.Id == id);
            return exists;
        }

        public async Task<ICollection<LeaveHistory>> GetLeaveRequestsByEmployee(string employeeId)
        {
            var requestHistory = await FindAll();
            return requestHistory.Where(x => x.RequestingEmployeeId == employeeId).ToList();
        }
    }
}
