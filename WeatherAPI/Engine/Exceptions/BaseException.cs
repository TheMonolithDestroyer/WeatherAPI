using System.Net;

namespace WeatherAPI.Engine.Exceptions
{
    public class BaseException : Exception
    {
        public HttpStatusCode StatusCode;
        public BaseException(HttpStatusCode statusCode)
        {
            StatusCode = statusCode;
        }

        public BaseException(HttpStatusCode statusCode, string message) : this(statusCode, message, null)
        {
        }

        public BaseException(HttpStatusCode statusCode, string message, Exception? inner) : base(message, inner)
        {
            StatusCode = statusCode;
        }
    }
}
