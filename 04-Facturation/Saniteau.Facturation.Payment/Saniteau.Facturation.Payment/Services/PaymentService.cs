using Saniteau.Facturation.Payment.Dto.Paypal;
using Saniteau.Facturation.Payment.Dto.Saniteau;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace Saniteau.Facturation.Payment.Services
{
    public class PaymentService
    {
        private readonly AccessTokenManager _accessTokenManager;
        private readonly HttpMethodCaller _httpMethodCaller;

        public PaymentService(AccessTokenManager accessTokenManager, HttpMethodCaller httpMethodCaller)
        {
            _accessTokenManager = accessTokenManager ?? throw new ArgumentNullException(nameof(accessTokenManager));
            _httpMethodCaller = httpMethodCaller ?? throw new ArgumentNullException(nameof(httpMethodCaller));
        }


        public async Task<ApiResponse<bool>> CheckIfPaymentIsValid(string orderId, int idFacturation)
        {
            try
            {
                var createOrderResponse = await CallGetOrder<get_order_response>(orderId);
                var response = new ApiResponse<bool>();
                response.Result = createOrderResponse.status.ToUpper() == get_order_response.StatusCompleted;
                return response;
            }
            catch (Exception ex)
            {
                var response = new ApiResponse<bool>();
                response.IsError = true;
                response.ErrorMessage = ex.Message;
                return response;
            }
        }

        private async Task<TResult> CallGetOrder<TResult>(string orderId)
        {
            var accessToken = await _accessTokenManager.GetAccessToken();
            string orderUrl = Constants.PaypalGetOrderUrl.Replace("[order_id]", orderId);
            Dictionary<string, string> headers = new Dictionary<string, string>();
            var createOrderResponse = await _httpMethodCaller.CallGetMethod<TResult>(orderUrl, headers, accessToken);
            return createOrderResponse;
        }

    }
}
