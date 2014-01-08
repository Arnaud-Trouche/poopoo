using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Code
{
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

        public Joueur(String name, Peuple hisPeuple)
        {
            nom = name;
            peuple = hisPeuple;
        }
        public void augmenterScore(int delta)
        {
            this.score += delta;
        }

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
