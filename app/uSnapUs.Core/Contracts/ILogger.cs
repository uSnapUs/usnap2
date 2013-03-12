using System;

namespace uSnapUs.Core.Contracts
{
    public interface ILogger
    {
        void Trace(string message);
        void Exception(Exception e);
    }
}