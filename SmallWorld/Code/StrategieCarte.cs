using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Modelisation
{
    public abstract class StrategieCarte : iStrategieCarte
    {
        private int nbUnites;
        private int nbTours;
        private int nbHauteurCases;

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

        public void creerCarte()
        {
            //On appelle la Fabrique le bon nombre de fois pour initialiser les bonnes cases à la bonne valeur
            Coord c;
            for (int i = 0; i < nbHauteurCases; i++)
            {
                for (int j = 0; i < nbHauteurCases; i++)
                {
                    c = new Coord(i, j);
                    FabriqueCase.INSTANCE.obtenirCase(c);
                }
            }
        }
    }
}
