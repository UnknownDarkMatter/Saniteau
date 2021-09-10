using Saniteau.Common.Mediator;
using Saniteau.Facturation.Application.Mappers;
using Saniteau.Facturation.Contract.Commands;
using Saniteau.Facturation.Domain;
using Saniteau.Facturation.Domain.Commands;
using System;
using System.Collections.Generic;
using System.Text;

namespace Saniteau.Facturation.Application.Handlers
{
    public class GetFacturationCommandHandler : ActionHandlerBase<GetFacturationCommand, GetFacturationDomainCommand, Contract.Model.Facturation>
    {
        private readonly RéférentielFacturation _référentielFacturation;
        private readonly RéférentielAbonnés _référentielAbonnés;

        public GetFacturationCommandHandler(RéférentielFacturation référentielFacturation, RéférentielAbonnés référentielAbonnés)
            : base(new GetFacturationCommandMapper())
        {
            _référentielFacturation = référentielFacturation ?? throw new ArgumentNullException(nameof(référentielFacturation));
            _référentielAbonnés = référentielAbonnés ?? throw new ArgumentNullException(nameof(référentielAbonnés));
        }

        protected override Contract.Model.Facturation Handle(GetFacturationDomainCommand action)
        {
           var facturationAdresse = action.GetFacturation(_référentielFacturation, _référentielAbonnés);
            return FacturationMapper.Map(facturationAdresse.Item1, facturationAdresse.Item2);
        }
    }
}
