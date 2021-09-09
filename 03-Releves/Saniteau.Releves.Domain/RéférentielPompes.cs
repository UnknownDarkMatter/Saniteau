using System;
using System.Collections.Generic;
using System.Text;

namespace Saniteau.Releves.Domain
{
    public interface RéférentielPompes
    {
        List<Pompe> GetAllPompes();
        Pompe EnregistrerPompe(Pompe pompe);
    }
}
