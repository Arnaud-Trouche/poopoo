using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Code
{
    [Serializable]
    public class Eau : iCase
    {

        public int getPtScore(int peuple)
        {
            //Pas d'exception seul les vikins peuvent s'y rendre, mais il n'y gagnent pas de points
            return 0;
        }

        public bool deplacementPossible(int peuple, Coord init, Coord nouvelle)
        {
            throw new NotImplementedException();
        }
    }
}
