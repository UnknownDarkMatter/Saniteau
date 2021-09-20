using Saniteau.Common.Mediator;
using Saniteau.Compteurs.Application.Mappers;
using Saniteau.Compteurs.Contract.Commands;
using Saniteau.Compteurs.Domain;
using Saniteau.Compteurs.Domain.Commands;
using System;
using System.Collections.Generic;
using System.Text;

namespace Saniteau.Compteurs.Application.Handlers
{
    public class DeleteCompteurCommandHandler : ActionHandlerBase<DeleteCompteurCommand, DeleteCompteurDomainCommand>
    {
        private readonly RéférentielCompteurs _référentielCompteurs;
        private readonly RéférentielAppairage _référentielAppairage;
        private readonly RéférentielPDL _référentielPDL;
        private readonly RéférentielIndexesCompteurs _référentielIndexesCompteurs;

        public DeleteCompteurCommandHandler(RéférentielCompteurs référentielCompteurs,
           RéférentielAppairage référentielAppairage, RéférentielPDL référentielPDL,
           RéférentielIndexesCompteurs référentielIndexesCompteurs) : base(new DeleteCompteurCommandMapper())
        {
            _référentielCompteurs = référentielCompteurs ?? throw new ArgumentNullException(nameof(référentielCompteurs));
            _référentielAppairage = référentielAppairage ?? throw new ArgumentNullException(nameof(référentielAppairage));
            _référentielPDL = référentielPDL ?? throw new ArgumentNullException(nameof(référentielPDL));
            _référentielIndexesCompteurs = référentielIndexesCompteurs ?? throw new ArgumentNullException(nameof(référentielIndexesCompteurs));
        }

        protected override void Handle(DeleteCompteurDomainCommand action)
        {
            action.DeleteCompteur(_référentielCompteurs, _référentielAppairage, _référentielPDL, _référentielIndexesCompteurs);
        }
    }
}
