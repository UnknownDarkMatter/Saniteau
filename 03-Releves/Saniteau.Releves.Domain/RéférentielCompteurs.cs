using System;
using System.Collections.Generic;
using System.Text;

namespace Saniteau.Releves.Domain
{
    public interface RéférentielCompteurs
    {
        Compteur GetCompteur(IdCompteur idCompteur);
        Compteur EnregistrerCompteur(Compteur compteur);
        List<Compteur> GetAllCompteurs();
        List<Tuple<Compteur, IndexCompteur>> GetAllCompteursIndexes();
    }
}
