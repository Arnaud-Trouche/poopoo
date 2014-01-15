using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Code
{
    [Serializable]
    public class CarteNormale : StrategieCarte
    {
        public CarteNormale()
        {
            nbUnites = 8;
            nbTours = 30;
            nbHauteurCases = 15;
        }
        
    }
}
