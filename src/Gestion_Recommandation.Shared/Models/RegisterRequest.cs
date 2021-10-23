using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
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
        [Display(Name = "Identité")]
        public string FirstName { get; set; }        
        public string Password { get; set; }
        public string ConfirmPassword { get; set; }
    }
}
