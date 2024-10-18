using System;
using System.Collections.Generic;
using System.Data.SqlTypes;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Common.ServiceResults
{
    public class LoginResult : ServiceCallResult
    {
        public string Token { get; set; }
        public LoginResult(bool isSuccessfull = false, string? errorMessage = null, string? token = null) : base(isSuccessfull, errorMessage)
        {
            Token = token ?? string.Empty;
        }

        public static LoginResult Success(string token) => new(true, null, token);


        public new static LoginResult Fail(string message) => new(false, message);
    }
}
