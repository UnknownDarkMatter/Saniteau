using Saniteau.Common.Mediator;
using Saniteau.DSP.Contract.Commands;
using Saniteau.DSP.Domain.Commands;
using System;
using System.Collections.Generic;
using System.Text;

namespace Saniteau.DSP.Application.Mappers
{
    public class ObtientDelegantCommandMapper : IActionMapper<ObtientDelegantCommand, ObtientDelegantDomainCommand>
    {
        public ObtientDelegantDomainCommand Map(ObtientDelegantCommand action, IActionValidation<ObtientDelegantCommand> validation)
        {
            return new ObtientDelegantDomainCommand();
        }
    }
}
