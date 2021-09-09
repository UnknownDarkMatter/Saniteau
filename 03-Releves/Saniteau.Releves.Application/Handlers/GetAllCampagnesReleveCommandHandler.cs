using Saniteau.Common.Mediator;
using Saniteau.Releves.Application.Mappers;
using Saniteau.Releves.Contract.Commands;
using Saniteau.Releves.Contract.Model;
using Saniteau.Releves.Domain;
using Saniteau.Releves.Domain.Commands;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;


namespace Saniteau.Releves.Application.Handlers
{
    public class GetAllCampagnesReleveCommandHandler : ActionHandlerBase<GetAllCampagnesReleveCommand, GetAllCampagnesReleveDomainCommand, List<Contract.Model.CampagneReleve>>
    {
        private readonly RéférentielPompes _référentielPompes;
        private readonly RéférentielIndexesCompteurs _référentielIndexesCompteurs;
        private readonly RéférentielCompteurs _référentielCompteurs;
        public GetAllCampagnesReleveCommandHandler(RéférentielPompes référentielPompes, RéférentielIndexesCompteurs référentielIndexesCompteurs, RéférentielCompteurs référentielCompteurs) 
            : base (new GetAllCampagnesReleveCommandMapper())
        {
            if (référentielPompes is null) { throw new ArgumentNullException(nameof(référentielPompes)); }
            if (référentielIndexesCompteurs is null) { throw new ArgumentNullException(nameof(référentielIndexesCompteurs)); }
            if (référentielCompteurs is null) { throw new ArgumentNullException(nameof(référentielCompteurs)); }

            _référentielPompes = référentielPompes;
            _référentielIndexesCompteurs = référentielIndexesCompteurs;
            _référentielCompteurs = référentielCompteurs;
        }

        protected override List<Contract.Model.CampagneReleve> Handle(GetAllCampagnesReleveDomainCommand action)
        {
            var campagnes = action.GetAllCampagnesReleve(_référentielPompes, _référentielIndexesCompteurs, _référentielCompteurs);
            return campagnes.Select(CampagneReleveMapper.Map).ToList();
        }
    }
}
