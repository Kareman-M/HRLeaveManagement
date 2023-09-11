using FluentValidation.Results;

namespace HRLeaveManagement.Application.Exceptions
{
    public class BadRequestException : Exception
    {
        public List<string> ValidationErrors { get; set; }
        public BadRequestException(string msg) : base(msg)
        {

        }

        public BadRequestException(string msg, ValidationResult validationResult) : base(msg)
        {
            ValidationErrors = new();
            foreach (var error in validationResult.Errors)
            {
                ValidationErrors.Add(error.ErrorMessage);
            }
        }
    }
}
