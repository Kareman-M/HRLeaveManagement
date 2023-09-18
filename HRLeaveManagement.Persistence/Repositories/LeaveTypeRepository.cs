using HRLeaveManagement.Application.Contracts.Persistence;
using HRLeaveManagement.Domain;
using HRLeaveManagement.Persistence.DatabaseContext;
using Microsoft.EntityFrameworkCore;

namespace HRLeaveManagement.Persistence.Repositories
{
    public class LeaveTypeRepository : GenericRepository<LeaveType>, ILeaveTypeRepository
    {
        public LeaveTypeRepository(HRDatabaseContext context) : base(context)
        {
        }

        public Task<bool> IsLeaveTypeExist(int id)
        {
            return _context.LeaveTypes.AnyAsync(x => x.Id == id);
        }

        public Task<bool> IsLeaveTypeUnique(string name)
        {
            return _context.LeaveTypes.AnyAsync(x => x.Name == name);
        }
    }
}
