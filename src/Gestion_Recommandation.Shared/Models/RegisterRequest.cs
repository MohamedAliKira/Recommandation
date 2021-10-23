using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Gestion_Recommandation.Shared.Models
{
    public class RegisterRequest
    {
        public string Email { get; set; }
        public string Matricule { get; set; }
        public string Bureau { get; set; }
        public string FirstName { get; set; }
        public string LastName { get; set; }        
        public string Password { get; set; }
        public string ConfirmPassword { get; set; }
    }
}
