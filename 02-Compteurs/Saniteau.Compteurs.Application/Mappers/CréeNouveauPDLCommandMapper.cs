using Saniteau.Common.Mediator;
using Saniteau.Compteurs.Contract.Commands;
using Saniteau.Compteurs.Domain;
using Saniteau.Compteurs.Domain.Commands;
using System;
using System.Collections.Generic;
using System.Text;

namespace Saniteau.Compteurs.Application.Mappers
{
    public class CréeNouveauPDLCommandMapper : IActionMapper<CréeNouveauPDLCommand, CréeNouveauPDLDomainCommand>
    {
        public CréeNouveauPDLDomainCommand Map(CréeNouveauPDLCommand action, IActionValidation<CréeNouveauPDLCommand> validation)
        {
            return new CréeNouveauPDLDomainCommand(new ChampLibre(action.NuméroPDL));
        }
    }
}
