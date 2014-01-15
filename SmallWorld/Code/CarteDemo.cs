using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Code
{
    /// <summary>
    /// Classe faisant partie du patron de conception StratégieCarte
    /// Carte de Démonstration
    /// </summary>
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
