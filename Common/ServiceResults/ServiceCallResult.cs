using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Text;
using System.Threading.Tasks;

namespace Common.ServiceResults
{
    public class ServiceCallResult
    {
        public string ErrorMessage { get; set; } = string.Empty;
        public bool IsSuccessfull { get; set; } = false;

        public ServiceCallResult(bool isSuccessfull = false, string? errorMessage = null)
        {
            ErrorMessage = errorMessage ?? string.Empty;
            IsSuccessfull = isSuccessfull;
        }

        public static ServiceCallResult Success() => new(true);


        public static ServiceCallResult Fail(string message) => new(false, message);
    }
}
