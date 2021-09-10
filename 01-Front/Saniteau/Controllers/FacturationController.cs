using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using Saniteau.Models;
using Saniteau.Facturation.Repository;
using Saniteau.Facturation.Domain;
using Saniteau.Facturation.Application.Handlers;
using Saniteau.Facturation.Contract.Commands;
using Saniteau.Common;
using Saniteau.PdfRendering;

namespace Saniteau.Controllers
{
    [Authorize(Policy = Constants.JWT.ApiAccessPolicyName)] 
    public class FacturationController : Controller
    {
        private readonly RéférentielAbonnésOnEfCore _référentielAbonnés;
        private readonly RéférentielAppairageOnEfCore _référentielAppairage;
        private readonly RéférentielCompteursOnEfCore _référentielCompteurs;
        private readonly RéférentielFacturationOnEfCore _référentielFacturation;
        private readonly RéférentielIndexesCompteursOnEfCore _référentielIndexesCompteurs;
        private readonly RéférentielPDLOnEfCore _référentielPDL;

        public FacturationController(RéférentielAbonnésOnEfCore référentielAbonnés, RéférentielAppairageOnEfCore référentielAppairage, RéférentielCompteursOnEfCore référentielCompteurs,
            RéférentielFacturationOnEfCore référentielFacturation, RéférentielIndexesCompteursOnEfCore référentielIndexesCompteurs, RéférentielPDLOnEfCore référentielPDL) {
            if (référentielAbonnés is null) { throw new ArgumentNullException(nameof(référentielAbonnés)); }
            if (référentielAppairage is null) { throw new ArgumentNullException(nameof(référentielAppairage)); }
            if (référentielCompteurs is null) { throw new ArgumentNullException(nameof(référentielAbonnés)); }
            if (référentielFacturation is null) { throw new ArgumentNullException(nameof(référentielFacturation)); }
            if (référentielIndexesCompteurs is null) { throw new ArgumentNullException(nameof(référentielIndexesCompteurs)); }
            if (référentielPDL is null) { throw new ArgumentNullException(nameof(référentielPDL)); }

            _référentielAbonnés = référentielAbonnés;
            _référentielAppairage = référentielAppairage;
            _référentielCompteurs = référentielCompteurs;
            _référentielFacturation = référentielFacturation;
            _référentielIndexesCompteurs = référentielIndexesCompteurs;
            _référentielPDL = référentielPDL;
        }

        [HttpGet]
        public ActionResult CreerFacturations()
        {
            var dateFacturation = Horloge.Instance.GetDateTime();
            var créeFacturationCommand = new CréeFacturationCommand(dateFacturation);
            var créeFacturationCommandHandler = new CréeFacturationCommandHandler(_référentielAbonnés, _référentielAppairage, _référentielIndexesCompteurs, _référentielFacturation);
            créeFacturationCommandHandler.Handle(créeFacturationCommand);

            return RedirectToAction(nameof(ObtenirFacturations));
        }


        [HttpGet]
        public ActionResult ObtenirFacturations()
        {
            var getAllFacturationsCommand = new GetAllFacturationsCommand();
            var getAllFacturationsCommandHandler = new GetAllFacturationsCommandHandler(_référentielFacturation, _référentielAbonnés);
            var facturations = getAllFacturationsCommandHandler.Handle(getAllFacturationsCommand);
            var clientFacturations = facturations.Select(Mappers.FacturationMapper.Map).ToArray();
            return new JsonResult(clientFacturations);
        }


        [HttpGet]
        public ActionResult ObtenirFacture(int idFacturation, int idAbonne)
        {
            var getFacturationCommand = new GetFacturationCommand(idFacturation, idAbonne);
            var getFacturationCommandHandler = new GetFacturationCommandHandler(_référentielFacturation, _référentielAbonnés);
            var facturationAsContract = getFacturationCommandHandler.Handle(getFacturationCommand);
            var facturation = Mappers.FacturationMapper.Map(facturationAsContract);
            var pdfGenerator = new PdfFactureGenerator();
            var bytes = pdfGenerator.GeneratePdfFacture(facturation);
            return File(bytes, "application/pdf", $"Facture-eau-{facturation.Abonne.Nom}-{facturation.DateFacturation.ToString("yyyy-MM-dd")}.pdf");
        }

    }
}
