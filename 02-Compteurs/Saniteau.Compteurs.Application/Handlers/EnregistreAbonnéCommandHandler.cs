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
    public class EnregistreAbonnéCommandHandler : ActionHandlerBase<EnregistreAbonnéCommand, EnregistreAbonnéDomainCommand, Contract.Model.Abonne>
    {
        private readonly RéférentielAbonnés _référentielAbonnés;

        public EnregistreAbonnéCommandHandler(RéférentielAbonnés référentielAbonnés) : base(new EnregistreAbonnéCommandMapper())
        {
            if (référentielAbonnés is null) { throw new ArgumentNullException(nameof(référentielAbonnés)); }

            _référentielAbonnés = référentielAbonnés;
        }

        protected override Contract.Model.Abonne Handle(EnregistreAbonnéDomainCommand action)
        {
            var tuple = action.EnregistreAbonné(_référentielAbonnés);
            return AbonnéMapper.Map(tuple.Item1, tuple.Item2);
        }
    }
}
