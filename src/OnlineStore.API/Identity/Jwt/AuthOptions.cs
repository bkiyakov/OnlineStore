using Microsoft.IdentityModel.Tokens;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace OnlineStore.API.Identity.Jwt
{
    public class AuthOptions
    {
        public const string ISSUER = "OnlineStore.API";
        public const string AUDIENCE = "OnlineStore.Front";
        const string KEY = "online_store_secret_key_1234567";
        public const int LIFETIME_DAYS = 1;
        public static SymmetricSecurityKey GetSymmetricSecurityKey()
        {
            return new SymmetricSecurityKey(Encoding.ASCII.GetBytes(KEY));
        }
    }
}
