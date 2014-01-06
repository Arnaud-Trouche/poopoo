using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Code
{
    public class Montagne : iCase
    {
        public int getPtScore(int peuple)
        {
            if (peuple == Constants.GAULOIS)
            {
                return 0;
            }
            else {
                return 1;
            }
        }

        public bool deplacementPossible(int peuple, Coord init, Coord nouvelle)
        {
            throw new NotImplementedException();
        }
    }
}
