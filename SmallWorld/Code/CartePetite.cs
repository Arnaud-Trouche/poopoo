using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Modelisation
{

    public class CartePetite : StrategieCarte
    {
        private int nbUnites;
        private int nbTours;
        private int nbHauteurCases;

        public CartePetite()
        {
            nbUnites = 6;
            nbTours = 20;
            nbHauteurCases = 10;
        }

    }
}
