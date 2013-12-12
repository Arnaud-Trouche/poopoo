using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Code
{
    public class Plaine : iCase
    {

        int getPtScore(int peuple)
        {
            switch(peuple){
                case Constants.GAULOIS:
                    return 2;

                case Constants.NAIN:
                    return 0;

                default:
                    return 1;           
            }
        }

        bool deplacementPossible(int peuple, Coord init, Coord nouvelle)
        {
            throw new NotImplementedException();
        }
    }
}
