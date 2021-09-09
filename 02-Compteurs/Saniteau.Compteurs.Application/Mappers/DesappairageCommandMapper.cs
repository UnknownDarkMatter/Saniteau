using Saniteau.Common.Mediator;
using Saniteau.Compteurs.Contract.Commands;
using Saniteau.Compteurs.Domain;
using Saniteau.Compteurs.Domain.Commands;
using System;
using System.Collections.Generic;
using System.Text;

namespace Saniteau.Compteurs.Application.Mappers
{
    public class DesappairageCommandMapper : IActionMapper<DesappairageCommand, DesappairageDomainCommand>
    {
        public DesappairageDomainCommand Map(DesappairageCommand action, IActionValidation<DesappairageCommand> validation)
        {
            return new DesappairageDomainCommand(IdAppairageCompteur.Parse(action.IdAppairageCompteur));
        }
    }
}
