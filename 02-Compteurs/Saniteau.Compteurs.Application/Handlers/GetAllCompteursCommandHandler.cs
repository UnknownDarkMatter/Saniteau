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
    public class GetAllCompteursCommandHandler : ActionHandlerBase<GetAllCompteursCommand, GetAllCompteursDomainCommand, List<Contract.Model.Compteur>>
    {
        private readonly RéférentielCompteurs _référentielCompteurs;
        public GetAllCompteursCommandHandler(RéférentielCompteurs référentielCompteurs) : base (new GetAllCompteursCommandMapper())
        {
            if (référentielCompteurs is null) { throw new ArgumentNullException(nameof(référentielCompteurs)); }

            _référentielCompteurs = référentielCompteurs;
        }

        protected override List<Contract.Model.Compteur> Handle(GetAllCompteursDomainCommand action)
        {
            return _référentielCompteurs.GetAllCompteurs().Select(CompteurMapper.Map).ToList();
        }
    }
}
