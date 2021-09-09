﻿using System;
using System.Collections.Generic;
using System.Text;

namespace Saniteau.Compteurs.Domain
{
    public interface RéférentielIndexesCompteurs
    {
        IndexCompteur EnregistreIndex(IndexCompteur index);
        List<IndexCompteur> GetIndexesOfCompteur(IdCompteur idCompteur);
    }
}
