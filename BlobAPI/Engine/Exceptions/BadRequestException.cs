﻿using System.Net;

namespace BlobAPI.Engine.Exceptions
{
    public class BadRequestException : BaseException
    {
        public BadRequestException(string message) : base(HttpStatusCode.BadRequest, message) { }
        public BadRequestException(string message, Exception? inner) : base(HttpStatusCode.BadRequest, message, inner) { }
    }
}
