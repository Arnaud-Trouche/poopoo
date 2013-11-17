using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Modelisation
{
    public class Carte : iCarte
    {
        private Object strategie;
        private Case[] listeCases;
        private int nbTour;
        private int nbUnites;
    
        public void creerCarte()
        {
            throw new System.NotImplementedException();
        }

        public void definirTaille(int taille)
        {
            throw new System.NotImplementedException();
        }

        public int getNbUnites()
        {
            throw new System.NotImplementedException();
        }

        public int getNbTours()
        {
            throw new System.NotImplementedException();
        }
    }
}
