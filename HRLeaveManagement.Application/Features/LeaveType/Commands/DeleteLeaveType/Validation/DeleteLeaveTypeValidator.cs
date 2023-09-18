using FluentValidation;
using HRLeaveManagement.Application.Contracts.Persistence;

namespace HRLeaveManagement.Application.Features.LeaveType.Commands.DeleteLeaveType.Validation
{
    public class DeleteLeaveTypeValidator : AbstractValidator<DeleteLeaveTypeCommand>
    {
        private readonly ILeaveTypeRepository _leaveTypeRepository;
        public DeleteLeaveTypeValidator(ILeaveTypeRepository leaveTypeRepository)
        {
            AddRules();
            _leaveTypeRepository = leaveTypeRepository;
        }
        private void AddRules()
        {
            RuleFor(p => p.Id)
              .NotNull().NotEmpty().WithMessage("Id Cannot be Null or Empty")
              .NotEqual(0).WithMessage("Not valid Id number")
              .MustAsync(IsLeaveTypeExist);
        }

        private Task<bool> IsLeaveTypeExist(int id, CancellationToken token)
        {
            return _leaveTypeRepository.IsLeaveTypeExist(id);
        }

    }
}
