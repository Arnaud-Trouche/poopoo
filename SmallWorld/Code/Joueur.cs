using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Code
{
    public class Joueur : iJoueur
    {
        private int nom;
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

        public int Nom
        {
            get
            {
                return nom;
            }
        }

        public void augmenterScore(int delta)
        {
            this.score += delta;
        }
    }
}
