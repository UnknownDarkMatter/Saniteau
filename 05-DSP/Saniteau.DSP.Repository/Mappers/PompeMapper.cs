using Saniteau.Infrastructure.DataModel;
using Saniteau.DSP.Domain;
using System;
using System.Collections.Generic;
using System.Text;

namespace Saniteau.DSP.Repository.Mappers
{
    public static class PompeMapper
    {
        public static Pompe Map(PompeModel model)
        {
            return new Pompe(IdPompe.Parse(model.IdPompe), IdCompteur.Parse(model.IdCompteur), new ChampLibre(model.NuméroPompe));
        }

        public static PompeModel Map(Pompe pompe)
        {
            return new PompeModel((int) pompe.IdPompe, (int)pompe.IdCompteur, pompe.NuméroPompe.ToString());
        }
    }
}
