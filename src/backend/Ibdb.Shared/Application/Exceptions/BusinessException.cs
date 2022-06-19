
namespace Ibdb.Shared.Application.Exceptions
{
    public class BusinessException : ApplicationException
    {
        public BusinessException() : base()
        {
        }

        public BusinessException(string? message) : base(message)
        {
        }

        public BusinessException(string? message, Exception? innerException) : base(message, innerException)
        {
        }

        public BusinessException(string? message, string? code) : base(message)
        {
            Code = code;
        }

        public BusinessException(string? message, string? code, Exception? innerException) : base(message, innerException)
        {
            Code = code;
        }

        public string? Code { get; set; }
    }
}
