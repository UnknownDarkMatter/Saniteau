using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Saniteau.Compteurs.Domain.Commands
{
    public class GetCompteurDomainCommand
    {
        public IdCompteur IdCompteur { get; private set; }
        public GetCompteurDomainCommand(IdCompteur idCompteur)
        {
            if (idCompteur is null) { throw new ArgumentNullException(nameof(idCompteur)); }

            IdCompteur = idCompteur;
        }

        public Compteur GetCompteur(RéférentielCompteurs référentielCompteurs)
        {
            return référentielCompteurs.GetCompteur(IdCompteur);
        }
    }
}
