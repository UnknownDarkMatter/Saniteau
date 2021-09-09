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
    public class DissocieAdressePDLCommandHandler : ActionHandlerBase<DissocieAdressePDLCommand, DissocieAdressePDLDomainCommand, Contract.Model.Compteur>
    {
        private readonly RéférentielCompteurs _référentielCompteurs;
        private readonly RéférentielAppairage _référentielAppairage;
        private readonly RéférentielPDL _référentielPDL;
        public DissocieAdressePDLCommandHandler(RéférentielCompteurs référentielCompteurs, RéférentielAppairage référentielAppairage, RéférentielPDL référentielPDL) : base(new DissocieAdressePDLCommandMapper())
        {
            if (référentielCompteurs is null) { throw new ArgumentNullException(nameof(référentielCompteurs)); }
            if (référentielAppairage is null) { throw new ArgumentNullException(nameof(référentielAppairage)); }
            if (référentielPDL is null) { throw new ArgumentNullException(nameof(référentielPDL)); }

            _référentielCompteurs = référentielCompteurs;
            _référentielAppairage = référentielAppairage;
            _référentielPDL = référentielPDL;
        }

        protected override Contract.Model.Compteur Handle(DissocieAdressePDLDomainCommand action)
        {
            var compteur =  action.DissocieAdressePDL(_référentielCompteurs, _référentielAppairage, _référentielPDL);
            return CompteurMapper.Map(compteur);
        }
    }
}
