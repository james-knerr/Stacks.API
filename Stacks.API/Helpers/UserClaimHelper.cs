using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Claims;
using System.Security.Principal;
using System.Threading.Tasks;

namespace Stacks.API.Helpers
{
    public static class UserClaimHelper
    {
        public static string UserId(IIdentity Identity)
        {
            var cId = (ClaimsIdentity)Identity;
            var claimId = cId.Claims.FirstOrDefault(k => k.Type.ToLower() == ClaimTypes.NameIdentifier);
            if (claimId != null)
                return claimId.Value;
            else
                return null;
        }
        public static string Email(IIdentity Identity)
        {
            var cId = (ClaimsIdentity)Identity;
            var claimEmail = cId.Claims.FirstOrDefault(k => k.Type.ToLower() == "email");
            if (claimEmail != null)
                return claimEmail.Value;
            else
                return null;
        }
    }
}
