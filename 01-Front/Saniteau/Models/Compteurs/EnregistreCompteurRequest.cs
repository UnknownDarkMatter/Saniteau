using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Saniteau.Models.Compteurs
{
    public class EnregistreCompteurRequest
    {
        public int IdCompteur { get; set; }
        public string NomCompteur { get; set; }
    }
}
