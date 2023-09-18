using AutoMapper;
using HRLeaveManagement.Application.Contracts.Persistence;
using HRLeaveManagement.Application.Exceptions;
using HRLeaveManagement.Application.Features.LeaveType.Commands.UpdateLeaveType.Validation;
using MediatR;

namespace HRLeaveManagement.Application.Features.LeaveType.Commands.UpdateLeaveType
{
    public class UpdateLeaveTypeCommandHandler : IRequestHandler<UpdateLeaveTypeCommand, Unit>
    {
        private readonly ILeaveTypeRepository _leaveTypeRepisitory;
        private readonly IMapper _mapper;
        public UpdateLeaveTypeCommandHandler(ILeaveTypeRepository leaveTypeRepisitory, IMapper mapper)
        {
            _leaveTypeRepisitory = leaveTypeRepisitory;
            _mapper = mapper;
        }

        public async Task<Unit> Handle(UpdateLeaveTypeCommand request, CancellationToken cancellationToken)
        {
            //ValidateOptions incoming data

            var validator = await new UpdateLeaveTypeValidator(_leaveTypeRepisitory).ValidateAsync(request);
            if (!validator.IsValid) throw new BadRequestException("Invalid LeaveType To Update", validator);

            //convert to domain entity object 
            var leaveTypeToUpdate = _mapper.Map<Domain.LeaveType>(request);

            // add to database 
            await _leaveTypeRepisitory.UpdateAsync(leaveTypeToUpdate);

            //return unit value
            return Unit.Value;
        }
    }
}
