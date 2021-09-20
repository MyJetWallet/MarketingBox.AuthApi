using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MarketingBox.AuthApi.Domain.Models.Errors
{
    public class LoginError
    {
        public LoginErrorType Type { get; set; }
    }

    public enum LoginErrorType
    {
        NoUser,
        WrongPassword
    }
}
