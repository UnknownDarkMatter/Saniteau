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
    public class DesappairageCommandHandler : ActionHandlerBase<DesappairageCommand, DesappairageDomainCommand>
    {
        private readonly RéférentielAppairage _référentielAppairage;

        public DesappairageCommandHandler(RéférentielAppairage référentielAppairage) : base(new DesappairageCommandMapper())
        {
            if (référentielAppairage is null) { throw new ArgumentNullException(nameof(référentielAppairage)); }

            _référentielAppairage = référentielAppairage;
        }

        protected override void Handle(DesappairageDomainCommand action)
        {
            action.DesappairageCompteur( _référentielAppairage);
        }

    }
}
