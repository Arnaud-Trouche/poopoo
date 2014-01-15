using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Code
{
    /// <summary>
    /// Classe représentant un peuple de type Nain
    /// </summary>
    [Serializable]
    public class PeupleNain : Peuple
    {
    
        public PeupleNain(int nbUnites,Coord pos) : base(nbUnites, pos)
        {
           
        }


        public override String ToString()
        {
            return "Nains";
        }
    }
}
