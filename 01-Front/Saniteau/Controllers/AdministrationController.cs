using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using Saniteau.Models;
using Saniteau.Database;
using Saniteau.Models.Abonnés;
using Saniteau.Compteurs.Contract.Model;
using Saniteau.Infrastructure.DataContext;
using Saniteau.Compteurs.Contract.Commands;
using Saniteau.Compteurs.Application.Handlers;
using Microsoft.AspNetCore.Authorization;

namespace Saniteau.Controllers
{
    [Authorize(Policy = Constants.JWT.ApiAccessPolicyName)]
    public class AdministrationController : Controller
    {
        private readonly Compteurs.Repository.RéférentielAbonnésOnEfCore _référentielAbonnésOfCompteur;
        private readonly Compteurs.Repository.RéférentielAppairageOnEfCore _référentielAppairageOfCompteur;
        private readonly Compteurs.Repository.RéférentielCompteursOnEfCore _référentielCompteursOfCompteur;
        private readonly Compteurs.Repository.RéférentielIndexesCompteursOnEfCore _référentielIndexesCompteursOfCompteur;
        private readonly Compteurs.Repository.RéférentielPDLOnEfCore _référentielPDLOfCompteur;

        public AdministrationController(Compteurs.Repository.RéférentielAbonnésOnEfCore référentielAbonnésOfCompteur,
            Compteurs.Repository.RéférentielAppairageOnEfCore référentielAppairageOfCompteur, 
            Compteurs.Repository.RéférentielCompteursOnEfCore référentielCompteursOfCompteur,
            Compteurs.Repository.RéférentielIndexesCompteursOnEfCore référentielIndexesCompteursOfCompteur,
            Compteurs.Repository.RéférentielPDLOnEfCore référentielPDLOfCompteur)
        {
            _référentielAbonnésOfCompteur = référentielAbonnésOfCompteur;
            _référentielAppairageOfCompteur = référentielAppairageOfCompteur;
            _référentielCompteursOfCompteur = référentielCompteursOfCompteur;
            _référentielIndexesCompteursOfCompteur = référentielIndexesCompteursOfCompteur;
            _référentielPDLOfCompteur = référentielPDLOfCompteur;
        }


    }
}
