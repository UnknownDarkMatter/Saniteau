using Saniteau.Common.Mediator;
using Saniteau.Compteurs.Application.Mappers;
using Saniteau.Compteurs.Contract.Commands;
using Saniteau.Compteurs.Contract.Model;
using Saniteau.Compteurs.Domain;
using Saniteau.Compteurs.Domain.Commands;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Saniteau.Compteurs.Application.Handlers
{
    public class GetAbonnéCommandHandler : ActionHandlerBase<GetAbonnéCommand, GetAbonnéDomainCommand, Contract.Model.Abonne>
    {
        private readonly RéférentielAbonnés _référentielAbonnés;
        public GetAbonnéCommandHandler(RéférentielAbonnés référentielAbonnés) : base (new GetAbonnéCommandMapper())
        {
            if (référentielAbonnés is null) { throw new ArgumentNullException(nameof(référentielAbonnés)); }

            _référentielAbonnés = référentielAbonnés;
        }

        protected override Contract.Model.Abonne Handle(GetAbonnéDomainCommand action)
        {
            var abonné = _référentielAbonnés.GetAbonné(action.IdAbonné);
            var adresse = _référentielAbonnés.GetAddresse(abonné.IdAdresse);
            var abonnéContract = AbonnéMapper.Map(abonné, adresse);
            return abonnéContract;
        }
    }
}
