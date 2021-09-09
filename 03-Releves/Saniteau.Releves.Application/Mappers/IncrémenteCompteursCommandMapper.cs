using Saniteau.Common.Mediator;
using Saniteau.Releves.Contract.Commands;
using Saniteau.Releves.Domain.Commands;
using System;
using System.Collections.Generic;
using System.Text;

namespace Saniteau.Releves.Application.Mappers
{
    public class IncrémenteCompteursCommandMapper : IActionMapper<IncrémenteCompteursCommand, IncrémenteCompteursDomainCommand>
    {
        public IncrémenteCompteursDomainCommand Map(IncrémenteCompteursCommand action, IActionValidation<IncrémenteCompteursCommand> validation)
        {
            return new IncrémenteCompteursDomainCommand();
        }
    }
}
