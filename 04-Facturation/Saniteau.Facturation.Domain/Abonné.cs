using System;
using System.Collections.Generic;
using System.Text;

namespace Saniteau.Facturation.Domain
{
    public class Abonné
    {
        public IdAbonné IdAbonné { get; private set; }
        public IdAdresse IdAdresse { get; private set; }
        public ChampLibre Nom { get; private set; }
        public ChampLibre Prénom { get; private set; }
        public Tarification Tarification { get; private set; }

        public Abonné(IdAbonné idAbonné, IdAdresse idAdresse, ChampLibre nom, ChampLibre prénom, Tarification tarification)
        {
            if (idAbonné is null) { throw new ArgumentNullException(nameof(idAbonné)); }
            if (idAdresse is null) { throw new ArgumentNullException(nameof(idAdresse)); }
            if (nom is null) { throw new ArgumentNullException(nameof(nom)); }
            if (prénom is null) { throw new ArgumentNullException(nameof(prénom)); }

            IdAbonné = idAbonné;
            IdAdresse = idAdresse;
            Nom = nom;
            Prénom = prénom;
            Tarification = tarification;
        }

        public void ChangeAdresse(IdAdresse idAdresse)
        {
            IdAdresse = idAdresse;
        }

        public void ChangeNom(ChampLibre nom)
        {
            Nom = nom;
        }

        public void ChangePrénom(ChampLibre prénom)
        {
            Prénom = prénom;
        }

        public void DéfinitTarification(Tarification tarification)
        {
            Tarification = tarification;
        }
    }
}
