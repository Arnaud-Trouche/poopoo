using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Code
{
    /// <summary>
    /// Classe représenant une Carte de jeu.
    /// Elle contient le nombre de Tours, le nombre d'unités par joueur
    /// et la taille en nombre de case par coté de la carte. 
    /// </summary>
    [Serializable]
    public class Carte : iCarte
    {
        private iStrategieCarte strategie;
        private int nbTours;
        private int nbUnites;
        private int taille;


        /// <summary>
        /// Définit la taille de la Carte en fonction de la difficulté.
        /// </summary>
        /// <param name="taille">Taille de la Carte</param>
        public void definirTaille(int taille)
        {
            this.taille = taille;
            //Switch sur la taille pour sélectionner la bonne stratégie
            switch (taille)
            {
                
                //Appelle la bonne stratégie
                case Code.Constants.DEMO:
                    strategie = new CarteDemo();
                    break;

                case Code.Constants.PETITE:
                    strategie = new CartePetite();
                    break;

                case Code.Constants.NORMALE:
                    strategie = new CarteNormale();
                    break;
            }
            //affecte les attributs de la carte grâce à la StrategieCarte
            this.nbTours = strategie.getNbTours();
            this.nbUnites = strategie.getNbUnites();
        }

        public int NbUnites
        {
            get
            {
                return nbUnites;

            }
            set
            {
                nbUnites = value;
            }
        }

        public int NbTours
        {
            get
            {
                return nbTours;
            }
            set
            {
                nbTours = value;
            }
        }

        /// <summary>
        /// Renvoie la taille de la Carte.
        /// </summary>
        /// <returns>La taille de la Carte</returns>
        public int getTaille()
        {
            return taille;
        }
    }
}
