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
    [Serializable]
    public abstract class StrategieCarte : iStrategieCarte
    {
        protected int nbUnites;
        protected int nbTours;
        protected int nbHauteurCases;

        public int getNbTours()
        {
            return nbTours;
        }

        public int getNbUnites()
        {
            return nbUnites;
        }

        public int getHauteurCases()
        {
            return nbHauteurCases;
        }
    }
}
