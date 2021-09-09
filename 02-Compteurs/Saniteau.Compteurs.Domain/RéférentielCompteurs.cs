using System;
using System.Collections.Generic;
using System.Text;

namespace Saniteau.Compteurs.Domain
{
    public interface RéférentielCompteurs
    {
        Compteur CréerCompteur(ChampLibre numéroCompteur);
        Compteur EnregistrerCompteur(Compteur compteur);
        void SupprimerCompteur(IdCompteur idCompteur);
        Compteur GetCompteur(IdCompteur idCompteur);
        IEnumerable<Compteur> GetAllCompteurs();
    }
}
