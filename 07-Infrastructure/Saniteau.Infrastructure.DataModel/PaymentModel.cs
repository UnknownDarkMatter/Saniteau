using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Text;


namespace Saniteau.Infrastructure.DataModel
{
    [Table("Payments")]
    public class PaymentModel
    {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int IdPayment { get; set; }

        [ForeignKey("IdFacturation")]
        public virtual FacturationModel Facturation { get; set; }

        public int IdFacturation { get; set; }

        public string PaypalOrderId { get; set; }

        public PaymentStatut PaymentStatut { get; set; }

        public DateTime PaymentDate { get; set; }

        public PaymentModel() { }

        public PaymentModel(int idPaiement, int idFacturation, string paypalOrderId, PaymentStatut paymentStatut, DateTime paymentDate)
        {
            IdPayment = idPaiement;
            IdFacturation = idFacturation;
            PaypalOrderId = paypalOrderId;
            PaymentStatut = paymentStatut;
            PaymentDate = paymentDate;
        }
    }
}
