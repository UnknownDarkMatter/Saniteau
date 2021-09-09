using Saniteau.Common.Mediator;
using Saniteau.DSP.Contract.Commands;
using Saniteau.DSP.Domain.Commands;
using System;
using System.Collections.Generic;
using System.Text;

namespace Saniteau.DSP.Application.Mappers
{
    public class CréeDelegantCommandMapper : IActionMapper<CréeDelegantCommand, CréeDelegantDomainCommand>
    {
        public CréeDelegantDomainCommand Map(CréeDelegantCommand action, IActionValidation<CréeDelegantCommand> validation)
        {
            return new CréeDelegantDomainCommand(new Domain.ChampLibre(action.Nom), new Domain.ChampLibre(action.Adresse), action.DateContrat);
        }
    }
}
