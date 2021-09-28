using System;

namespace UrlShortner.Exceptions
{
    public class InvalidUrlException : Exception
    {
        public InvalidUrlException(string message) : base(message)
        {
        }
    }
}
