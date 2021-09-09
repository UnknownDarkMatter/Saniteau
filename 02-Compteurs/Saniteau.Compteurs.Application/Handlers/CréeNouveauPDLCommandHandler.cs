using Saniteau.Common.Mediator;
using Saniteau.Compteurs.Application.Mappers;
using Saniteau.Compteurs.Contract.Commands;
using Saniteau.Compteurs.Contract.Model;
using Saniteau.Compteurs.Domain;
using Saniteau.Compteurs.Domain.Commands;
using System;
using System.Collections.Generic;
using System.Text;

namespace Saniteau.Compteurs.Application.Handlers
{
    public class CréeNouveauPDLCommandHandler : ActionHandlerBase<CréeNouveauPDLCommand, CréeNouveauPDLDomainCommand, Contract.Model.PDL>
    {
        private readonly RéférentielPDL _référentielPDL;

        public CréeNouveauPDLCommandHandler(RéférentielPDL référentielPDL) : base(new CréeNouveauPDLCommandMapper())
        {
            if(référentielPDL is null) { throw new ArgumentNullException(nameof(référentielPDL)); }

            _référentielPDL = référentielPDL;
        }

        protected override Contract.Model.PDL Handle(CréeNouveauPDLDomainCommand action)
        {
            var pdl = action.CréeNouveauPDL(_référentielPDL);
            return PDLMapper.Map(pdl);
        }
    }
}
