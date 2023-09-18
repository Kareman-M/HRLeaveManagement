using FluentValidation;
using HRLeaveManagement.Application.Contracts.Persistence;

namespace HRLeaveManagement.Application.Features.LeaveType.Commands.CreateLeaveType.Validation
{
    public class CreateLeaveTypeValidator : AbstractValidator<CreateLeaveTypeCommand>
    {
        private readonly ILeaveTypeRepository _leaveTypeRepository;
        public CreateLeaveTypeValidator(ILeaveTypeRepository leaveTypeRepository)
        {
            _leaveTypeRepository = leaveTypeRepository;
            AddRules();
        }

        private void AddRules()
        {

            RuleFor(p => p.Name)
                .NotNull()
                .NotEmpty().WithMessage("{PropertyName} is Required")
                .MaximumLength(70).WithMessage("{PropertyName} must be fewer than 70 characters");


            RuleFor(p => p.DefaultDays)
                .LessThan(100).WithMessage("{PropertyName} cannot exceed 100")
                .GreaterThan(1).WithMessage("{PropertyName} cannot be less than 1");

            RuleFor(q => q)
                .MustAsync(IsLeaveTypeUnique)
                .WithMessage("LeaveType Must be unique");
        }

        private Task<bool> IsLeaveTypeUnique(CreateLeaveTypeCommand command, CancellationToken token)
        {
            return _leaveTypeRepository.IsLeaveTypeUnique(command.Name);
        }
    }
}
