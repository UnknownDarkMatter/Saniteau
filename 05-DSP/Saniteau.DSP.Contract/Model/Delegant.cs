using Saniteau.Common;
using System;
using System.Collections.Generic;
using System.Text;

namespace Saniteau.DSP.Contract.Model
{
    public class Delegant
    {
        public int IdDelegant { get; set; }
        public string Nom { get; set; }
        public string Adresse { get; set; }
        public DateTime DateContrat { get; set; }

        public Delegant() { }
        public Delegant(int idDelegant, string nom, string adresse, DateTime dateContrat)
        {
            IdDelegant = idDelegant;
            Nom = nom;
            Adresse = adresse;
            DateContrat = dateContrat;
        }
    }
}
