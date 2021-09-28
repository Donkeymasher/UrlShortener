﻿using System;

namespace UrlShortener.Exceptions
{
    public class InvalidUrlException : Exception
    {
        public InvalidUrlException(string message) : base(message)
        {
        }
    }
}
