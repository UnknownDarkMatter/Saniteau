using Saniteau.Common.Mediator;
using Saniteau.DSP.Application.Mappers;
using Saniteau.DSP.Contract.Commands;
using Saniteau.DSP.Domain;
using Saniteau.DSP.Domain.Commands;
using System;
using System.Collections.Generic;
using System.Text;

namespace Saniteau.DSP.Application.Handlers
{
    public class CréePayeCommandHandler : ActionHandlerBase<CréePayeCommand, CréePayeDomainCommand>
    {
        private RéférentielPaye _référentielPaye;
        private RéférentielAbonnés _référentielAbonnés;
        private RéférentielFacturation _référentielFacturation;
        private RéférentielPompes _référentielPompes;
        private RéférentielIndexesCompteurs _référentielIndexesCompteurs;
        private RéférentielCompteurs _référentielCompteurs;

        public CréePayeCommandHandler(RéférentielPaye référentielPaye, RéférentielAbonnés référentielAbonnés,
            RéférentielFacturation référentielFacturation, RéférentielPompes référentielPompes, RéférentielIndexesCompteurs référentielIndexesCompteurs,
            RéférentielCompteurs référentielCompteurs) : base (new CréePayeCommandMapper())
        {
            if (référentielPaye is null) { throw new ArgumentNullException(nameof(référentielPaye)); }
            if (référentielAbonnés is null) { throw new ArgumentNullException(nameof(référentielAbonnés)); }
            if (référentielFacturation is null) { throw new ArgumentNullException(nameof(référentielFacturation)); }
            if (référentielPompes is null) { throw new ArgumentNullException(nameof(référentielPompes)); }
            if (référentielIndexesCompteurs is null) { throw new ArgumentNullException(nameof(référentielIndexesCompteurs)); }
            if (référentielCompteurs is null) { throw new ArgumentNullException(nameof(référentielCompteurs)); }

            _référentielPaye = référentielPaye;
            _référentielAbonnés = référentielAbonnés;
            _référentielFacturation = référentielFacturation;
            _référentielPompes = référentielPompes;
            _référentielIndexesCompteurs = référentielIndexesCompteurs;
            _référentielCompteurs = référentielCompteurs;
        }

        protected override void Handle(CréePayeDomainCommand action)
        {
            var payeCalculator = new PayeCalculator(_référentielPaye, _référentielAbonnés, _référentielFacturation, _référentielPompes, _référentielIndexesCompteurs, _référentielCompteurs);
            var payeDelegant = payeCalculator.CalculePayeDelegant(action.DatePaye, action.IdDelegant, out List<IndexPayéParDelegant> nouveauxIndexPayés, out List<FacturePayeeAuDelegant> nouvellesFacturesPayées);

            payeDelegant = _référentielPaye.EnregistrePayeDelegant(payeDelegant);
            foreach(var nouveauIndexPayé in nouveauxIndexPayés)
            {
                _référentielPaye.EnregistreIndexesPayésParDelegant(nouveauIndexPayé);
            }
            foreach (var nouvellesFacturePayée in nouvellesFacturesPayées)
            {
                _référentielPaye.EnregistreFacturePayeeAuDelegant(nouvellesFacturePayée);
            }
        }
    }
}
