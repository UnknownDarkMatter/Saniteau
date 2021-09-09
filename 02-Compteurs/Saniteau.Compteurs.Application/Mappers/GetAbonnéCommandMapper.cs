using Saniteau.Common.Mediator;
using Saniteau.Compteurs.Contract.Commands;
using Saniteau.Compteurs.Domain;
using Saniteau.Compteurs.Domain.Commands;
using System;
using System.Collections.Generic;
using System.Text;

namespace Saniteau.Compteurs.Application.Mappers
{
    public class GetAbonnéCommandMapper : IActionMapper<GetAbonnéCommand, GetAbonnéDomainCommand>
    {
        public GetAbonnéDomainCommand Map(GetAbonnéCommand action, IActionValidation<GetAbonnéCommand> validation)
        {
            return new GetAbonnéDomainCommand(IdAbonné.Parse(action.IdAbonné));
        }
    }
}
