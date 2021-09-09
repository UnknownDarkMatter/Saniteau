using Saniteau.Common.Mediator;
using Saniteau.Facturation.Application.Mappers;
using Saniteau.Facturation.Contract.Commands;
using Saniteau.Facturation.Domain;
using Saniteau.Facturation.Domain.Commands;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Saniteau.Facturation.Application.Handlers
{
    public class GetAllFacturationsCommandHandler : ActionHandlerBase<GetAllFacturationsCommand, GetAllFacturationsDomainCommand, List<Contract.Model.Facturation>>
    {
        private readonly RéférentielFacturation _référentielFacturation;
        private readonly RéférentielAbonnés _référentielAbonnés;

        public GetAllFacturationsCommandHandler(RéférentielFacturation référentielFacturation, RéférentielAbonnés référentielAbonnés) : base(new GetAllFacturationsCommandMapper())
        {
            if (référentielFacturation is null) { throw new ArgumentNullException(nameof(référentielFacturation)); }
            if (référentielAbonnés is null) { throw new ArgumentNullException(nameof(référentielAbonnés)); }

            _référentielFacturation = référentielFacturation;
            _référentielAbonnés = référentielAbonnés;
        }

        protected override List<Contract.Model.Facturation> Handle(GetAllFacturationsDomainCommand action)
        {
            var facturations = new List<Contract.Model.Facturation>();
            var allAdresses = _référentielAbonnés.GetAllAddresses();
            foreach(var facturation in action.GetAllFacturations(_référentielFacturation))
            {
                var adresse = allAdresses.FirstOrDefault(m => m.IdAdresse == facturation.Abonné.IdAdresse);
                facturations.Add(FacturationMapper.Map(facturation, adresse));
            }
            return facturations;
        }
    }
}
