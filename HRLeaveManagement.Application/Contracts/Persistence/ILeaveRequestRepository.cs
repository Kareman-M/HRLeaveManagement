using HRLeaveManagement.Domain;

namespace HRLeaveManagement.Application.Contracts.Persistence
{
    public interface ILeaveRequestRepository : IGenericRepository<LeaveRequest>
    {
        Task<List<LeaveRequest>> GetLeaveRequestsWithDetailsAsync();
        Task<List<LeaveRequest>> GetLeaveRequestsWithDetailsAsync(string userId);
        Task<LeaveRequest> GetLeaveRequestWithDetailsAsync(int id);
    }
}
