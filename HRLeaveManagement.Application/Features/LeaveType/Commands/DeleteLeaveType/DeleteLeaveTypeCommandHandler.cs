using HRLeaveManagement.Application.Contracts.Persistence;
using HRLeaveManagement.Application.Exceptions;
using HRLeaveManagement.Application.Features.LeaveType.Commands.DeleteLeaveType.Validation;
using MediatR;

namespace HRLeaveManagement.Application.Features.LeaveType.Commands.DeleteLeaveType
{
    public class DeleteLeaveTypeCommandHandler : IRequestHandler<DeleteLeaveTypeCommand, Unit>
    {
        private readonly ILeaveTypeRepository _leaveTypeRepository;
        public DeleteLeaveTypeCommandHandler(ILeaveTypeRepository leaveTypeRepository)
        {
            _leaveTypeRepository = leaveTypeRepository;
        }

        public async Task<Unit> Handle(DeleteLeaveTypeCommand request, CancellationToken cancellationToken)
        {
            // validate delete Leave type
            var validationResult = await new DeleteLeaveTypeValidator(_leaveTypeRepository).ValidateAsync(request);
            if (!validationResult.IsValid) throw new BadRequestException("Not Valid Delete LeaveType", validationResult);

            // retrieve domain entity object
            var leaveType = await _leaveTypeRepository.GetByIdAsync(request.Id);

            // remove from database
            await _leaveTypeRepository.DeleteAsync(leaveType);

            // return record id 
            return Unit.Value;
        }
    }
}
