using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Code
{
    /// <summary>
    /// Classe représentant un peuple de type Gaulois
    /// </summary>
    [Serializable]
    public class PeupleGaulois : Peuple
    {

        public PeupleGaulois(int nbUnites,Coord pos) : base(nbUnites, pos)
        {
           
        }

        public override String ToString()
        {
            return "Gaulois";
        }
    }
}
