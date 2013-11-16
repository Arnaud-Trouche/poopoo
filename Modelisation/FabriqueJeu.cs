using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Modelisation
{
    public interface FabriqueJeu
    {
        Joueur creerJoueur(string nom, string peuple);

        Carte creerCarte(int taille);
    }
}
