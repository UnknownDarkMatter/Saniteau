using Saniteau.Common;
using System;
using System.Collections.Generic;
using System.Text;

namespace Saniteau.DSP.Domain.Commands
{
    public class CréePayeDomainCommand
    {
        public Date DatePaye { get; private set; }
        public IdDelegant IdDelegant { get; private set; }

        public CréePayeDomainCommand(Date datePaye, IdDelegant idDelegant)
        {
            if (datePaye is null) { throw new ArgumentNullException(nameof(datePaye)); }
            if (idDelegant is null) { throw new ArgumentNullException(nameof(idDelegant)); }

            DatePaye = datePaye;
            IdDelegant = idDelegant;
        }

        public void CréePayeDéléguant(RéférentielPaye référentielPaye, RéférentielAbonnés référentielAbonnés,
            RéférentielFacturation référentielFacturation, RéférentielPompes référentielPompes, RéférentielIndexesCompteurs référentielIndexesCompteurs,
            RéférentielCompteurs référentielCompteurs)
        {
            var payeCalculator = new PayeCalculator(référentielPaye, référentielAbonnés, référentielFacturation, référentielPompes, référentielIndexesCompteurs, référentielCompteurs);
            var payeDelegant = payeCalculator.CalculePayeDelegant(DatePaye, IdDelegant, out List<IndexPayéParDelegant> nouveauxIndexPayés, out List<FacturePayeeAuDelegant> nouvellesFacturesPayées);

            payeDelegant = référentielPaye.EnregistrePayeDelegant(payeDelegant);
            foreach (var nouveauIndexPayé in nouveauxIndexPayés)
            {
                référentielPaye.EnregistreIndexesPayésParDelegant(nouveauIndexPayé);
            }
            foreach (var nouvellesFacturePayée in nouvellesFacturesPayées)
            {
                référentielPaye.EnregistreFacturePayeeAuDelegant(nouvellesFacturePayée);
            }

        }
    }
}
