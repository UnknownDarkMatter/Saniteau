using System;
using System.Collections.Generic;
using System.Text;

namespace Saniteau.Facturation.Payment.Dto.Paypal
{
    public class get_order_response
    {
        public const string StatusCompleted = "COMPLETED";

        public string id { get; set; }
        /// <summary>
        /// CAPTURE
        /// </summary>
        public string intent { get; set; }
        /// <summary>
        /// COMPLETED
        /// </summary>
        public string status { get; set; }
    }
}
