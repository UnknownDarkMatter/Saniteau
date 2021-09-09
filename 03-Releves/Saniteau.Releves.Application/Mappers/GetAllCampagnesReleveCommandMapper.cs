using Saniteau.Common.Mediator;
using Saniteau.Releves.Contract.Commands;
using Saniteau.Releves.Domain;
using Saniteau.Releves.Domain.Commands;
using System;
using System.Collections.Generic;
using System.Text;
namespace Saniteau.Releves.Application.Mappers
{
    public class GetAllCampagnesReleveCommandMapper : IActionMapper<GetAllCampagnesReleveCommand, GetAllCampagnesReleveDomainCommand>
    {
        public GetAllCampagnesReleveDomainCommand Map(GetAllCampagnesReleveCommand action, IActionValidation<GetAllCampagnesReleveCommand> validation)
        {
            return new GetAllCampagnesReleveDomainCommand();
        }
    }
}
