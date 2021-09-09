using Saniteau.Common.Mediator;
using Saniteau.Compteurs.Contract.Commands;
using Saniteau.Compteurs.Domain;
using Saniteau.Compteurs.Domain.Commands;
using System;
using System.Collections.Generic;
using System.Text;

namespace Saniteau.Compteurs.Application.Mappers
{
    public class GetAllCompteursCommandMapper : IActionMapper<GetAllCompteursCommand, GetAllCompteursDomainCommand>
    {
        public GetAllCompteursDomainCommand Map(GetAllCompteursCommand action, IActionValidation<GetAllCompteursCommand> validation)
        {
            return new GetAllCompteursDomainCommand();
        }
    }
}
