using AutoMapper;
using HRLeaveManagement.Application.Contracts.Persistence;
using HRLeaveManagement.Application.Exceptions;
using HRLeaveManagement.Application.Features.LeaveType.Commands.CreateLeaveType.Validation;
using MediatR;

namespace HRLeaveManagement.Application.Features.LeaveType.Commands.CreateLeaveType
{
    public class CreateLeaveTypeCommandHandler : IRequestHandler<CreateLeaveTypeCommand, int>
    {
        private readonly IMapper _mapper;
        private readonly ILeaveTypeRepository _leaveTypeRepository;

        public CreateLeaveTypeCommandHandler(ILeaveTypeRepository leaveTypeRepository, IMapper mapper)
        {
            _leaveTypeRepository = leaveTypeRepository;
            _mapper = mapper;
        }

        public async Task<int> Handle(CreateLeaveTypeCommand request, CancellationToken cancellationToken)
        {
            //validate incoming data
            var validator = new CreateLeaveTypeValidator(_leaveTypeRepository);
            var validationResult = await validator.ValidateAsync(request);

            if (!validationResult.IsValid) // can say also   ==> if (!validationResult.Errors.Any())
                throw new BadRequestException("Invalid LeaveType To Create", validationResult);

            // convert to domain entoty object
            var leaveTypeToCreate = _mapper.Map<Domain.LeaveType>(request);

            //add to database
            await _leaveTypeRepository.CreateAsync(leaveTypeToCreate);

            // return record id
            return leaveTypeToCreate.Id;
        }
    }
}
