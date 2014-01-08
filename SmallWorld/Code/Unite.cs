using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Wrapper;

namespace Code
{
    public unsafe class Unite : iUnite
    {
        private int pointVie;
        private int pointDefense;
        private int pointAttaque;
        private int pointDeDeplacement;
        private Peuple peuple;
        private Coord position;
        protected WrapperAlgo wrapperAlgo;
        private double* tabCout;
        private int* tabDeplacement;
        protected int* tabCarte;

        /**
        * @fn TabCarte
        * @brief Properties pour l'attribut tabCarte
        */
        public int* TabCarte
        {
            get
            {
                return tabCarte;
            }
            set
            {
                tabCarte = value;
            }
        }

        /**
         * @fn CartePartie
         * @brief Properties pour l'attribut tabDeplacement
         */
        public int* TabDeplacement
        {
            get
            {
                return tabDeplacement;
            }
            set
            {
                tabDeplacement = value;
            }
        }

        /**
         * @fn TabCout
         * @brief Properties pour l'attribut tabCout
         */
        public double* TabCout
        {
            get
            {
                return tabCout;
            }
            set
            {
                tabCout = value;
            }
        }


        public int PointVie
        {
            get
            {
                return pointVie;
            }
            set
            {
                pointVie = value;
            }
        }

        public int PointAttaque
        {
            get
            {
                return pointAttaque;
            }
            set
            {
                PointAttaque = value;
            }
        }
        public int PointDefense
        {
            get
            {
                return pointDefense;
            }
            set
            {
                pointDefense = value;
            }
        }
        public int PointDeplacement
        {
            get
            {
                return pointDeDeplacement;
            }
            set
            {
                pointDeDeplacement = value;
            }
        }

        public int Peuple    // the peuple property
        {
            get
            {
                return peuple;
            }
        }

        public Coord Position    // the position property
        {
            get
            {
                return position;
            }
        }

        public Unite(Coord pos, Peuple peuple)
        {
            this.pointVie = 2;
            this.pointDefense = 1;
            this.pointAttaque = 2;
            this.position = pos;
            this.peuple = peuple;
            this.pointDeDeplacement = 0;
            wrapperAlgo = new WrapperAlgo();
        }

        public void action(Coord casecliquee)
        {
            Peuple p = Jeu.INSTANCE.recupAdversaire().Peuple;
            if (p.getUnite(casecliquee) != null)
            {
                attaquer(casecliquee);
            }
            else
            {
                deplacer(casecliquee);    
            }
        }
    
        public int[] deplacementPossibles()
        {
            int[] tabRes = new int[Carte.getTaille() * Carte.getTaille()];
            if (this.peuple is PeupleGaulois)
            {

            }

        }


        public void attaquer(Coord caseAttaquee)
        {
            throw new System.NotImplementedException();
        }

        public void deplacer(Coord caseDeplacement)
        {
            throw new System.NotImplementedException();
        }

        public void debutTour()
        {
            this.pointDeDeplacement = 1;
        }


    }
}
