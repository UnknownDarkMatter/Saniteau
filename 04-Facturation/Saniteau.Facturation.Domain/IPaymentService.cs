using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace Saniteau.Facturation.Domain
{
    public interface IPaymentService
    {
        Task<ApiResponse<bool>> CheckIfPaymentIsValid(string orderId, int idFacturation);
    }
}
