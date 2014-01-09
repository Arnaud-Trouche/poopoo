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
        
        public void  remettrePtDeplacement(){
            foreach (Unite unite in Unites)
            {
                unite.debutTour();
            }

        }
        /**
    * @fn meilleureUnite(List<Unite> listeUnite)
    * @brief Récupère la meilleure unité défensive de la list
    * 
    * @param List<Unite> <b>listeUnite</b> la liste d'unité
    * @return Unite la meilleure unité défensive
    */
        public Unite meilleureUnite(Coord pos)
        {
            List<Unite> lesUnites = getUnitesPos(pos);

            if (lesUnites.Count == 0)
            {
                return null;
            }
            else
            {
                Unite meilleureU = lesUnites[0];
                foreach (Unite u in lesUnites)
                {
                    if ((u.PointDefense + u.PointVie) > (meilleureU.PointDefense + meilleureU.PointVie))
                    {
                        meilleureU = u;
                    }
                }
                return meilleureU;
            }

        }

        public List<Unite> getUnitesPos(Coord pos)
        {
            List<Unite> res = new List<Unite>();

            foreach(Unite u in unites)
            {
                if (u.Position.Equals(pos))
                    res.Add(u);
            }

            return res;
        }


         public Peuple(int nbUnites, Coord pos)
        {
            this.nbUnites = nbUnites;
            unites = new List<Unite>(nbUnites);

            for (int i = 0; i < nbUnites; i++)
            {
                unites.Add(new Unite(pos, this));
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
