using System;
using System.Collections.Generic;
using System.Text;

namespace Saniteau.Releves.Domain.Commands
{
    public class CréePompeDomainCommand
    {
        public IdCompteur IdCompteur { get; private set; }
        public ChampLibre NuméroPompe { get; private set; }

        public CréePompeDomainCommand(IdCompteur idCompteur, ChampLibre numéroPompe)
        {
            IdCompteur = idCompteur;
            NuméroPompe = numéroPompe;
        }

        public Pompe CréePompe(RéférentielPompes référentielPompes)
        {
            var pompe = new Pompe(IdPompe.Parse(0), IdCompteur, NuméroPompe);
            return référentielPompes.EnregistrerPompe(pompe);
        }
    }
}
