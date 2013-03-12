using System;

namespace uSnapUs.Core.Exceptions
{
    public class ApiException : Exception
    {
        public ApiException(string content):base(content)
        {
            
        }
    }
}