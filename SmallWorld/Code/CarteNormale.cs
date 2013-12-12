using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Code
{
    public class CarteNormale : StrategieCarte
    {
        private int nbHauteurCases;
        private int nbTours;
        private int nbUnites;

        public CarteNormale()
        {
            nbUnites = 8;
            nbTours = 30;
            nbHauteurCases = 15;
        }
        
    }
}
