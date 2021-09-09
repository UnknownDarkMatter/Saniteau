using Saniteau.Compteurs.Domain;
using Saniteau.Infrastructure.DataModel;
using System;
using System.Collections.Generic;
using System.Text;

namespace Saniteau.Compteurs.Repository.Mappers
{
    public static class AdressePDLMapper
    {
        public static AdressePDLModel Map(AdressePDL adressePDL)
        {
            if(adressePDL is null) { return null; }

            return new AdressePDLModel((int)adressePDL.IdAdressePDL, (int)adressePDL.IdAdresse, (int)adressePDL.IdPDL);
        }

        public static AdressePDL Map(AdressePDLModel model)
        {
            if (model is null) { return null; }

            return new AdressePDL(IdAdressePDL.Parse(model.IdAdressePDL), IdAdresse.Parse(model.IdAdresse), IdPDL.Parse(model.IdPDL));
        }
    }
}
