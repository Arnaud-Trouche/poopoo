using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Code
{
    public class Carte : iCarte
    {
        private iStrategieCarte strategie;
        private iCase[] listeCases;
        private int nbTour;
        private int nbUnites;
        private static int TAILLE;

        public void creerCarte()
        {
            strategie.creerCarte();
        }

        public void definirTaille(int taille)
        {
            Carte.TAILLE = taille;
            switch (taille)
            {
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
            this.nbTour = strategie.getNbTours();
            this.nbUnites = strategie.getNbUnites();
        }

        public int getNbUnites()
        {
            return nbUnites;
        }

        public int getNbTours()
        {
            return nbTour;
        }

        public static int getTaille()
        {
            return TAILLE;
        }
    }
}
