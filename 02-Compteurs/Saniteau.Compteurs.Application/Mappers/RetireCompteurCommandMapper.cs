using Saniteau.Common.Mediator;
using Saniteau.Compteurs.Contract.Commands;
using Saniteau.Compteurs.Domain;
using Saniteau.Compteurs.Domain.Commands;
using System;
using System.Collections.Generic;
using System.Text;

namespace Saniteau.Compteurs.Application.Mappers
{
    public class RetireCompteurCommandMapper : IActionMapper<RetireCompteurCommand, RetireCompteurDomainCommand>
    {
        public RetireCompteurDomainCommand Map(RetireCompteurCommand action, IActionValidation<RetireCompteurCommand> validation)
        {
            var compteur = CompteurMapper.Map(action.Compteur);
            return new RetireCompteurDomainCommand(compteur);
        }
    }
}
