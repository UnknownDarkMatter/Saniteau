using System;
using System.Collections.Generic;
using System.Text;

namespace Saniteau.Releves.Domain.Commands
{
    public class IncrémenteCompteursDomainCommand
    {
        public void ExecuteRelevé(RéférentielCompteurs référentielCompteurs, RéférentielIndexesCompteurs référentielIndexesCompteurs, RéférentielPompes référentielPompes)
        {
            IndexRelevéMoteur indexRelevéMoteur = new IndexRelevéMoteur();
            indexRelevéMoteur.ExecuteRelevé(référentielCompteurs, référentielIndexesCompteurs, référentielPompes);
        }

    }
}
