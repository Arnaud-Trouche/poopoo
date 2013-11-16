using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Modelisation
{
    public interface MonteurPartie
    {
        bool initialiser();

        bool sauvegarder();

        bool restaurer();
    }
}
