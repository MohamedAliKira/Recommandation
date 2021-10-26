using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Gestion_Recommandation.Shared.Models
{
    public class Trace_Recommandations
    {
        [Key]
        public int Id { get; set; }
        public int ID_Recommandations { get; set; }
        public string Description { get; set; }
        public string AgentBureau { get; set; }
        public string AgentSaisie { get; set; }
        public DateTime DateSaisie { get; set; } = DateTime.Now;
    }
}
