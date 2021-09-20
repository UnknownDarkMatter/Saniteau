using Saniteau.Common.Mediator;
using Saniteau.Compteurs.Contract.Commands;
using Saniteau.Compteurs.Domain;
using Saniteau.Compteurs.Domain.Commands;
using System;
using System.Collections.Generic;
using System.Text;

namespace Saniteau.Compteurs.Application.Mappers
{
    public class DeleteCompteurCommandMapper : IActionMapper<DeleteCompteurCommand, DeleteCompteurDomainCommand>
    {
        public DeleteCompteurDomainCommand Map(DeleteCompteurCommand action, IActionValidation<DeleteCompteurCommand> validation)
        {
            return new DeleteCompteurDomainCommand(IdCompteur.Parse(action.IdCompteur));
        }
    }
}
