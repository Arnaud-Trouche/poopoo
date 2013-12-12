using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Code
{
    public class FabriquePeuple : iFabriquePeuple
    {
        public FabriquePeuple INSTANCE;
    
        public iPeuple creerPeuple(int peuple, int nbUnites, Coord pos)
        {
            if (peuple == null) return null;
            switch (peuple)
            {
                case Constants.GAULOIS:
                    return new PeupleGaulois(nbUnites, pos);

                case Constants.NAIN:
                    return new PeupleNain(nbUnites, pos);

                case Constants.VIKING:
                    return new PeupleViking(nbUnites, pos);

                default:
                    return null;
            }
        }
    }
}
