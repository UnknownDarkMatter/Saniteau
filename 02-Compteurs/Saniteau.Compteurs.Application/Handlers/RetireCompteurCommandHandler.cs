using Saniteau.Common.Mediator;
using Saniteau.Compteurs.Application.Mappers;
using Saniteau.Compteurs.Contract.Commands;
using Saniteau.Compteurs.Contract.Model;
using Saniteau.Compteurs.Domain;
using Saniteau.Compteurs.Domain.Commands;
using System;
using System.Collections.Generic;
using System.Text;

namespace Saniteau.Compteurs.Application.Handlers
{
    public class RetireCompteurCommandHandler : ActionHandlerBase<RetireCompteurCommand, RetireCompteurDomainCommand>
    {
        private readonly RéférentielCompteurs _référentielCompteurs;
        private readonly RéférentielIndexesCompteurs _référentielIndexesCompteurs;
        private readonly RéférentielAppairage _référentielAppairage;
        private readonly RéférentielPDL _référentielPDL;

        public RetireCompteurCommandHandler(RéférentielCompteurs référentielCompteurs, RéférentielIndexesCompteurs référentielIndexesCompteurs,
            RéférentielAppairage référentielAppairage, RéférentielPDL référentielPDL) : base(new RetireCompteurCommandMapper())
        {
            if (référentielCompteurs is null) { throw new ArgumentNullException(nameof(référentielCompteurs)); }
            if (référentielIndexesCompteurs is null) { throw new ArgumentNullException(nameof(référentielIndexesCompteurs)); }
            if (référentielAppairage is null) { throw new ArgumentNullException(nameof(référentielAppairage)); }
            if (référentielPDL is null) { throw new ArgumentNullException(nameof(référentielPDL)); }

            _référentielCompteurs = référentielCompteurs;
            _référentielIndexesCompteurs = référentielIndexesCompteurs;
            _référentielAppairage = référentielAppairage;
            _référentielPDL = référentielPDL;
        }


        protected override void Handle(RetireCompteurDomainCommand action)
        {
            action.RetireCompteur(_référentielCompteurs, _référentielIndexesCompteurs, _référentielAppairage, _référentielPDL);
        }
    }
}
