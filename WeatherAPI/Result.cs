﻿using System.Net;

namespace WeatherAPI
{
    public class Result
    {
        public Result(bool success, string? message, HttpStatusCode statusCode)
        {
            Success = success;
            Message = message;
            StatusCode = statusCode;
        }

        public Result(object? data, bool success, string? message, HttpStatusCode statusCode) : this(success, message, statusCode)
        {
            Data = data;
        }

        public object? Data { get; set; }
        public HttpStatusCode StatusCode { get; set; }
        public bool Success { get; set; }
        public string? Message { get; set; }

        public static Result Failed(string message, HttpStatusCode statusCode)
        {
            if ((int)statusCode < 400) throw new InvalidOperationException();

            return new Result(false, message, statusCode);
        }

        public static Result Succeed(HttpStatusCode statusCode = HttpStatusCode.OK)
        {
            if ((int)statusCode < 200 || (int)statusCode >= 400) throw new InvalidOperationException();

            return new Result(true, null, statusCode);
        }

        public static Result Succeed(object? value, HttpStatusCode statusCode = HttpStatusCode.OK)
        {
            if ((int)statusCode < 200 || (int)statusCode >= 400) throw new InvalidOperationException();

            return new Result(value, true, null, statusCode);
        }
    }
}