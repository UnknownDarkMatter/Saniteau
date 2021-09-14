using Saniteau.Common.Mediator;
using System;
using System.Collections.Generic;
using System.Text;

namespace Saniteau.Facturation.Contract.Commands
{
    public class EnregistrePaymentCommand : IAction<Model.ApiResponse<bool>>
    {
        public readonly string OrderId;
        public readonly int IdFacturation;
        public EnregistrePaymentCommand(string orderId, int idFacturation)
        {
            OrderId = orderId ?? throw new ArgumentNullException(nameof(orderId));
            IdFacturation = idFacturation;
        }
    }
}
