using Saniteau.DSP.Domain;
using Saniteau.Infrastructure.DataModel;
using System;
using System.Collections.Generic;
using System.Text;

namespace Saniteau.DSP.Repository.Mappers
{
    public static class AbonnéMapper
    {
        public static AbonnéModel Map(Abonné abonné)
        {
            if(abonné is null) { throw new ArgumentNullException(nameof(abonné));  }
            var tarification = TarificationMapper.Map(abonné.Tarification);

            return new AbonnéModel((int)abonné.IdAbonné, (int)abonné.IdAdresse, abonné.Nom.ToString(), abonné.Prénom.ToString(), tarification);
        }

        public static Abonné Map(AbonnéModel model)
        {
            if (model is null) { throw new ArgumentNullException(nameof(model)); }
            var tarification = TarificationMapper.Map(model.Tarification);

            return new Abonné(IdAbonné.Parse(model.IdAbonné), IdAdresse.Parse(model.IdAdresse), new ChampLibre(model.Nom), new ChampLibre(model.Prénom), tarification);
        }
    }
}
