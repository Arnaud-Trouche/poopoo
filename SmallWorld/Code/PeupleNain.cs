using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Code
{
    public class PeupleNain : Peuple
    {
        public PeupleNain(int nbUnites, Coord pos)
        {
            this.nbUnites = nbUnites;
            unites = new List<Unite>(nbUnites);

            for (int i = 0; i < nbUnites; i++)
            {
                unites.Add(new Unite(pos, Constants.NAIN));
            }
        }

        public Unite getUnite(Coord coordonnee)
        {
            Unite result = unites.Find(
            delegate(Unite uni)
            {
                return uni.Position == coordonnee;
            }
            );
            //si pas trouvé, null est envoyé
            return result;
        }
    }
}
