using Saniteau.Common.Mediator;
using Saniteau.Facturation.Application.Mappers;
using Saniteau.Facturation.Contract.Commands;
using Saniteau.Facturation.Domain;
using Saniteau.Facturation.Domain.Commands;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Saniteau.Facturation.Application.Handlers
{
    public class CréeFacturationCommandHandler : ActionHandlerBase<CréeFacturationCommand, CréeFacturationDomainCommand, List<Contract.Model.Facturation>>
    {
        private RéférentielAbonnés _référentielAbonnés;
        private RéférentielAppairage _référentielAppairage;
        private RéférentielIndexesCompteurs _référentielIndexesCompteurs;
        private RéférentielFacturation _référentielFacturation;

        public CréeFacturationCommandHandler(RéférentielAbonnés référentielAbonnés, RéférentielAppairage référentielAppairage,
            RéférentielIndexesCompteurs référentielIndexesCompteurs, RéférentielFacturation référentielFacturation) : base(new CréeFacturationCommandMapper())
        {
            if (référentielAbonnés is null) { throw new ArgumentNullException(nameof(référentielAbonnés)); }
            if (référentielAppairage is null) { throw new ArgumentNullException(nameof(référentielAppairage)); }
            if (référentielIndexesCompteurs is null) { throw new ArgumentNullException(nameof(référentielIndexesCompteurs)); }
            if (référentielFacturation is null) { throw new ArgumentNullException(nameof(référentielFacturation)); }

            _référentielAbonnés = référentielAbonnés;
            _référentielAppairage = référentielAppairage;
            _référentielIndexesCompteurs = référentielIndexesCompteurs;
            _référentielFacturation = référentielFacturation;
        }

        protected override List<Contract.Model.Facturation> Handle(CréeFacturationDomainCommand action)
        {
            var facturations = new List<Contract.Model.Facturation>();

            var idCampagneFacturation = _référentielFacturation.GetDerniereCampagneFacturationId() + 1;

            bool facturationIsNull = true;
            var calculatedFacturations = new List<Domain.Facturation>();
            foreach(var abonné in _référentielAbonnés.GetAllAbonnés())
            {
                var facturation = Domain.Facturation.CalculeFacturation(idCampagneFacturation, action.DateFacturation, abonné.IdAbonné,
                    _référentielAbonnés, _référentielAppairage, _référentielIndexesCompteurs, _référentielFacturation);
                calculatedFacturations.Add(facturation);
                var montantFacturation = facturation.LignesFacturation.Sum(m => (decimal)m.MontantEuros);
                facturationIsNull = facturationIsNull && montantFacturation == 0;
            }

            if (facturationIsNull)
            {
                return facturations;
            }

            var allAdresses = _référentielAbonnés.GetAllAddresses();

            for (int i=0;i< calculatedFacturations.Count;i++)
            {
                var facturation = calculatedFacturations[i];
                facturation = _référentielFacturation.EnregistrerFacturation(facturation);
                var adresse = allAdresses.FirstOrDefault(m => m.IdAdresse == facturation.Abonné.IdAdresse);
                facturations.Add(FacturationMapper.Map(facturation, adresse));
            }

            return facturations;
        }
    }
}
