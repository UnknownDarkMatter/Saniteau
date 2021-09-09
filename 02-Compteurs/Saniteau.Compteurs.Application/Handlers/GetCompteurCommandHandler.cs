using Saniteau.Common.Mediator;
using Saniteau.Compteurs.Application.Mappers;
using Saniteau.Compteurs.Contract.Commands;
using Saniteau.Compteurs.Contract.Model;
using Saniteau.Compteurs.Domain;
using Saniteau.Compteurs.Domain.Commands;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Saniteau.Compteurs.Application.Handlers
{
    public class GetCompteurCommandHandler : ActionHandlerBase<GetCompteurCommand, GetCompteurDomainCommand, Contract.Model.Compteur>
    {
        private readonly RéférentielCompteurs _référentielCompteurs;
        public GetCompteurCommandHandler(RéférentielCompteurs référentielCompteurs) : base (new GetCompteurCommandMapper())
        {
            if (référentielCompteurs is null) { throw new ArgumentNullException(nameof(référentielCompteurs)); }

            _référentielCompteurs = référentielCompteurs;
        }

        protected override Contract.Model.Compteur Handle(GetCompteurDomainCommand action)
        {
            var compteur =  action.GetCompteur(_référentielCompteurs);
            return CompteurMapper.Map(compteur);
        }
    }
}
