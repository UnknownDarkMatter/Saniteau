using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using Saniteau.Models;
using Saniteau.Releves.Application.Handlers;
using Saniteau.Releves.Contract.Commands;
using Saniteau.Releves.Repository;

namespace Saniteau.Controllers
{
    [Authorize(Policy = Constants.JWT.ApiAccessPolicyName)]
    public class RelevesController : Controller
    {
        private readonly RéférentielCompteursOnEfCore _référentielCompteurs;
        private readonly RéférentielIndexesCompteursOnEfCore _référentielIndexesCompteurs;
        private readonly RéférentielPompesOnEfCore _référentielPompes;
        public RelevesController(RéférentielCompteursOnEfCore référentielCompteurs,
            RéférentielIndexesCompteursOnEfCore référentielIndexesCompteurs,
            RéférentielPompesOnEfCore référentielPompes)
        {
            if (référentielCompteurs is null) { throw new ArgumentNullException(nameof(référentielCompteurs)); }
            if (référentielIndexesCompteurs is null) { throw new ArgumentNullException(nameof(référentielIndexesCompteurs)); }
            if (référentielPompes is null) { throw new ArgumentNullException(nameof(référentielPompes)); }

            _référentielCompteurs = référentielCompteurs;
            _référentielIndexesCompteurs = référentielIndexesCompteurs;
            _référentielPompes = référentielPompes;
        }

        [HttpGet]
        public ActionResult LancerReleve()
        {
            var incrémenteCompteursCommand = new IncrémenteCompteursCommand();
            var incrémenteCompteursCommandHandler = new IncrémenteCompteursCommandHandler(_référentielCompteurs, _référentielIndexesCompteurs, _référentielPompes);
            incrémenteCompteursCommandHandler.Handle(incrémenteCompteursCommand);

            return RedirectToAction(nameof(ObtenirCampagnesReleve));
        }

        [HttpGet]
        public ActionResult ObtenirCampagnesReleve()
        {
            var getAllCampagnesReleveCommand = new GetAllCampagnesReleveCommand();
            var getAllCampagnesReleveCommandHandler = new GetAllCampagnesReleveCommandHandler(_référentielPompes, _référentielIndexesCompteurs, _référentielCompteurs);
            var campagnesReleve = getAllCampagnesReleveCommandHandler.Handle(getAllCampagnesReleveCommand);
            return new JsonResult(campagnesReleve);
        }

    }
}
