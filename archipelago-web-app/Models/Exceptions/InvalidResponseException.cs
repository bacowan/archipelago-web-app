using System;

namespace archipelago_web_app.Models.Exceptions;

public class InvalidResponseException : Exception
{
    public InvalidResponseException()
    {
    }

    public InvalidResponseException(string message)
        : base(message)
    {
    }

    public InvalidResponseException(string message, Exception inner)
        : base(message, inner)
    {
    }
}