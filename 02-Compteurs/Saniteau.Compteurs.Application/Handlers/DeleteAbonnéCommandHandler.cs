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
    public class DeleteAbonnéCommandHandler : ActionHandlerBase<DeleteAbonnéCommand, DeleteAbonnéDomainCommand>
    {
        private readonly RéférentielAbonnés _référentielAbonnés;

        public DeleteAbonnéCommandHandler(RéférentielAbonnés référentielAbonnés) : base(new DeleteAbonnéCommandMapper())
        {
            if (référentielAbonnés is null) { throw new ArgumentNullException(nameof(référentielAbonnés)); }

            _référentielAbonnés = référentielAbonnés;
        }

        protected override void Handle(DeleteAbonnéDomainCommand action)
        {
            action.DeleteAbonné(_référentielAbonnés);
        }
    }
}
