using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Code
{
    /// <summary>
    /// Classe faisant partie du patron de conception StratégieCarte
    /// Carte de Normale
    /// </summary>
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
