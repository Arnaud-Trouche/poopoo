using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Code
{
    public class Peuple : iPeuple
    {
        protected int nbUnites;
        protected List<Unite> unites;

        public List<Unite> Unites
        {
            get
            {
                return unites;
            }
        }
        public Unite creerUnite()
        {
            throw new NotImplementedException();
        }

        public Unite getUnite(Coord coordonnee)
        {
            throw new NotImplementedException();
        }

        public int nombreUnitesRestantes()
        {
            int alive = 0;
            //Parcours des Unités d'un Peuple
            foreach (Unite unite in Unites)
            {
                if (unite.PointVie > 0)
                    alive++;
            }

            return alive;
        }

    }
}
