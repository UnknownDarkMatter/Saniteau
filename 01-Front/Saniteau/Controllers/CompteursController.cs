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
using Saniteau.Compteurs.Repository;
using Saniteau.Compteurs.Contract.Commands;
using Saniteau.Compteurs.Application.Handlers;
using Microsoft.AspNetCore.Authorization;
using Saniteau.Models.Compteurs;

namespace Saniteau.Controllers
{
    [Authorize(Policy = Constants.JWT.ApiAccessPolicyName)]
    public class CompteursController : Controller
    {
        private readonly RéférentielAbonnésOnEfCore _référentielAbonnés;
        private readonly RéférentielAppairageOnEfCore _référentielAppairage;
        private readonly RéférentielCompteursOnEfCore _référentielCompteurs;
        private readonly RéférentielIndexesCompteursOnEfCore _référentielIndexesCompteurs;
        private readonly RéférentielPDLOnEfCore _référentielPDL;

        public CompteursController(RéférentielAbonnésOnEfCore référentielAbonnés,
            RéférentielAppairageOnEfCore référentielAppairage, RéférentielCompteursOnEfCore référentielCompteurs,
            RéférentielIndexesCompteursOnEfCore référentielIndexesCompteurs, RéférentielPDLOnEfCore référentielPDL)
        {
            if (référentielAbonnés is null) { throw new ArgumentNullException(nameof(référentielAbonnés)); }
            if (référentielAppairage is null) { throw new ArgumentNullException(nameof(référentielAppairage)); }
            if (référentielCompteurs is null) { throw new ArgumentNullException(nameof(référentielCompteurs)); }
            if (référentielIndexesCompteurs is null) { throw new ArgumentNullException(nameof(référentielIndexesCompteurs)); }
            if (référentielPDL is null) { throw new ArgumentNullException(nameof(référentielPDL)); }

            _référentielAbonnés = référentielAbonnés;
            _référentielAppairage = référentielAppairage;
            _référentielCompteurs = référentielCompteurs;
            _référentielIndexesCompteurs = référentielIndexesCompteurs;
            _référentielPDL = référentielPDL;
        }

        [HttpPost]
        public ActionResult EnregistreAbonne([FromBody] EnregistreAbonneRequest enregistreAbonneRequest)
        {
            var enregistreAdresseCommand = new EnregistreAdresseCommand(enregistreAbonneRequest.IdAdresse, enregistreAbonneRequest.Adresse, enregistreAbonneRequest.Ville, enregistreAbonneRequest.CodePostal);
            var enregistreAdresseCommandHandler = new EnregistreAdresseCommandHandler(_référentielAbonnés);
            var adresse = enregistreAdresseCommandHandler.Handle(enregistreAdresseCommand);

            var tarification = Mappers.TarificationMapper.MapToCompteurs(enregistreAbonneRequest.Tarification);
            var enregistreAbonnéCommand = new EnregistreAbonnéCommand(enregistreAbonneRequest.IdAbonne, adresse.IdAdresse, enregistreAbonneRequest.Nom, enregistreAbonneRequest.Prenom, tarification);
            var enregistreAbonnéCommandHandler = new EnregistreAbonnéCommandHandler(_référentielAbonnés);
            var abonné = enregistreAbonnéCommandHandler.Handle(enregistreAbonnéCommand);

            return new JsonResult(abonné);
        }

        [HttpPost]
        public ActionResult DeleteAbonne([FromBody] DeleteAbonneRequest deleteAbonneRequest)
        {
            var deleteAbonnéCommand = new DeleteAbonnéCommand(deleteAbonneRequest.IdAbonne);
            var deleteAbonnéCommandHandler = new DeleteAbonnéCommandHandler(_référentielAbonnés);
            deleteAbonnéCommandHandler.Handle(deleteAbonnéCommand);

            return new EmptyResult();
        }

        [HttpGet]
        public ActionResult GetAllAbonnes(bool filtrerAbonnesAvecCompteurs)
        {
            var getAllAbonnésCommand = new GetAllAbonnésCommand(filtrerAbonnesAvecCompteurs);
            var getAllAbonnésCommandHandler = new GetAllAbonnésCommandHandler(_référentielAbonnés);
            var abonnés = getAllAbonnésCommandHandler.Handle(getAllAbonnésCommand);
            return new JsonResult(abonnés.ToArray());
        }

        [HttpGet]
        public ActionResult GetAbonne(int idAbonne)
        {
            var getAbonnéCommand = new GetAbonnéCommand(idAbonne);
            var getAbonnéCommandHandler = new GetAbonnéCommandHandler(_référentielAbonnés);
            var abonné = getAbonnéCommandHandler.Handle(getAbonnéCommand);
            return new JsonResult(abonné);
        }


        [HttpPost]
        public ActionResult EnregistreCompteur([FromBody] EnregistreCompteurRequest enregistreCompteurRequest)
        {
            try
            {
                var enregistreCompteurCommand = new EnregistreCompteurCommand(enregistreCompteurRequest.IdCompteur, enregistreCompteurRequest.NomCompteur);
                var enregistreCompteurCommandHandler = new EnregistreCompteurCommandHandler(_référentielCompteurs);
                var compteur = enregistreCompteurCommandHandler.Handle(enregistreCompteurCommand);
                var result = new RequestResponse(false, "");
                return new JsonResult(result);
            }
            catch (Exception ex)
            {
                var result = new RequestResponse(true, ex.Message);
                return new JsonResult(result);
            }
        }

        [HttpGet]
        public ActionResult GetAllCompteurs()
        {
            var getAllCompteursCommand = new GetAllCompteursCommand();
            var getAllCompteursCommandHandler = new GetAllCompteursCommandHandler(_référentielCompteurs);
            var compteurs = getAllCompteursCommandHandler.Handle(getAllCompteursCommand);
            compteurs = compteurs.Where(m => m.NuméroCompteur != Constants.Database.NomCompteurPompePrincipale).ToList();
            var clientCompteurs = compteurs.Select(Mappers.CompteurMapper.Map).ToList();
            return new JsonResult(clientCompteurs.ToArray());
        }


        [HttpPost]
        public ActionResult PoseCompteur([FromBody] Models.Compteurs.Compteur compteur)
        {
            try
            {
                var poseCompteurCommand = new PoseCompteurCommand(Mappers.CompteurMapper.Map(compteur));
                var poseCompteurCommandHandler = new PoseCompteurCommandHandler(_référentielCompteurs, _référentielIndexesCompteurs);
                poseCompteurCommandHandler.Handle(poseCompteurCommand);

                var result = new RequestResponse(false, "");
                return new JsonResult(result);
            }
            catch (Exception ex)
            {
                var result = new RequestResponse(true, ex.Message);
                return new JsonResult(result);
            }
        }

        [HttpPost]
        public ActionResult RetireCompteur([FromBody] Models.Compteurs.Compteur compteur)
        {
            try
            {
                var retireCompteurCommand = new RetireCompteurCommand(Mappers.CompteurMapper.Map(compteur));
                var retireCompteurCommandHandler = new RetireCompteurCommandHandler(_référentielCompteurs, _référentielIndexesCompteurs, 
                    _référentielAppairage, _référentielPDL);
                retireCompteurCommandHandler.Handle(retireCompteurCommand);

                var result = new RequestResponse(false, "");
                return new JsonResult(result);
            }
            catch (Exception ex)
            {
                var result = new RequestResponse(true, ex.Message);
                return new JsonResult(result);
            }
        }


        [HttpPost]
        public ActionResult GetCompteur([FromBody] int idCompteur)
        {
            var getCompteurCommand = new GetCompteurCommand(idCompteur);
            var getCompteurCommandHandler = new GetCompteurCommandHandler(_référentielCompteurs);
            var compteur = getCompteurCommandHandler.Handle(getCompteurCommand);
            var result = Mappers.CompteurMapper.Map(compteur);
            return new JsonResult(result);
        }


        [HttpPost]
        public ActionResult AssocieAdressePDL([FromBody] AdressePDL adressePDL)
        {
            var associeAdressePDLCommand = new AssocieAdressePDLCommand(adressePDL.IdAdresse, adressePDL.IdCompteur);
            var associeAdressePDLCommandHandler = new AssocieAdressePDLCommandHandler(_référentielCompteurs, _référentielAppairage, _référentielPDL);
            var compteur = associeAdressePDLCommandHandler.Handle(associeAdressePDLCommand);
            var result = Mappers.CompteurMapper.Map(compteur);
            return new JsonResult(result);
        }


        [HttpPost]
        public ActionResult DissocieAdressePDL([FromBody] AdressePDL adressePDL)
        {
            var dissocieAdressePDLCommand = new DissocieAdressePDLCommand(adressePDL.IdAdresse, adressePDL.IdCompteur);
            var dissocieAdressePDLCommandHandler = new DissocieAdressePDLCommandHandler(_référentielCompteurs, _référentielAppairage, _référentielPDL);
            var compteur = dissocieAdressePDLCommandHandler.Handle(dissocieAdressePDLCommand);
            var result = Mappers.CompteurMapper.Map(compteur);
            return new JsonResult(result);
        }



        [HttpPost]
        public ActionResult AppaireCompteur([FromBody] Models.Compteurs.Compteur compteur)
        {
            try
            {
                var getAbonnéCommand = new GetAbonnéCommand(compteur.IdAbonne);
                var getAbonnéCommandHandler = new GetAbonnéCommandHandler(_référentielAbonnés);
                var abonné = getAbonnéCommandHandler.Handle(getAbonnéCommand);

                var appairageCommand = new AppairageCommand(0, abonné.IdAdresse, compteur.IdCompteur, compteur.PDL.IdPDL);
                var appairageCommandHandler = new AppairageCommandHandler(_référentielCompteurs, _référentielPDL, _référentielAbonnés, _référentielAppairage);
                var appairage = appairageCommandHandler.Handle(appairageCommand);

                var result = new RequestResponse(false, "");
                return new JsonResult(result);
            }
            catch (Exception ex)
            {
                var result = new RequestResponse(true, ex.Message);
                return new JsonResult(result);
            }
        }


        [HttpPost]
        public ActionResult DesappaireCompteur([FromBody] Models.Compteurs.Compteur compteur)
        {
            try
            {
                var getAbonnéCommand = new GetAbonnéCommand(compteur.IdAbonne);
                var getAbonnéCommandHandler = new GetAbonnéCommandHandler(_référentielAbonnés);
                var abonné = getAbonnéCommandHandler.Handle(getAbonnéCommand);

                var appairageCommand = new AppairageCommand(0, abonné.IdAdresse, compteur.IdCompteur, compteur.PDL.IdPDL);
                var appairageCommandHandler = new AppairageCommandHandler(_référentielCompteurs, _référentielPDL, _référentielAbonnés, _référentielAppairage);
                var appairage = appairageCommandHandler.Handle(appairageCommand);

                var desappairageCommand = new DesappairageCommand(appairage.IdAppairageCompteur);
                var desappairageCommandHandler = new DesappairageCommandHandler( _référentielAppairage);
                desappairageCommandHandler.Handle(desappairageCommand);

                var result = new RequestResponse(false, "");
                return new JsonResult(result);
            }
            catch (Exception ex)
            {
                var result = new RequestResponse(true, ex.Message);
                return new JsonResult(result);
            }
        }

        [HttpPost]
        public ActionResult SupprimeCompteur([FromBody] SupprimeCompteurRequest supprimeCompteurRequest)
        {
            try
            {
                var deleteCompteurCommand = new DeleteCompteurCommand(supprimeCompteurRequest.IdCompteur);
                var deleteCompteurCommandHandler = new DeleteCompteurCommandHandler(_référentielCompteurs, _référentielAppairage, _référentielPDL, _référentielIndexesCompteurs);
                deleteCompteurCommandHandler.Handle(deleteCompteurCommand);
                var result = new RequestResponse(false, "");
                return new JsonResult(result);
            }
            catch (Exception ex)
            {
                var result = new RequestResponse(true, ex.Message);
                return new JsonResult(result);
            }
        }

    }
}
