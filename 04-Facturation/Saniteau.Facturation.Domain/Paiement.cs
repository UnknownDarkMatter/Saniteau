using Saniteau.Common;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Saniteau.Facturation.Domain
{
    public class Paiement
    {
        public IdPaiement IdPaiement { get; private set; }
        public IdFacturation IdFacturation { get; private set; }
        public string PaypalOrderId { get; private set; }
        public PaymentStatut PaymentStatut { get; private set; }
        public DateTime PaymentDate { get; private set; }

        public Paiement(IdPaiement idPaiement, IdFacturation idFacturation, string paypalOrderId,
            PaymentStatut paymentStatut, DateTime paymentDate)
        {
            IdPaiement = idPaiement;
            IdFacturation = idFacturation;
            PaypalOrderId = paypalOrderId;
            PaymentStatut = paymentStatut;
            PaymentDate = paymentDate;
        }
    }
}
