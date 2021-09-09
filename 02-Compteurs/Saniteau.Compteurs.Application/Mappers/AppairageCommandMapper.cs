using Saniteau.Common.Mediator;
using Saniteau.Compteurs.Contract.Commands;
using Saniteau.Compteurs.Domain;
using Saniteau.Compteurs.Domain.Commands;
using System;
using System.Collections.Generic;
using System.Text;

namespace Saniteau.Compteurs.Application.Mappers
{
    public class AppairageCommandMapper : IActionMapper<AppairageCommand, AppairageDomainCommand>
    {
        public AppairageDomainCommand Map(AppairageCommand action, IActionValidation<AppairageCommand> validation)
        {
            return new AppairageDomainCommand(IdAppairageCompteur.Parse(action.IdAppairageCompteur), IdAdresse.Parse(action.IdAdresse), IdCompteur.Parse(action.IdCompteur), IdPDL.Parse(action.IdPDL));
        }
    }
}
