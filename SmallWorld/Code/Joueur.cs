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
        private iPeuple peuple;

        public iPeuple iPeuple
        {
            get
            {
                return peuple;
            }
        }

        public int Score
        {
            get
            {
                return score;
            }
        }

        public String Nom
        {
            get
            {
                return nom;
            }
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
    }
}
