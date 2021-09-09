using System;
using System.Collections.Generic;
using System.Text;

namespace Saniteau.Releves.Contract.Model
{
    public class Pompe
    {
        public string NuméroPompe { get; set; }
        public int IdCompteur { get; set; }
        public Pompe()
        {

        }

        public Pompe(string numéroPompe, int idCompteur)
        {
            IdCompteur = idCompteur;
            NuméroPompe = numéroPompe;
        }
    }
}
