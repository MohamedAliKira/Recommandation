using System.Collections.Generic;
using System.Security.Claims;

namespace Gestion_Recommandation.API.Models
{
    public class ClaimsStore
    {
        public static List<Claim> AllClaims = new List<Claim>()
        {
            new Claim("Create Role","Create Role"),
            new Claim("Edit Role","Edit Role"),
            new Claim("Delete Role","Delete Role"),
            new Claim("Create User","Create User"),
            new Claim("Edit User","Edit User"),
            new Claim("Delete User","Delete User")
        };
    }
}
