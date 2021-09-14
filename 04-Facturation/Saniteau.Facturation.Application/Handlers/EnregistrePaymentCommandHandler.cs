using Saniteau.Common.Mediator;
using Saniteau.Facturation.Application.Mappers;
using Saniteau.Facturation.Contract.Commands;
using Saniteau.Facturation.Domain;
using Saniteau.Facturation.Domain.Commands;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace Saniteau.Facturation.Application.Handlers
{
    public class EnregistrePaymentCommandHandler : ActionHandlerBase<EnregistrePaymentCommand, EnregistrePaymentDomainCommand, Contract.Model.ApiResponse<bool>>
    {
        private readonly RéférentielFacturation _référentielFacturation;
        private readonly RéférentielPaiement _référentielPaiement;
        private readonly IPaymentService _paymentService;

        public EnregistrePaymentCommandHandler(IPaymentService paymentService, RéférentielFacturation référentielFacturation,
            RéférentielPaiement référentielPaiement)
            : base(new EnregistrePaymentCommandMapper())
        {
            _référentielFacturation = référentielFacturation ?? throw new ArgumentNullException(nameof(référentielFacturation));
            _référentielPaiement = référentielPaiement ?? throw new ArgumentNullException(nameof(référentielPaiement));
            _paymentService = paymentService ?? throw new ArgumentNullException(nameof(_paymentService));
        }

        /// <summary>
        /// Obsolète : Seule la méthode asynchrone est implémentée
        /// </summary>
        /// <param name="action"></param>
        /// <returns></returns>
        [Obsolete]
        protected override Contract.Model.ApiResponse<bool> Handle(EnregistrePaymentDomainCommand action)
        {
            throw new NotImplementedException("Seule la méthode asynchrone est implémentée");
        }

        protected async override Task<Contract.Model.ApiResponse<bool>> HandleAsync(EnregistrePaymentDomainCommand action)
        {
            var paymentEnregistrementResult = await action.EnregistrePayment(_paymentService, _référentielFacturation, _référentielPaiement);
            return ApiResponseMapper<bool>.Map(paymentEnregistrementResult);
        }
    }
}
