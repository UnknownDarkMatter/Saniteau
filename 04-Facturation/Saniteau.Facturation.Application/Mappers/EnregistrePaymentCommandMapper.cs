using Saniteau.Common.Mediator;
using Saniteau.Facturation.Contract.Commands;
using Saniteau.Facturation.Domain;
using Saniteau.Facturation.Domain.Commands;
using System;
using System.Collections.Generic;
using System.Text;

namespace Saniteau.Facturation.Application.Mappers
{
    public class EnregistrePaymentCommandMapper : IActionMapper<EnregistrePaymentCommand, EnregistrePaymentDomainCommand>
    {
        public EnregistrePaymentDomainCommand Map(EnregistrePaymentCommand action, IActionValidation<EnregistrePaymentCommand> validation)
        {
            return new EnregistrePaymentDomainCommand(action.OrderId, action.IdFacturation);
        }
    }
}
