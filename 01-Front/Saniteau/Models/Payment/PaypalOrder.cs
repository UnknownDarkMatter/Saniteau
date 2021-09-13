using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Saniteau.Models.Payment
{
    public class PaypalOrder
    {
        public int IdFacturation { get; set; }
        public string OrderId { get; set; }
    }
}
