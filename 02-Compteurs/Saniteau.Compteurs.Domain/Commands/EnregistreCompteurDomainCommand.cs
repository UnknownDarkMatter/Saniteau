using System;
using System.Collections.Generic;
using System.Text;

namespace Saniteau.Compteurs.Domain.Commands
{
    public class EnregistreCompteurDomainCommand
    {
        public IdCompteur IdCompteur { get; private set; }
        public ChampLibre NuméroCompteur { get; private set; }

        public EnregistreCompteurDomainCommand(IdCompteur idCompteur, ChampLibre numéroCompteur)
        {
            if (idCompteur is null) { throw new ArgumentNullException(nameof(idCompteur)); }
            if (numéroCompteur is null) { throw new ArgumentNullException(nameof(numéroCompteur)); }

            IdCompteur = idCompteur;
            NuméroCompteur = numéroCompteur;
        }

        public Compteur EnregistreCompteur(RéférentielCompteurs référentielCompteurs)
        {
            if (référentielCompteurs is null) { throw new ArgumentNullException(nameof(référentielCompteurs)); }

            Compteur compteur = null;
            bool creerCompteur = (int)IdCompteur == 0;
            if (!creerCompteur)
            {
                compteur = référentielCompteurs.GetCompteur(IdCompteur);
                creerCompteur = compteur == null;
            }

            if (creerCompteur)
            {
                return référentielCompteurs.CréerCompteur(NuméroCompteur);
            }
            compteur.SetNuméroCompteur(this.NuméroCompteur);
            return référentielCompteurs.EnregistrerCompteur(compteur);
        }
    }
}
