using Saniteau.Common.Mediator;
using Saniteau.DSP.Application.Mappers;
using Saniteau.DSP.Contract.Commands;
using Saniteau.DSP.Contract.Model;
using Saniteau.DSP.Domain;
using Saniteau.DSP.Domain.Commands;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Saniteau.DSP.Application.Handlers
{
    public class ObtientDelegantCommandHandler : ActionHandlerBase<ObtientDelegantCommand, ObtientDelegantDomainCommand, List<Contract.Model.Delegant>>
    {
        public RéférentielDelegant _référentielDelegant { get; set; }

        public ObtientDelegantCommandHandler(RéférentielDelegant référentielDelegant) : base(new ObtientDelegantCommandMapper())
        {
            _référentielDelegant = référentielDelegant ?? throw new ArgumentNullException(nameof(référentielDelegant));
        }

        protected override List<Contract.Model.Delegant> Handle(ObtientDelegantDomainCommand action)
        {
            var delegants = action.ObtientDéléguants(_référentielDelegant);
            return delegants.Select(DelegantMapper.Map).ToList();
        }
    }
}
