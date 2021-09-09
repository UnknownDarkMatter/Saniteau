using Saniteau.Common.Mediator;
using Saniteau.Compteurs.Contract.Commands;
using Saniteau.Compteurs.Domain;
using Saniteau.Compteurs.Domain.Commands;
using System;
using System.Collections.Generic;
using System.Text;

namespace Saniteau.Compteurs.Application.Mappers
{
    public class DissocieAdressePDLCommandMapper : IActionMapper<DissocieAdressePDLCommand, DissocieAdressePDLDomainCommand>
    {
        public DissocieAdressePDLDomainCommand Map(DissocieAdressePDLCommand action, IActionValidation<DissocieAdressePDLCommand> validation)
        {
            return new DissocieAdressePDLDomainCommand(IdAdresse.Parse(action.IdAdresse), IdCompteur.Parse(action.IdCompteur));
        }
    }
}
