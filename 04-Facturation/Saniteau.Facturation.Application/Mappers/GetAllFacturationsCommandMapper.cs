using Saniteau.Common.Mediator;
using Saniteau.Facturation.Contract.Commands;
using Saniteau.Facturation.Domain;
using Saniteau.Facturation.Domain.Commands;
using System;
using System.Collections.Generic;
using System.Text;

namespace Saniteau.Facturation.Application.Mappers
{
    public class GetAllFacturationsCommandMapper : IActionMapper<GetAllFacturationsCommand, GetAllFacturationsDomainCommand>
    {
        public GetAllFacturationsDomainCommand Map(GetAllFacturationsCommand action, IActionValidation<GetAllFacturationsCommand> validation)
        {
            return new GetAllFacturationsDomainCommand();
        }
    }
}
