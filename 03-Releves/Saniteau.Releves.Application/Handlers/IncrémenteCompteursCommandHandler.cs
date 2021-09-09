using Saniteau.Common.Mediator;
using Saniteau.Releves.Application.Mappers;
using Saniteau.Releves.Contract.Commands;
using Saniteau.Releves.Domain;
using Saniteau.Releves.Domain.Commands;
using System;
using System.Collections.Generic;
using System.Text;

namespace Saniteau.Releves.Application.Handlers
{
    public class IncrémenteCompteursCommandHandler : ActionHandlerBase<IncrémenteCompteursCommand, IncrémenteCompteursDomainCommand>
    {
        private readonly RéférentielCompteurs _référentielCompteurs;
        private readonly RéférentielIndexesCompteurs _référentielIndexesCompteurs;
        private readonly RéférentielPompes _référentielPompes;
        public IncrémenteCompteursCommandHandler(RéférentielCompteurs référentielCompteurs, RéférentielIndexesCompteurs référentielIndexesCompteurs, RéférentielPompes référentielPompes) 
            : base(new IncrémenteCompteursCommandMapper())
        {
            if (référentielCompteurs is null) { throw new ArgumentNullException(nameof(référentielCompteurs)); }
            if (référentielIndexesCompteurs is null) { throw new ArgumentNullException(nameof(référentielIndexesCompteurs)); }
            if (référentielPompes is null) { throw new ArgumentNullException(nameof(référentielPompes)); }

            _référentielCompteurs = référentielCompteurs;
            _référentielIndexesCompteurs = référentielIndexesCompteurs;
            _référentielPompes = référentielPompes;
        }

        protected override void Handle(IncrémenteCompteursDomainCommand action)
        {
            action.ExecuteRelevé(_référentielCompteurs, _référentielIndexesCompteurs, _référentielPompes);
        }
    }
}
