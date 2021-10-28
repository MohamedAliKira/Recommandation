using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Gestion_Recommandation.Shared.Models
{
    public class Recommandations
    {
        [Key]
        public int Id { get; set; }

        [Required]
        public string NumeroReference { get; set; }

        [Required]
        public DateTime DateReference { get; set; } = DateTime.Now;

        [Required]
        public string DeLaPart { get; set; }

        [Required]
        public string Source { get; set; }

        [Required]
        public string IdentityRecommandation { get; set; }

        [Required]
        public string Type { get; set; }

        [Required]
        public string InstructionDRH { get; set; }
        public DateTime DateCreation { get; set; } = DateTime.Now;
        public string Bureau { get; set; }
        public string ID_User { get; set; }

    }
}
