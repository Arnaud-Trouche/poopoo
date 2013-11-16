using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Modelisation
{
    public interface iMonteurPartie
    {
        bool initialiser();

        bool sauvegarder();

        bool restaurer();
    }
}
