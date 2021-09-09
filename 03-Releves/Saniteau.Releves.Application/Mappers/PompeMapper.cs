using Saniteau.Releves.Domain;
using System;
using System.Collections.Generic;
using System.Text;

namespace Saniteau.Releves.Application.Mappers
{
    public static class PompeMapper
    {
        public static Contract.Model.Pompe Map(Pompe pompe)
        {
            var contract = new Contract.Model.Pompe(pompe.NuméroPompe.ToString(), (int)pompe.IdCompteur);
            return contract;
        }

    }
}
