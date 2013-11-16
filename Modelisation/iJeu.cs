using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Modelisation
{
    public interface iJeu
    {
        void lancerJeu(iCarte c, iJoueur j1, iJoueur j2);
    }
}
