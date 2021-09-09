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
    public class EnregistreAdresseCommandHandler : ActionHandlerBase<EnregistreAdresseCommand, EnregistreAdresseDomainCommand, Contract.Model.Adresse>
    {
        private readonly RéférentielAbonnés _référentielAbonnés;

        public EnregistreAdresseCommandHandler(RéférentielAbonnés référentielAbonnés) : base(new EnregistreAdresseCommandMapper())
        {
            if(référentielAbonnés is null) { throw new ArgumentNullException(nameof(référentielAbonnés));  }

            _référentielAbonnés = référentielAbonnés;
        }

        protected override Contract.Model.Adresse Handle(EnregistreAdresseDomainCommand action)
        {
            var adresse = action.EnregistreAdresse(_référentielAbonnés);
            return AdresseMapper.Map(adresse);
        }
    }
}
