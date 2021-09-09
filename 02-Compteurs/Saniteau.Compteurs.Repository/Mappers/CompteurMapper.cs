using Saniteau.Compteurs.Domain;
using Saniteau.Infrastructure.DataModel;
using System;
using System.Collections.Generic;
using System.Text;

namespace Saniteau.Compteurs.Repository.Mappers
{
    public static class CompteurMapper
    {
        public static CompteurModel Map(Compteur compteur)
        {
            if(compteur is null) { return null; }

            var model = new CompteurModel((int)compteur.IdCompteur, compteur.NuméroCompteur.ToString(), compteur.CompteurPosé);
            return model;
        }

        public static Compteur Map(CompteurModel model, bool compteurAppairé, PDLModel pdlModel, int idAbonné)
        {
            if (model is null) { return null; }

            var pdl = PDLMapper.Map(pdlModel);
            var compteur = new Compteur(IdCompteur.Parse(model.IdCompteur), new ChampLibre(model.NuméroCompteur), model.CompteurPosé, compteurAppairé, pdl,
                IdAbonné.Parse(idAbonné));
            return compteur;
        }
    }
}
