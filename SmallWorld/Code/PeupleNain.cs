using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Code
{
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
