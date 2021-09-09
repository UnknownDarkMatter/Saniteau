using Saniteau.Common.Mediator;
using Saniteau.Facturation.Contract.Commands;
using Saniteau.Facturation.Domain;
using Saniteau.Facturation.Domain.Commands;
using System;
using System.Collections.Generic;
using System.Text;

namespace Saniteau.Facturation.Application.Mappers
{
    public class CréeFacturationCommandMapper : IActionMapper<CréeFacturationCommand, CréeFacturationDomainCommand>
    {
        public CréeFacturationDomainCommand Map(CréeFacturationCommand action, IActionValidation<CréeFacturationCommand> validation)
        {
            return new CréeFacturationDomainCommand(action.DateFacturation);
        }
    }
}
