using Saniteau.Common.Mediator;
using Saniteau.Compteurs.Application.Mappers;
using Saniteau.Compteurs.Contract.Commands;
using Saniteau.Compteurs.Contract.Model;
using Saniteau.Compteurs.Domain;
using Saniteau.Compteurs.Domain.Commands;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Saniteau.Compteurs.Application.Handlers
{
    public class GetAllAbonnésCommandHandler : ActionHandlerBase<GetAllAbonnésCommand, GetAllAbonnésDomainCommand, List<Contract.Model.Abonne>>
    {
        private readonly RéférentielAbonnés _référentielAbonnés;
        public GetAllAbonnésCommandHandler(RéférentielAbonnés référentielAbonnés) : base (new GetAllAbonnésCommandMapper())
        {
            if (référentielAbonnés is null) { throw new ArgumentNullException(nameof(référentielAbonnés)); }

            _référentielAbonnés = référentielAbonnés;
        }

        protected override List<Contract.Model.Abonne> Handle(GetAllAbonnésDomainCommand action)
        {
            List<Contract.Model.Abonne> result = new List<Contract.Model.Abonne>();
            var abonnésAdressesTuples = _référentielAbonnés.GetAllAbonnés(action.FiltrerAbonnésAvecCompteur);
            foreach(var tuple in abonnésAdressesTuples)
            {
                result.Add(AbonnéMapper.Map(tuple.Item1, tuple.Item2));
            }
            return result;
        }
    }
}
