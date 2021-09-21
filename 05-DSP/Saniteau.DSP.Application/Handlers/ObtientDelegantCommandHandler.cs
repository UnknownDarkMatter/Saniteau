using Saniteau.Common.Mediator;
using Saniteau.DSP.Application.Mappers;
using Saniteau.DSP.Contract.Commands;
using Saniteau.DSP.Contract.Model;
using Saniteau.DSP.Domain;
using Saniteau.DSP.Domain.Commands;
using System;
using System.Collections.Generic;
using System.Text;

namespace Saniteau.DSP.Application.Handlers
{
    public class ObtientDelegantCommandHandler : ActionHandlerBase<ObtientDelegantCommand, ObtientDelegantDomainCommand, Contract.Model.Delegant>
    {
        public RéférentielDelegant _référentielDelegant { get; set; }

        public ObtientDelegantCommandHandler(RéférentielDelegant référentielDelegant) : base(new ObtientDelegantCommandMapper())
        {
            _référentielDelegant = référentielDelegant ?? throw new ArgumentNullException(nameof(référentielDelegant));
        }

        protected override Contract.Model.Delegant Handle(ObtientDelegantDomainCommand action)
        {
            var delegant = action.ObtientDéléguant(_référentielDelegant);
            return DelegantMapper.Map(delegant);
        }
    }
}
