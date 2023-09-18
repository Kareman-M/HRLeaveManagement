using HRLeaveManagement.Domain;


namespace HRLeaveManagement.Application.Contracts.Persistence
{
    public interface ILeaveAllocationRepository : IGenericRepository<LeaveAllocation>
    {
        Task<LeaveAllocation> GetLeaveAllocationWithDetailsAsync(int id);
        Task<List<LeaveAllocation>> GetLeaveAllocationsWithDetailsAsync(string userId);
        Task<List<LeaveAllocation>> GetLeaveAllocationsWithDetailsAsync();
        Task<bool> AllocationExistsAsync(string userId, int leaveTypeId, int period);
        Task AddAllocationsAsync(List<LeaveAllocation> allocations);
        Task<LeaveAllocation> GetUserAllocationsAsync(string userId, int leaveTypeId);
    }
}
