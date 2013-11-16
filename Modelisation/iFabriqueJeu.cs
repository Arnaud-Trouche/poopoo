using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Modelisation
{
    public interface FabriqueJeu
    {
        iJoueur creerJoueur(string nom, string peuple);

        iCarte creerCarte(int taille);
    }
}
