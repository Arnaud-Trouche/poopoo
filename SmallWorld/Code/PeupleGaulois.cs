using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Code
{
    public class PeupleGaulois : Peuple
    {

        public PeupleGaulois(int nbUnites, Coord pos)
        {
            this.nbUnites = nbUnites;
            unites = new List<Unite>(nbUnites);

            for (int i = 0; i < nbUnites; i++)
            {
                unites.Add(new Unite(pos, Constants.GAULOIS));
            }
        }

        public String ToString()
        {
            return "Gaulois";
        }
    }
}
