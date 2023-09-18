using HRLeaveManagement.Application.Contracts.Persistence;
using HRLeaveManagement.Domain;
using HRLeaveManagement.Persistence.DatabaseContext;
using Microsoft.EntityFrameworkCore;

namespace HRLeaveManagement.Persistence.Repositories
{
    public class LeaveRequestRepository : GenericRepository<LeaveRequest>, ILeaveRequestRepository
    {
        public LeaveRequestRepository(HRDatabaseContext context) : base(context)
        {
        }

        public async Task<List<LeaveRequest>> GetLeaveRequestsWithDetailsAsync()
        {
            return await _context.LeaveRequests.Include(c => c.LeaveType).ToListAsync();

        }

        public async Task<List<LeaveRequest>> GetLeaveRequestsWithDetailsAsync(string userId)
        {
            return await _context.LeaveRequests
                        .Where(x => x.RequestingEmployeeId == userId)
                        .Include(c => c.LeaveType).ToListAsync();
        }

        public async Task<LeaveRequest> GetLeaveRequestWithDetailsAsync(int id)
        {
            return await _context.LeaveRequests
                        .Include(c => c.LeaveType)
                        .FirstOrDefaultAsync(c => c.Id == id);
        }

    }
}
