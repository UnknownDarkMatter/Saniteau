using Saniteau.Common.Mediator;
using Saniteau.DSP.Contract.Commands;
using Saniteau.DSP.Domain;
using Saniteau.DSP.Domain.Commands;
using System;
using System.Collections.Generic;
using System.Text;

namespace Saniteau.DSP.Application.Mappers
{
    public class CréePayeCommandMapper : IActionMapper<CréePayeCommand, CréePayeDomainCommand>
    {
        public CréePayeDomainCommand Map(CréePayeCommand action, IActionValidation<CréePayeCommand> validation)
        {
            return new CréePayeDomainCommand(action.DatePaye, IdDelegant.Parse(action.IdDelegant));
        }
    }
}
