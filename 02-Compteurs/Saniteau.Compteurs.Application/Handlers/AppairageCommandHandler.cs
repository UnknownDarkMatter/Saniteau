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
    public class AppairageCommandHandler : ActionHandlerBase<AppairageCommand, AppairageDomainCommand, Contract.Model.AppairageCompteur>
    {
        private readonly RéférentielCompteurs _référentielCompteurs;
        private readonly RéférentielPDL _référentielPDL;
        private readonly RéférentielAbonnés _référentielAbonnés;
        private readonly RéférentielAppairage _référentielAppairage;

        public AppairageCommandHandler(RéférentielCompteurs référentielCompteurs, RéférentielPDL référentielPDL, 
            RéférentielAbonnés référentielAbonnés, RéférentielAppairage référentielAppairage) : base(new AppairageCommandMapper())
        {
            if (référentielCompteurs is null) { throw new ArgumentNullException(nameof(référentielCompteurs)); }
            if (référentielPDL is null) { throw new ArgumentNullException(nameof(référentielPDL)); }
            if (référentielAbonnés is null) { throw new ArgumentNullException(nameof(référentielAbonnés)); }
            if (référentielAppairage is null) { throw new ArgumentNullException(nameof(référentielAppairage)); }

            _référentielCompteurs = référentielCompteurs;
            _référentielPDL = référentielPDL;
            _référentielAbonnés = référentielAbonnés;
            _référentielAppairage = référentielAppairage;
        }

        protected override Contract.Model.AppairageCompteur Handle(AppairageDomainCommand action)
        {
            var apparairageCompteur = action.AppairageCompteur(_référentielCompteurs, _référentielPDL, _référentielAbonnés, _référentielAppairage);
            return AppairageMapper.Map(apparairageCompteur, action.IdAdresse);
        }


    }
}
