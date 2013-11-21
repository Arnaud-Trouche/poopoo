using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Modelisation
{
    public interface iCase
    {
        int getPtDeplacement(Peuple p);

        bool deplacementPossible(Peuple p, Unite u);

        void partir(Unite u);
    }
}
