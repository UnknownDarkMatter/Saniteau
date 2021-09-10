using System;
using System.Collections.Generic;
using System.Text;

namespace Saniteau.Facturation.Domain.Commands
{
    public class GetFacturationDomainCommand
    {
        private IdFacturation _idFacturation;
        private IdAbonné _idAbonné;

        public GetFacturationDomainCommand(IdFacturation idFacturation, IdAbonné idAbonné)
        {
            _idFacturation = idFacturation ?? throw new ArgumentNullException(nameof(idFacturation));
            _idAbonné = idAbonné ?? throw new ArgumentNullException(nameof(idAbonné));
        }

        public Tuple<Facturation, Adresse> GetFacturation(RéférentielFacturation référentielFacturation, RéférentielAbonnés référentielAbonnés)
        {
            var facturation =  référentielFacturation.GetFacturation(_idFacturation);
            var adresse = référentielAbonnés.GetAddresseOfAbonné(_idAbonné);
            return new Tuple<Facturation, Adresse>(facturation, adresse);
        }
    }
}
