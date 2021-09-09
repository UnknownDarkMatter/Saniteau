using Saniteau.Common.Mediator;
using Saniteau.Compteurs.Contract.Commands;
using Saniteau.Compteurs.Domain;
using Saniteau.Compteurs.Domain.Commands;
using System;
using System.Collections.Generic;
using System.Text;

namespace Saniteau.Compteurs.Application.Mappers
{
    public class GetCompteurCommandMapper : IActionMapper<GetCompteurCommand, GetCompteurDomainCommand>
    {
        public GetCompteurDomainCommand Map(GetCompteurCommand action, IActionValidation<GetCompteurCommand> validation)
        {
            return new GetCompteurDomainCommand(IdCompteur.Parse(action.IdCompteur));
        }
    }
}
