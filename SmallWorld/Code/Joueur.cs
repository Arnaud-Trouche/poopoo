using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Code
{
    /// <summary>
    /// Classe répresentant un Joueur. 
    /// Un joueur à un nom, un score et 
    /// possède un Peuple (liste d'unités). 
    /// </summary>
    [Serializable]
    public class Joueur : iJoueur
    {
        private String nom;
        private int score;
        private Peuple peuple;

        public Peuple Peuple
        {
            get
            {
                return peuple;
            }
            set
            {
                peuple = value;
            }
        }

        public int Score
        {
            get
            {
                return score;
            }
            set 
            {
                score = value;
            }
        }

        public String Nom
        {
            get
            {
                return nom;
            }
            set
            {
                nom = value;
            }
        }
        public Joueur()
        {
         
        }

        /// <summary>
        /// Calcule le score du joueur qui est la somme des scores de ses unités.
        /// </summary>
        /// <returns>Le score du joueur.</returns>
        public int calculeScore()
        {
            int score = 0;

            foreach(Unite unite in Peuple.Unites)
            {
                score += unite.score();
            }

            return score;
        }
    }
}
