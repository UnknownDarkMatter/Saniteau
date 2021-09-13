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

namespace Saniteau.Controllers
{
    [Authorize(Policy = Constants.JWT.ApiAccessPolicyName)]
    public class PaymentController
    { 
        private readonly PaymentService _paymentService;
        private readonly RéférentielFacturationOnEfCore _référentielFacturation;

        public PaymentController(PaymentService paymentService, RéférentielFacturationOnEfCore référentielFacturation)
        {
            _paymentService = paymentService ?? throw new ArgumentNullException(nameof(paymentService));
            _référentielFacturation = référentielFacturation ?? throw new ArgumentNullException(nameof(référentielFacturation));
        }

        [HttpPost]
        public async Task<ActionResult> EnregistrePayment([FromBody] PaypalOrder paypalOrder)
        {
            var getOrderResult = await _paymentService.CheckIfPaymentIsValid(paypalOrder.OrderId, paypalOrder.IdFacturation);
            if (getOrderResult.IsError)
            {
                return new JsonResult(new RequestResponse(true, getOrderResult.ErrorMessage));
            }
            if (!getOrderResult.Result)
            {
                return new JsonResult(new RequestResponse(true, "Le paiement n'a pas été exécuté"));
            }
            try
            {
                var facturation = _référentielFacturation.GetFacturation(IdFacturation.Parse(paypalOrder.IdFacturation));
                facturation.SetFacturationPayée(true);
                _référentielFacturation.EnregistrerFacturation(facturation);
            }
            catch(Exception ex)
            {
                return new JsonResult(new RequestResponse(true, ex.Message));
            }
            return new JsonResult(new RequestResponse(false, ""));
        }

    }
}
