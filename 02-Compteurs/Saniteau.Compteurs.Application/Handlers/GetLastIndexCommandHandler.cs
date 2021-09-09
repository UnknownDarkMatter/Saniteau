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
    public class GetLastIndexCommandHandler : ActionHandlerBase<GetLastIndexCommand, GetLastIndexDomainCommand, Contract.Model.IndexCompteur>
    {
        private readonly RéférentielIndexesCompteurs _référentielIndexesCompteurs;
        public GetLastIndexCommandHandler(RéférentielIndexesCompteurs référentielIndexesCompteurs) : base (new GetLastIndexCommandMapper())
        {
            if(référentielIndexesCompteurs is null) { throw new ArgumentNullException(nameof(référentielIndexesCompteurs)); }

            _référentielIndexesCompteurs = référentielIndexesCompteurs;
        }

        protected override Contract.Model.IndexCompteur Handle(GetLastIndexDomainCommand action)
        {
            var index = action.GetLastIndex(_référentielIndexesCompteurs);
            return IndexCompteurMapper.Map(index);
        }
    }
}
