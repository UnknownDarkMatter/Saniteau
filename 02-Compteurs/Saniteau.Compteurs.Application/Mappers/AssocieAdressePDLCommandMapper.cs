using Saniteau.Common.Mediator;
using Saniteau.Compteurs.Contract.Commands;
using Saniteau.Compteurs.Domain;
using Saniteau.Compteurs.Domain.Commands;
using System;
using System.Collections.Generic;
using System.Text;

namespace Saniteau.Compteurs.Application.Mappers
{
    public class AssocieAdressePDLCommandMapper : IActionMapper<AssocieAdressePDLCommand, AssocieAdressePDLDomainCommand>
    {
        public AssocieAdressePDLDomainCommand Map(AssocieAdressePDLCommand action, IActionValidation<AssocieAdressePDLCommand> validation)
        {
            return new AssocieAdressePDLDomainCommand(IdAdresse.Parse(action.IdAdresse), IdCompteur.Parse(action.IdCompteur));
        }
    }
}
