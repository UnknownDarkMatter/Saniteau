using System;
using System.Collections.Generic;
using System.Text;

namespace Saniteau.Compteurs.Domain.Commands
{
    public class GetLastIndexDomainCommand
    {
        public IdCompteur IdCompteur { get; private set; }

        public GetLastIndexDomainCommand(IdCompteur idCompteur)
        {
            IdCompteur = idCompteur;
        }

        public IndexCompteur GetLastIndex(RéférentielIndexesCompteurs référentielIndexesCompteurs)
        {
            var compteur = new Compteur(IdCompteur, new ChampLibre("XXX"), true, true, null, IdAbonné.Parse(0));
            return compteur.GetLastIndexCompteur(référentielIndexesCompteurs);
        }
    }
}
