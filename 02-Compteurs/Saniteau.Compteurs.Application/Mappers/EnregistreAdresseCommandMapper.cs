using Saniteau.Common.Mediator;
using Saniteau.Compteurs.Contract.Commands;
using Saniteau.Compteurs.Domain;
using Saniteau.Compteurs.Domain.Commands;
using System;
using System.Collections.Generic;
using System.Text;

namespace Saniteau.Compteurs.Application.Mappers
{
    public class EnregistreAdresseCommandMapper : IActionMapper<EnregistreAdresseCommand, EnregistreAdresseDomainCommand>
    {
        public EnregistreAdresseDomainCommand Map(EnregistreAdresseCommand action, IActionValidation<EnregistreAdresseCommand> validation)
        {
            return new EnregistreAdresseDomainCommand(IdAdresse.Parse(action.IdAdresse), new ChampLibre(action.NuméroEtRue), new ChampLibre(action.Ville), new ChampLibre(action.CodePostal));
        }
    }
}
