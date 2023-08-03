using System.Net;

namespace AuthAPI.Engine.Exceptions
{
    public class UnauthorizedException : BaseException
    {
        public UnauthorizedException() : this(null) { }
        public UnauthorizedException(string? message) : base(HttpStatusCode.Unauthorized, message) { }
        public UnauthorizedException(string? message, Exception? inner) : base(HttpStatusCode.Unauthorized, message, inner) { }
    }
}
