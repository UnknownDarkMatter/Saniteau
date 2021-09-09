using Saniteau.Common.Mediator;
using Saniteau.Compteurs.Contract.Commands;
using Saniteau.Compteurs.Domain;
using Saniteau.Compteurs.Domain.Commands;
using System;
using System.Collections.Generic;
using System.Text;

namespace Saniteau.Compteurs.Application.Mappers
{
    public class GetAllAbonnésCommandMapper : IActionMapper<GetAllAbonnésCommand, GetAllAbonnésDomainCommand>
    {
        public GetAllAbonnésDomainCommand Map(GetAllAbonnésCommand action, IActionValidation<GetAllAbonnésCommand> validation)
        {
            return new GetAllAbonnésDomainCommand(action.FiltrerAbonnésAvecCompteur);
        }
    }
}
