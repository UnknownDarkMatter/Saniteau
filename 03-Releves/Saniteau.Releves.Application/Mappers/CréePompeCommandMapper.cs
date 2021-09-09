using Saniteau.Common.Mediator;
using Saniteau.Releves.Contract.Commands;
using Saniteau.Releves.Domain;
using Saniteau.Releves.Domain.Commands;
using System;
using System.Collections.Generic;
using System.Text;

namespace Saniteau.Releves.Application.Mappers
{
    public class CréePompeCommandMapper : IActionMapper<CréePompeCommand, CréePompeDomainCommand>
    {
        public CréePompeDomainCommand Map(CréePompeCommand action, IActionValidation<CréePompeCommand> validation)
        {
            return new CréePompeDomainCommand(IdCompteur.Parse(action.IdCompteur), new ChampLibre(action.NuméroPompe));
        }
    }
}
