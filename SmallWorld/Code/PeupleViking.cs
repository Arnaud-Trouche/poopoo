using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Code
{
    /// <summary>
    /// Classe représentant un peuple de type Viking
    /// </summary>
    [Serializable]
    public class PeupleViking : Peuple
    {
       
        public PeupleViking(int nbUnites,Coord pos) : base(nbUnites, pos)
        {
           
        }

        public override String ToString()
        {
            return "Vikings";
        }
    }
}
