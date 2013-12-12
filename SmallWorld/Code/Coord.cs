using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Code
{
    public class Coord
    {
        private int x;
        private int y;

        /// <summary>
        ///  Constructeur à 2 arguments : abscisse et ordonnée 
        /// </summary>
        /// <param name="x1">Abscisse</param>
        /// <param name="y1">Ordonnée</param>
        public Coord(int x1, int y1)
        {
            int max = Carte.getTaille();
            if ((x1 < 0 || x1 > max) && (y1 < 0 || y1 > max))
            {
                throw new System.ArgumentOutOfRangeException();
            }
            else
            {
                this.x = x1;
                this.y = y1;
            }
        }

        /// <summary>
        /// Constructeur prenant un indice de tableau unidimensionel et le traduisant en coordonnée catésienne 
        /// </summary>
        /// <param name="indiceTab1dimension">Coordonnée unidimensionnelle</param>
        public Coord(int indiceTab1dimension)
        {
            int taille = Carte.getTaille();
            if (indiceTab1dimension < 0 || indiceTab1dimension > taille*taille)
            {
                throw new System.ArgumentOutOfRangeException();
            }
            this.x = indiceTab1dimension % taille;
            this.y = indiceTab1dimension / taille;
        }


        public int getX()
        {
            return x;
        }

        public int getY()
        {
            return y;
        }

        public void setX(int x1)
        {
            int max = Carte.getTaille();
            if (x1 < 0 || x1 > max)
            {
                throw new System.ArgumentOutOfRangeException();
            }
            else
            {
                this.x = x1;
            }
        }

        public void setY(int y1)
        {
            int max = Carte.getTaille();
            if (y1 < 0 || y1 > max)
            {
                throw new System.ArgumentOutOfRangeException();
            }
            else
            {
                this.y = y1;
            }
        }

        /// <summary>
        /// Renvoie l'indice unidimensionnel d'une coordonnée en prenant en compte la largeur de la carte.
        /// </summary>
        /// <returns> Renvoie l'indice unidimensionnel d'une coordonnée en prenant en compte la largeur de la carte.</returns>
        public int getIndiceTab1Dimension()
        {
            int max = Carte.getTaille();

            return this.y * max + this.x;
        }

        /// <summary>
        /// Indique si deux cases sont concommitantes
        /// </summary>
        /// <param name="c1">La première coordonée à tester</param>
        /// <param name="c2">La deuxième coordonnée à tester</param>
        /// <returns>Rend vrai si les deux coordonnées sont côte à côte, faux sinon</returns>
        public static bool aCote(Coord c1, Coord c2)
        {
            return (((c1.x == c2.x) && ((c1.y == c2.y + 1) || (c1.y == c2.y - 1))) || ((c1.y == c2.y) && ((c1.x == c2.x + 1) || (c1.x == c2.x - 1)))); 
        }

        /// <summary>
        /// surcharge de ==
        /// </summary>
        /// 
        public static bool operator ==(Coord c1, Coord c2)
        {
            return (c1.x == c2.x) && (c1.y == c2.y);
        }

        /// <summary>
        /// surcharge de !=
        /// </summary>
        /// 
        public static bool operator !=(Coord c1, Coord c2)
        {
            return (c1.x != c2.x) || (c1.y != c2.y);
        }

    }
}
