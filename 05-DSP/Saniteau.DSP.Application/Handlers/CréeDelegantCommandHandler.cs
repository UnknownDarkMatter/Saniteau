﻿using Saniteau.Common.Mediator;
using Saniteau.DSP.Application.Mappers;
using Saniteau.DSP.Contract.Commands;
using Saniteau.DSP.Contract.Model;
using Saniteau.DSP.Domain;
using Saniteau.DSP.Domain.Commands;
using System;
using System.Collections.Generic;
using System.Text;

namespace Saniteau.DSP.Application.Handlers
{
    public class CréeDelegantCommandHandler : ActionHandlerBase<CréeDelegantCommand, CréeDelegantDomainCommand, Contract.Model.Delegant>
    {
        private readonly RéférentielDelegant _référentielDelegant;
        public CréeDelegantCommandHandler(RéférentielDelegant référentielDelegant) : base(new CréeDelegantCommandMapper())
        {
            _référentielDelegant = référentielDelegant;
        }

        protected override Contract.Model.Delegant Handle(CréeDelegantDomainCommand action)
        {
            var delegant = new Domain.Delegant(IdDelegant.Parse(0), action.Nom, action.Adresse, action.DateContrat);
            delegant = _référentielDelegant.EnregistreDelegant(delegant);
            return DelegantMapper.Map(delegant);
        }
    }
}
