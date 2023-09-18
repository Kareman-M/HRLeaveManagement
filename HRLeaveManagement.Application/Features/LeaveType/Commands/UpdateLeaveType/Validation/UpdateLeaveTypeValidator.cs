using FluentValidation;
using HRLeaveManagement.Application.Contracts.Persistence;

namespace HRLeaveManagement.Application.Features.LeaveType.Commands.UpdateLeaveType.Validation
{
    public class UpdateLeaveTypeValidator : AbstractValidator<UpdateLeaveTypeCommand>
    {
        private readonly ILeaveTypeRepository _leaveTypeRepository;
        public UpdateLeaveTypeValidator(ILeaveTypeRepository leaveTypeRepository)
        {
            _leaveTypeRepository = leaveTypeRepository;
            AddRules();



        }

        private void AddRules()
        {
            RuleFor(p => p.Id)
                .NotEqual(0).WithMessage("Not Valid Id Number");

            RuleFor(p => p.Name)
                .NotNull()
                .NotEmpty().WithMessage("{PropertyName} is Required")
                .MaximumLength(70).WithMessage("{PropertyName} must be fewer than 70 characters");

            RuleFor(p => p.DefaultDays)
                .LessThan(100).WithMessage("{PropertyName} cannot exceed 100")
                .GreaterThan(1).WithMessage("{PropertyName} cannot be less than 1");

            RuleFor(p => p)
                .MustAsync(IsLeaveTypeExist).
                WithMessage("Cannt find leave type with this specification");
        }

        private Task<bool> IsLeaveTypeExist(UpdateLeaveTypeCommand command, CancellationToken token)
        {
            return _leaveTypeRepository.IsLeaveTypeExist(command.Id);
        }
    }
}
