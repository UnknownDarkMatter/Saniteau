using Saniteau.Common.Mediator;
using Saniteau.Compteurs.Contract.Commands;
using Saniteau.Compteurs.Domain;
using Saniteau.Compteurs.Domain.Commands;
using System;
using System.Collections.Generic;
using System.Text;

namespace Saniteau.Compteurs.Application.Mappers
{
    public class GetLastIndexCommandMapper : IActionMapper<GetLastIndexCommand, GetLastIndexDomainCommand>
    {
        public GetLastIndexDomainCommand Map(GetLastIndexCommand action, IActionValidation<GetLastIndexCommand> validation)
        {
            return new GetLastIndexDomainCommand(IdCompteur.Parse(action.IdCompteur));
        }
    }
}
