using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Saniteau.Mappers
{
    public static class CompteurMapper
    {
        public static Models.Compteurs.Compteur Map(Compteurs.Contract.Model.Compteur compteur)
        {
            if (compteur == null) { return null; }

            return new Models.Compteurs.Compteur(compteur.IdCompteur, compteur.NuméroCompteur, compteur.CompteurPosé, compteur.CompteurAppairé, PDLMapper.Map(compteur.PDL), compteur.IdAbonne);
        }
        public static Compteurs.Contract.Model.Compteur Map(Models.Compteurs.Compteur compteur)
        {
            if (compteur == null) { return null; }

            return new Compteurs.Contract.Model.Compteur(compteur.IdCompteur, compteur.NumeroCompteur, compteur.CompteurEstPose, compteur.compteurEstAppaire,
                PDLMapper.Map(compteur.PDL), compteur.IdAbonne);
        }
    }
}
