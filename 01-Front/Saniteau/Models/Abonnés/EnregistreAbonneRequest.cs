using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Saniteau.Compteurs.Contract.Model;

namespace Saniteau.Models.Abonnés
{
    public class EnregistreAbonneRequest
    {
        public int IdAbonne { get; set; }
        public int IdAdresse { get; set; }
        public string Nom { get; set; }
        public string Prenom { get; set; }
        public string Adresse { get; set; }
        public string Ville { get; set; }
        public string CodePostal { get; set; }
        public Tarification Tarification { get; set; }
    }
}
