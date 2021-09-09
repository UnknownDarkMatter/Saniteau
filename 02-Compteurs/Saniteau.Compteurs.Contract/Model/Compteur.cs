using System;
using System.Collections.Generic;
using System.Text;

namespace Saniteau.Compteurs.Contract.Model
{
    public class Compteur
    {
        public int IdCompteur { get; set; }
        public string NuméroCompteur { get; set; }
        public bool CompteurPosé { get; set; }
        public bool CompteurAppairé { get; set; }
        public PDL PDL { get; set; }
        public int IdAbonne { get; set; }

        public Compteur()
        {

        }

        public Compteur(int idCompteur, string numéroCompteur, bool compteurPosé, bool compteurAppairé, PDL pdl, int idAbonne)
        {
            IdCompteur = idCompteur;
            NuméroCompteur = numéroCompteur;
            CompteurPosé = compteurPosé;
            CompteurAppairé = compteurAppairé;
            PDL = pdl;
            IdAbonne = idAbonne;
        }
    }
}
