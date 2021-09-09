using System;
using System.Collections.Generic;
using System.Text;

namespace Saniteau.Models.Compteurs
{
    public class Compteur
    {
        public int IdCompteur { get; set; }
        public string NumeroCompteur { get; set; }
        public bool CompteurEstPose { get; set; }
        public bool compteurEstAppaire { get; set; }
        public PDL PDL { get; set; }
        public int IdAbonne { get; set; }

        public Compteur()
        {

        }

        public Compteur(int idCompteur, string numeroCompteur, bool compteurEstPose, bool compteurAppairé, PDL pdl, int idAbonne)
        {
            IdCompteur = idCompteur;
            NumeroCompteur = numeroCompteur;
            CompteurEstPose = compteurEstPose;
            compteurEstAppaire = compteurAppairé;
            PDL = pdl;
            IdAbonne = idAbonne;
        }
    }
}
