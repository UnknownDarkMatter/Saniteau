using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Saniteau.Models.Delegation
{
    public class Delegant
    {
        public int IdDelegant { get; set; }
        public string Nom { get; set; }
        public string Adresse { get; set; }
        public string DateContrat { get; set; }

        public Delegant(int idDelegant, string nom, string adresse, string dateContrat)
        {
            IdDelegant = idDelegant;
            Nom = nom;
            Adresse = adresse;
            DateContrat = dateContrat;
        }
    }
}
