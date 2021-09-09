using Saniteau.Common.Mediator;
using Saniteau.DSP.Contract.Commands;
using Saniteau.DSP.Domain.Commands;
using System;
using System.Collections.Generic;
using System.Text;

namespace Saniteau.DSP.Application.Mappers
{
    public class ObtientPayesCommandMapper : IActionMapper<ObtientPayesCommand, ObtientPayesDomainCommand>
    {
        public ObtientPayesDomainCommand Map(ObtientPayesCommand action, IActionValidation<ObtientPayesCommand> validation)
        {
            return new ObtientPayesDomainCommand();
        }
    }
}
