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
    public class ObtientPayesCommandHandler : ActionHandlerBase<ObtientPayesCommand, ObtientPayesDomainCommand, List<Contract.Model.PayeDelegant>>
    {
        private readonly RéférentielPaye _référentielPaye;
        public ObtientPayesCommandHandler(RéférentielPaye référentielPaye) : base (new ObtientPayesCommandMapper())
        {
            if(référentielPaye is null) { throw new ArgumentNullException(nameof(référentielPaye));  }

            _référentielPaye = référentielPaye;
        }

        protected override List<Contract.Model.PayeDelegant> Handle(ObtientPayesDomainCommand action)
        {
            var payesDelegant = _référentielPaye.GetPayesDelegants();
            return payesDelegant.Select(PayeDelegantMapper.Map).ToList();
        }
    }
}
