using Saniteau.Common.Mediator;
using Saniteau.Facturation.Contract.Commands;
using Saniteau.Facturation.Domain;
using Saniteau.Facturation.Domain.Commands;
using System;
using System.Collections.Generic;
using System.Text;

namespace Saniteau.Facturation.Application.Mappers
{
    public class GetFacturationCommandMapper : IActionMapper<GetFacturationCommand, GetFacturationDomainCommand>
    {
        public GetFacturationDomainCommand Map(GetFacturationCommand action, IActionValidation<GetFacturationCommand> validation)
        {
            return new GetFacturationDomainCommand(IdFacturation.Parse(action.IdFacturation), IdAbonné.Parse(action.IdAbonne));
        }
    }
}
