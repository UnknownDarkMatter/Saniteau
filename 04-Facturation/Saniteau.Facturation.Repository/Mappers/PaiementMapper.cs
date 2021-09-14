using Saniteau.Facturation.Domain;
using Saniteau.Infrastructure.DataModel;
using Saniteau.Common;
using System;
using System.Collections.Generic;
using System.Text;
using System.Linq;

namespace Saniteau.Facturation.Repository.Mappers
{
    public static class PaiementMapper
    {
        public static Domain.Paiement Map(PaymentModel model)
        {
            return new Domain.Paiement(IdPaiement.Parse(model.IdPayment), IdFacturation.Parse(model.IdFacturation), 
                model.PaypalOrderId, PaiementStatutMapper.Map(model.PaymentStatut), model.PaymentDate);
        }

        public static PaymentModel Map(Domain.Paiement paiement)
        {
            return new PaymentModel((int)paiement.IdPaiement, (int)paiement.IdFacturation, paiement.PaypalOrderId,
                PaiementStatutMapper.Map(paiement.PaymentStatut), paiement.PaymentDate);
        }
    }
}
