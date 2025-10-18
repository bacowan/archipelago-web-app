using System;

namespace archipelago_web_app.Models.Exceptions;


public class ConnectionRefusedException : Exception
{
    public ConnectionRefusedException()
    {
    }

    public ConnectionRefusedException(string message)
        : base(message)
    {
    }

    public ConnectionRefusedException(string message, Exception inner)
        : base(message, inner)
    {
    }
}