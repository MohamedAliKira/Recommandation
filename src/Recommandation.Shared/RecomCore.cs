using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Recommandation.Shared
{
    public class RecomCore
    {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int ID { get; set; }

        [Required(ErrorMessage = "Champs obligatoire !")]
        public string? NumReference { get; set; }

        [Required (ErrorMessage = "Champs obligatoire !")]
        public DateTime DateReference { get; set; }

        [Required(ErrorMessage = "Champs obligatoire !")]
        public string? SourceRecom { get; set; }

        [Required(ErrorMessage = "Champs obligatoire !")]
        public string? CandidatRecom { get; set; }

        [Required(ErrorMessage = "Champs obligatoire !")]
        public string? GradeRecom { get; set; }

        public string? InstructionDRH { get; set; }
    }
}
