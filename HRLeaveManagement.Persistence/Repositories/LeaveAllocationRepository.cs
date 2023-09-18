using HRLeaveManagement.Application.Contracts.Persistence;
using HRLeaveManagement.Domain;
using HRLeaveManagement.Persistence.DatabaseContext;
using Microsoft.EntityFrameworkCore;

namespace HRLeaveManagement.Persistence.Repositories
{
    public class LeaveAllocationRepository : GenericRepository<LeaveAllocation>, ILeaveAllocationRepository
    {
        public LeaveAllocationRepository(HRDatabaseContext context) : base(context)
        {
        }

        public async Task AddAllocationsAsync(List<LeaveAllocation> allocations)
        {
            await _context.LeaveAllocations.AddRangeAsync(allocations);
            await _context.SaveChangesAsync();
        }

        public async Task<bool> AllocationExistsAsync(string userId, int leaveTypeId, int period)
        {
            return await _context.LeaveAllocations.Include(q => q.LeaveType).AnyAsync(q => q.Period == period && q.LeaveTypeId == leaveTypeId && q.EmployeeId == userId);
        }

        public async Task<List<LeaveAllocation>> GetLeaveAllocationsWithDetailsAsync(string userId)
        {
            return await _context.LeaveAllocations.Where(q => q.EmployeeId == userId).Include(q => q.LeaveType).ToListAsync();
        }

        public async Task<List<LeaveAllocation>> GetLeaveAllocationsWithDetailsAsync()
        {
            return await _context.LeaveAllocations.Include(q => q.LeaveType).ToListAsync();
        }

        public async Task<LeaveAllocation> GetLeaveAllocationWithDetailsAsync(int id)
        {
            return await _context.LeaveAllocations.Include(q => q.LeaveType).FirstOrDefaultAsync(q => q.Id == id);
        }

        public async Task<LeaveAllocation> GetUserAllocationsAsync(string userId, int leaveTypeId)
        {
            return await _context.LeaveAllocations.Include(q => q.LeaveType).FirstOrDefaultAsync(q => q.EmployeeId == userId && q.LeaveTypeId == leaveTypeId);

        }
    }
}
