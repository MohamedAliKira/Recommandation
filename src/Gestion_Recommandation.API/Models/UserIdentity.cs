using Microsoft.AspNetCore.Identity;

namespace Gestion_Recommandation.API.Models
{
    public class UserIdentity : IdentityUser
    {
        public string Matricule { get; set; }
        public string Bureau { get; set; }
        public string Identite { get; set; }
        public string Application { get; set; }
    }
}
