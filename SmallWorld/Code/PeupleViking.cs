using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Code
{
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
