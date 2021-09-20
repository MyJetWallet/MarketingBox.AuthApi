using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace MarketingBox.AuthApi.Models.Auth
{
    public class AuthenticateRequest
    {
        public string Email { get; set; }

        public string Password { get; set; }
    }
}
