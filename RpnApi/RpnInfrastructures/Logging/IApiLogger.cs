using System;
using System.Collections.Generic;

namespace RpnInfrastructures.Logging
{
    public interface IApiLogger
    {
        void Info(string message, IDictionary<string, object> parameters = null);
        void Warn(string message, IDictionary<string, object> parameters = null);
        void Error(string message, Exception exception = null, IDictionary<string, object> parameters = null);
        void Fuctional(string feature, string message = null, IDictionary<string, object> parameters = null);
        void Performance(string message, double duration, IDictionary<string, object> parameters = null);
    }
}
