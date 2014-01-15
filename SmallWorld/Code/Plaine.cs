using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Code
{
    [Serializable]
    public class Plaine : iCase
    {

        public int getPtScore(int peuple)
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

        public bool deplacementPossible(int peuple, Coord init, Coord nouvelle)
        {
            throw new NotImplementedException();
        }
    }
}
