﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;


namespace Saniteau.Mappers
{
    public static class FacturationMapper
    {
        public static Models.Facturation.Facturation Map(Facturation.Contract.Model.Facturation facturation)
        {
            var abonné = AbonnéMapper.Map(facturation.Abonné);
            return new Models.Facturation.Facturation(facturation.IdFacturation, facturation.IdCampagneFacturation, abonné, facturation.DateFacturation, 
                facturation.FacturationLignes.Select(FacturationLigneMapper.Map).ToList(), (int)facturation.IdDernierIndex, facturation.Payée);
        }

    }
}
