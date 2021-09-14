using Saniteau.Facturation.Domain;
using Saniteau.Facturation.Payment.Services;
using Saniteau.Models;
using Saniteau.Models.Payment;
using Saniteau.Facturation.Repository;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Saniteau.Facturation.Contract.Commands;
using Saniteau.Facturation.Application.Handlers;

namespace Saniteau.Controllers
{
    [Authorize(Policy = Constants.JWT.ApiAccessPolicyName)]
    public class PaymentController
    { 
        private readonly PaymentService _paymentService;
        private readonly RéférentielFacturationOnEfCore _référentielFacturation;
        private readonly RéférentielPaiementOnEfCore _référentielPaiement;

        public PaymentController(PaymentService paymentService, RéférentielFacturationOnEfCore référentielFacturation,
            RéférentielPaiementOnEfCore référentielPaiement)
        {
            _paymentService = paymentService ?? throw new ArgumentNullException(nameof(paymentService));
            _référentielFacturation = référentielFacturation ?? throw new ArgumentNullException(nameof(référentielFacturation));
            _référentielPaiement = référentielPaiement ?? throw new ArgumentNullException(nameof(référentielPaiement));
        }

        [HttpPost]
        public async Task<ActionResult> EnregistrePayment([FromBody] PaypalOrder paypalOrder)
        {
            var getAllFacturationsCommand = new EnregistrePaymentCommand(paypalOrder.OrderId, paypalOrder.IdFacturation);
            var getAllFacturationsCommandHandler = new EnregistrePaymentCommandHandler(_paymentService, _référentielFacturation, _référentielPaiement);
            var getOrderResult = await getAllFacturationsCommandHandler.HandleAsync(getAllFacturationsCommand);
            if (getOrderResult.IsError)
            {
                return new JsonResult(new RequestResponse(true, getOrderResult.ErrorMessage));
            }
            if (!getOrderResult.Result)
            {
                return new JsonResult(new RequestResponse(true, "Le paiement n'a pas été exécuté par paypal"));
            }

            return new JsonResult(new RequestResponse(false, ""));
        }

    }
}
