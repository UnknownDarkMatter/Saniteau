using Saniteau.Common.Mediator;
using Saniteau.Compteurs.Contract.Commands;
using Saniteau.Compteurs.Domain;
using Saniteau.Compteurs.Domain.Commands;
using System;
using System.Collections.Generic;
using System.Text;

namespace Saniteau.Compteurs.Application.Mappers
{
    public class EnregistreAbonnéCommandMapper : IActionMapper<EnregistreAbonnéCommand, EnregistreAbonnéDomainCommand>
    {
        public EnregistreAbonnéDomainCommand Map(EnregistreAbonnéCommand action, IActionValidation<EnregistreAbonnéCommand> validation)
        {
            var tarification = TarificationMapper.Map(action.Tarification);
            return new EnregistreAbonnéDomainCommand(IdAbonné.Parse(action.IdAbonné), IdAdresse.Parse(action.IdAdresse), new ChampLibre(action.Nom), new ChampLibre(action.Prénom), tarification);
        }
    }
}
