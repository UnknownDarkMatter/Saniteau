using Saniteau.Common.Mediator;
using Saniteau.Compteurs.Contract.Commands;
using Saniteau.Compteurs.Domain;
using Saniteau.Compteurs.Domain.Commands;
using System;
using System.Collections.Generic;
using System.Text;

namespace Saniteau.Compteurs.Application.Mappers
{
    public class EnregistreCompteurCommandMapper : IActionMapper<EnregistreCompteurCommand, EnregistreCompteurDomainCommand>
    {
        public EnregistreCompteurDomainCommand Map(EnregistreCompteurCommand action, IActionValidation<EnregistreCompteurCommand> validation)
        {
            return new EnregistreCompteurDomainCommand(IdCompteur.Parse(action.IdCompteur), new ChampLibre(action.NuméroCompteur));
        }
    }
}
