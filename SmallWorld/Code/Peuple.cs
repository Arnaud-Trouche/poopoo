using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Code
{
    /// <summary>
    /// Classe représentant un Peuple 
    /// Un peuple contient sa liste d'unités. 
    /// Possède des méthodes renvoyant le nombres d'unités restantes, le nombre d'unités sur une case,...
    /// </summary>
    [Serializable]
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
        
        /// <summary>
        ///Remet le point de déplacement aux unités du peuple pour commencer un nouveau tour de jeu.
        /// </summary>
        public void  remettrePtDeplacement(){
            foreach (Unite unite in Unites)
            {
                unite.debutTour();
            }

        }
       
        /// <summary>
        /// Récupère la meilleur unités ennemie (défense) présente sur la case pos.
        /// </summary>
        /// <param name="pos">Case où la recherche se réalise</param>
        /// <returns>La meilleur unité (défense) présente sur la case</returns>
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

        /// <summary>
        /// Retourne la liste des unités présente sur la case passée en paramètre.
        /// </summary>
        /// <param name="pos">Case de recherche</param>
        /// <returns>Retourne la liste des unités présente sur la case pos.</returns>
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

        /// <summary>
        /// Constructeur de Peuple. Initialise le nombre d'unités et ajoute le bon nombre d'unités à la position voulue.
        /// </summary>
        /// <param name="nbUnites">Nombre d'unités du peuple.</param>
        /// <param name="pos">Position où les unités seront placées.</param>
         public Peuple(int nbUnites, Coord pos)
        {
            this.nbUnites = nbUnites;
            unites = new List<Unite>(nbUnites);

            for (int i = 0; i < nbUnites; i++)
            {
                unites.Add(new Unite(pos, this));
            }
        }

        /// <summary>
        /// Retourne le nombre d'unités restantes.
        /// </summary>
        /// <returns>Retourne le nombre d'unités restantes.</returns>
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
