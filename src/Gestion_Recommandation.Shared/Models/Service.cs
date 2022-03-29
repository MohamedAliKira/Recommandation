using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Gestion_Recommandation.Shared.Models
{
    public class Service
    {
        [Key]
        public int Id { get; set; }
        public string Libelle { get; set; }
        public int Tuttelle { get; set; }
    }
}
