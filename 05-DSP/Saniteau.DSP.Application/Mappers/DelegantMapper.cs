using Saniteau.DSP.Domain;
using System;
using System.Collections.Generic;
using System.Text;

namespace Saniteau.DSP.Application.Mappers
{
    public static class DelegantMapper
    {
        public static Delegant Map(Contract.Model.Delegant model)
        {
            if(model is null) { return null; }

            return new Delegant(IdDelegant.Parse(model.IdDelegant), new ChampLibre(model.Nom), new ChampLibre(model.Adresse), model.DateContrat);
        }

        public static Contract.Model.Delegant Map(Delegant delegant)
        {
            if (delegant is null) { return null; }

            return new Contract.Model.Delegant((int)delegant.IdDelegant, delegant.Nom.ToString(), delegant.Adresse.ToString(), delegant.DateContrat);
        }
    }
}
