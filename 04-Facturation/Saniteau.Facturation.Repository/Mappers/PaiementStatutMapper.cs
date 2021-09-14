using System;
using System.Collections.Generic;
using System.Text;

namespace Saniteau.Facturation.Repository.Mappers
{
    public static class PaiementStatutMapper
    {
        public static Domain.PaymentStatut Map(Infrastructure.DataModel.PaymentStatut paymentStatut)
        {
            switch (paymentStatut)
            {
                case Infrastructure.DataModel.PaymentStatut.NonDéfini:
                    {
                        return Domain.PaymentStatut.NonDéfini;
                    }
                case Infrastructure.DataModel.PaymentStatut.Completed:
                    {
                        return Domain.PaymentStatut.Completed;
                    }
                case Infrastructure.DataModel.PaymentStatut.Error:
                    {
                        return Domain.PaymentStatut.Error;
                    }
                default:
                    {
                        throw new ArgumentOutOfRangeException(nameof(paymentStatut));
                    }
            }
        }
        public static Infrastructure.DataModel.PaymentStatut Map(Domain.PaymentStatut paymentStatut)
        {
            switch (paymentStatut)
            {
                case Domain.PaymentStatut.NonDéfini:
                    {
                        return Infrastructure.DataModel.PaymentStatut.NonDéfini;
                    }
                case Domain.PaymentStatut.Completed:
                    {
                        return Infrastructure.DataModel.PaymentStatut.Completed;
                    }
                case Domain.PaymentStatut.Error:
                    {
                        return Infrastructure.DataModel.PaymentStatut.Error;
                    }
                default:
                    {
                        throw new ArgumentOutOfRangeException(nameof(paymentStatut));
                    }
            }
        }
    }
}
