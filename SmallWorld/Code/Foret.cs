using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Code
{
    public class Foret : iCase
    {

        int getPtScore(int peuple)
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

        bool deplacementPossible(int peuple, Coord init, Coord nouvelle)
        {
            throw new NotImplementedException();
        }
    }
}
