using Saniteau.Common.Mediator;
using Saniteau.Releves.Application.Mappers;
using Saniteau.Releves.Contract.Commands;
using Saniteau.Releves.Contract.Model;
using Saniteau.Releves.Domain;
using Saniteau.Releves.Domain.Commands;
using System;
using System.Collections.Generic;
using System.Text;

namespace Saniteau.Releves.Application.Handlers
{
    public class CréePompeCommandHandler : ActionHandlerBase<CréePompeCommand, CréePompeDomainCommand, Contract.Model.Pompe>
    {
        private readonly RéférentielPompes _référentielPompes;
        public CréePompeCommandHandler(RéférentielPompes référentielPompes) : base (new CréePompeCommandMapper())
        {
            if(référentielPompes is null) { throw new ArgumentNullException(nameof(référentielPompes)); }

            _référentielPompes = référentielPompes;
        }

        protected override Contract.Model.Pompe Handle(CréePompeDomainCommand action)
        {
            var pompe = action.CréePompe(_référentielPompes);
            return PompeMapper.Map(pompe);
        }
    }
}
