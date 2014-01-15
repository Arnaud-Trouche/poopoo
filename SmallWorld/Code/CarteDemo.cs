using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Code
{
    [Serializable]
    public class CarteDemo : StrategieCarte
    {
        public CarteDemo()
        {
            nbUnites = 4;
            nbTours = 5;
            nbHauteurCases = 5;
        }


    }

}
