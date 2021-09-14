using System;
using System.Collections.Generic;
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

        public async Task<ApiResponse<bool>> EnregistrePayment(IPaymentService paymentService, RéférentielFacturation référentielFacturation)
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
            var facturation = référentielFacturation.GetFacturation(IdFacturation.Parse(_idFacturation));
            facturation.SetFacturationPayée(true);
            référentielFacturation.EnregistrerFacturation(facturation);

            result.Result = true;
            result.IsError = false;
            result.ErrorMessage = "";
            return result;
        }
    }
}
