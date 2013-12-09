using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Modelisation
{
    public class Joueur : iJoueur
    {
        private int nom;
        private int score;

        public iPeuple iPeuple
        {
            get
            {
                throw new System.NotImplementedException();
            }
            set
            {
            }
        }

        public String getNom()
        {
            throw new System.NotImplementedException();
        }

        public int getScore()
        {
            throw new System.NotImplementedException();
        }

        public Peuple getPeuple()
        {
            throw new System.NotImplementedException();
        }

        public void setNom(string nom)
        {
            throw new System.NotImplementedException();
        }

        public void setScore(int score)
        {
            throw new System.NotImplementedException();
        }

        public void setPeuple(string peuple)
        {
            throw new System.NotImplementedException();
        }
    }
}
