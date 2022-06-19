
namespace Ibdb.Shared.Application.Exceptions
{
    public class ValidationException : Exception
    {
        public ValidationException() : base()
        {
        }

        public ValidationException(string? message) : base(message)
        {
        }

        public ValidationException(string? message, Exception? innerException) : base(message, innerException)
        {
        }

        public ValidationException(string? message, string? code) : base(message)
        {
            Code = code;
        }

        public ValidationException(string? message, string? code, Exception? innerException) : base(message, innerException)
        {
            Code = code;
        }

        public string? Code { get; set; }
    }
}
