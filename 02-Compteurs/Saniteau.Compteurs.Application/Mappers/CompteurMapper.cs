using Saniteau.Compteurs.Domain;
using System;
using System.Collections.Generic;
using System.Text;

namespace Saniteau.Compteurs.Application.Mappers
{
    public static class CompteurMapper
    {
        public static Contract.Model.Compteur Map(Compteur compteur)
        {
            var contractCompteur = new Contract.Model.Compteur((int)compteur.IdCompteur, compteur.NuméroCompteur?.ToString(), 
                compteur.CompteurPosé, compteur.CompteurAppairé, PDLMapper.Map(compteur.PDL), (int) compteur.IdAbonné);
            return contractCompteur;
        }
        public static Compteur Map(Contract.Model.Compteur compteur)
        {
            var domainCompteur = new Compteur(IdCompteur.Parse(compteur.IdCompteur), new ChampLibre(compteur.NuméroCompteur),
                compteur.CompteurPosé, compteur.CompteurAppairé, PDLMapper.Map(compteur.PDL),IdAbonné.Parse(compteur.IdAbonne));
            return domainCompteur;
        }
    }
}
