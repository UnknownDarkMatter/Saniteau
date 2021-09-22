using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using Saniteau.Common;
using Saniteau.DSP.Application.Handlers;
using Saniteau.DSP.Contract.Commands;
using Saniteau.DSP.Domain;
using Saniteau.DSP.Repository;
using Saniteau.Models;

namespace Saniteau.Controllers
{
    [Authorize(Policy = Constants.JWT.ApiAccessPolicyName)]
    public class DSPController : Controller
    {
        private readonly RéférentielDelegantOnEfCore _référentielDelegant;
        public DSPController(RéférentielDelegantOnEfCore référentielDelegant)
        {
            _référentielDelegant = référentielDelegant ?? throw new ArgumentNullException(nameof(référentielDelegant));
        }

        public ActionResult ObtientDeleguants()
        {
            var obtientDeleguantCommand = new ObtientDelegantCommand();
            var obtientDelegantCommandHandler = new ObtientDelegantCommandHandler(_référentielDelegant);
            var deleguants = obtientDelegantCommandHandler.Handle(obtientDeleguantCommand);
            var deleguantsAsModel = deleguants.Select(Mappers.DelegantMapper.Map);
            return new JsonResult(deleguantsAsModel.ToArray());
        }


        //public ActionResult CreerFacturations()
        //{
        //    var dateFacturation = Horloge.Instance.GetDateTime();
        //    var créePayeDspCommand = new CréePayeCommand(Horloge.Instance.GetDate(), delegant.IdDelegant); ;
        //    var créePayeDspCommandHandler = new CréePayeCommandHandler(référentielPayeOfDSP, référentielAbonnésOfDSP, référentielFacturationOfDSP, référentielPompesOfDSP, référentielIndexesCompteursOfDSP, référentielCompteursOfDSP);
        //    créePayeDspCommandHandler.Handle(créePayeDspCommand);

        //}

    }
}
