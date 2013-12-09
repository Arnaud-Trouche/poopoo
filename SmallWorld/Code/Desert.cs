using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Modelisation
{
    public class Desert : iCase
    {
            
        public int getPtDeplacement(Peuple p)
        {
            //Pas d'exception tous les peuples ont les mêmes points de déplacement
            return 1;
        }

        bool deplacementPossible(Peuple p, Coord init, Coord nouvelle)
        {
            throw new NotImplementedException();
        }

        int getPtScore(Peuple u)
        {
            throw new NotImplementedException();
        }
    }
}
