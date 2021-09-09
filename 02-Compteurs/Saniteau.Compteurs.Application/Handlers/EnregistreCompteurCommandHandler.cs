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
    public class EnregistreCompteurCommandHandler : ActionHandlerBase<EnregistreCompteurCommand, EnregistreCompteurDomainCommand, Contract.Model.Compteur>
    {
        private readonly RéférentielCompteurs _référentielCompteurs;

        public EnregistreCompteurCommandHandler(RéférentielCompteurs référentielCompteurs) : base(new EnregistreCompteurCommandMapper())
        {
            if (référentielCompteurs is null) { throw new ArgumentNullException(nameof(référentielCompteurs)); }

            _référentielCompteurs = référentielCompteurs;
        }

        protected override Contract.Model.Compteur Handle(EnregistreCompteurDomainCommand action)
        {
            var compteur = action.EnregistreCompteur(_référentielCompteurs);
            return CompteurMapper.Map(compteur);
        }
    }
}
