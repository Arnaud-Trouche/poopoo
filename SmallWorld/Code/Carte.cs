using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Code
{
    public class Carte : iCarte
    {
        private iStrategieCarte strategie;
        private int nbTours;
        private int nbUnites;
        private static int TAILLE;

        //public void creerCarte()
        //{
        //    strategie.creerCarte();
        //}

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

        public static int getTaille()
        {
            return TAILLE;
        }
    }
}
