using Saniteau.Common;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Saniteau.Facturation.Domain.Commands
{
    public class EnregistrePaymentDomainCommand
    {
        private readonly string _orderId;
        private readonly int _idFacturation;

        public EnregistrePaymentDomainCommand(string orderId, int idFacturation)
        {
            _orderId = orderId ?? throw new ArgumentNullException(nameof(orderId));
            _idFacturation = idFacturation;
        }

        public async Task<ApiResponse<bool>> EnregistrePayment(IPaymentService paymentService, RéférentielFacturation référentielFacturation,
            RéférentielPaiement référentielPaiement)
        {
            var result = await CheckPaymentCompleted(paymentService);
            if(result.IsError)
            {
                return result;
            }

            if (IsFacturationAlreadyPaid(référentielPaiement))
            {
                result.IsError = true;
                result.Result = false;
                result.ErrorMessage = "Un paiement pour cette facture existe déjà";
                return result;
            }

            try
            {
                EnregistreFacturationPayee(référentielFacturation, référentielPaiement);

                result.Result = true;
                result.IsError = false;
                result.ErrorMessage = "";
            }
            catch (Exception ex)
            {
                result.IsError = true;
                result.Result = false;
                result.ErrorMessage = ex.Message;
            }
            return result;
        }

        private void EnregistreFacturationPayee(RéférentielFacturation référentielFacturation, RéférentielPaiement référentielPaiement)
        {
            var facturation = référentielFacturation.GetFacturation(IdFacturation.Parse(_idFacturation));
            facturation.SetFacturationPayée(true);
            référentielFacturation.EnregistrerFacturation(facturation);

            var paiement = new Paiement(IdPaiement.Parse(0), IdFacturation.Parse(_idFacturation), _orderId, PaymentStatut.Completed,
                Horloge.Instance.GetDateTime());
            référentielPaiement.EnregistrerPaiement(paiement);
        }

        private async Task<ApiResponse<bool>> CheckPaymentCompleted(IPaymentService paymentService)
        {
            var result = new ApiResponse<bool>();
            result.Result = false;
            var getOrderResult = await paymentService.CheckIfPaymentIsValid(_orderId, _idFacturation);
            if (getOrderResult.IsError)
            {
                result.IsError = true;
                result.ErrorMessage = getOrderResult.ErrorMessage;
                return result;
            }
            if (!getOrderResult.Result)
            {
                result.IsError = true;
                result.ErrorMessage = "Le paiement n'a pas été exécuté côté paypal";
                return result;
            }
            return result;
        }

        private bool IsFacturationAlreadyPaid(RéférentielPaiement référentielPaiement)
        {
            var existingPayments = référentielPaiement.GetPaiements(IdFacturation.Parse(_idFacturation));
            return existingPayments.Any(m => m.PaymentStatut == PaymentStatut.Completed);
        }
    }
}
