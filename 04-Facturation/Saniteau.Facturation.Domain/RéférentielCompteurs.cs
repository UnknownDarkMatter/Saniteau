using System;
using System.Collections.Generic;
using System.Text;

namespace Saniteau.Facturation.Domain
{
    public interface RéférentielCompteurs
    {
        Compteur GetCompteur(IdCompteur idCompteur);
        List<Compteur> GetAllCompteurs();
    }
}
