using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Code
{
    /// <summary>
    /// StategieCarte permet de choisir la taille de la carte, le nombre d'unités et le nombre de tours
    /// en fonction de la difficulté choisie
    /// </summary>
    public interface iStrategieCarte
    {
        int getNbTours();

        int getNbUnites();

        int getHauteurCases();

  
    }
}
