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
    public class PoseCompteurCommandHandler : ActionHandlerBase<PoseCompteurCommand, PoseCompteurDomainCommand>
    {
        private readonly RéférentielCompteurs _référentielCompteurs;
        private readonly RéférentielIndexesCompteurs _référentielIndexesCompteurs;

        public PoseCompteurCommandHandler(RéférentielCompteurs référentielCompteurs, RéférentielIndexesCompteurs référentielIndexesCompteurs) : base(new PoseCompteurCommandMapper())
        {
            if (référentielCompteurs is null) { throw new ArgumentNullException(nameof(référentielCompteurs)); }
            if (référentielIndexesCompteurs is null) { throw new ArgumentNullException(nameof(référentielIndexesCompteurs)); }

            _référentielCompteurs = référentielCompteurs;
            _référentielIndexesCompteurs = référentielIndexesCompteurs;
        }

        protected override void Handle(PoseCompteurDomainCommand action)
        {
            action.PoseCompteur(_référentielCompteurs, _référentielIndexesCompteurs);
        }
    }
}
