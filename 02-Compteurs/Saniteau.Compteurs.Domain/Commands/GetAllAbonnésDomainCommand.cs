using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Saniteau.Compteurs.Domain.Commands
{
    public class GetAllAbonnésDomainCommand
    {
        public bool FiltrerAbonnésAvecCompteur { get; set; }

        public GetAllAbonnésDomainCommand(bool filtrerAbonnésAvecCompteur)
        {
            FiltrerAbonnésAvecCompteur = filtrerAbonnésAvecCompteur;
        }
    }
}
