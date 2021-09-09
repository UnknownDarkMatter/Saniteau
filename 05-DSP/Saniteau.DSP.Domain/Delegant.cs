using Saniteau.Common;
using System;
using System.Collections.Generic;
using System.Text;

namespace Saniteau.DSP.Domain
{
    public class Delegant
    {
        public IdDelegant IdDelegant { get; private set; }
        public ChampLibre Nom { get; private set; }
        public ChampLibre Adresse { get; private set; }
        public Date DateContrat { get; private set; }

        public Delegant(IdDelegant idDelegant, ChampLibre nom, ChampLibre adresse, Date dateContrat)
        {
            if (idDelegant is null) { throw new ArgumentNullException(nameof(idDelegant)); }
            if (nom is null) { throw new ArgumentNullException(nameof(nom)); }
            if (adresse is null) { throw new ArgumentNullException(nameof(adresse)); }
            if (dateContrat is null) { throw new ArgumentNullException(nameof(dateContrat)); }

            IdDelegant = idDelegant;
            Nom = nom;
            Adresse = adresse;
            DateContrat = dateContrat;
        }

        public void ChangeNom(ChampLibre nom)
        {
            Nom = nom;
        }
        public void ChangeAdresse(ChampLibre adresse)
        {
            Adresse = adresse;
        }
        public void ChangeDateContrat(Date dateContrat)
        {
            DateContrat = dateContrat;
        }
    }
}
