using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Code
{
    /// <summary>
    /// Classe faisant partie du patron de conception StratégieCarte
    /// Carte de Petite
    /// </summary>
    [Serializable]
    public class CartePetite : StrategieCarte
    {
        public CartePetite()
        {
            nbUnites = 6;
            nbTours = 20;
            nbHauteurCases = 10;
        }

    }
}
