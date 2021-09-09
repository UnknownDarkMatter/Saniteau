using Saniteau.Common.Mediator;
using Saniteau.Compteurs.Contract.Commands;
using Saniteau.Compteurs.Domain;
using Saniteau.Compteurs.Domain.Commands;
using System;
using System.Collections.Generic;
using System.Text;

namespace Saniteau.Compteurs.Application.Mappers
{
    public class DeleteAbonnéCommandMapper : IActionMapper<DeleteAbonnéCommand, DeleteAbonnéDomainCommand>
    {
        public DeleteAbonnéDomainCommand Map(DeleteAbonnéCommand action, IActionValidation<DeleteAbonnéCommand> validation)
        {
            return new DeleteAbonnéDomainCommand(IdAbonné.Parse(action.IdAbonné));
        }
    }
}
