using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Code
{
    [Serializable]
    public class Foret : iCase
    {

        public int getPtScore(int peuple)
        {
            if (peuple == Constants.NAIN)
            {
                return 2;
            }
            else
            {
                return 1;
            }
        }

        public bool deplacementPossible(int peuple, Coord init, Coord nouvelle)
        {
            throw new NotImplementedException();
        }
    }
}
