using System;
using System.Collections.Generic;
using System.Linq;

namespace RpnInfrastructures.Logging
{
    public class ApiLogger : IApiLogger
    {
        public void Error(string message, Exception exception = null, IDictionary<string, object> parameters = null)
        {
            Console.WriteLine($"Error: {message}. Exception: {exception?.Message}. {FormatParameters(parameters)}");
        }

        public void Fuctional(string feature, string message = null, IDictionary<string, object> parameters = null)
        {
            Console.WriteLine($"Functional: {feature}. Message: {message}. {FormatParameters(parameters)}");
        }

        public void Info(string message, IDictionary<string, object> parameters = null)
        {
            Console.WriteLine($"Info: {message}. {FormatParameters(parameters)}");
        }

        public void Performance(string message, double duration, IDictionary<string, object> parameters = null)
        {
            Console.WriteLine($"Performance: {message}. Duration: {duration}. {FormatParameters(parameters)}");
        }

        public void Warn(string message, IDictionary<string, object> parameters = null)
        {
            Console.WriteLine($"Warn: {message}. {FormatParameters(parameters)}");
        }

        private static string FormatParameters(IDictionary<string, object> parameters = null)
        {
            return parameters is null ? string.Empty : $"Parameters: {string.Join(',', parameters?.Select(kv => $"{kv.Key}:{kv.Value}"))}";
        }
    }
}
