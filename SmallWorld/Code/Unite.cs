﻿using System;
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
        private double pointDeDeplacement;
        private Peuple peuple;
        private Coord position;
        protected WrapperAlgo wrapperAlgo;
        private double* tabCout;
        private int* tabDeplacement;
        protected int* tabCarte;

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
        public double PointDeplacement
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
        public Peuple Peuple    // the peuple property
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
            int* tabDeplacement;

            if (this.peuple is PeupleGaulois)
            {
                tabDeplacement = wrapperAlgo.Algo_deplacementPossibleNainInit(MonteurPartie.INSTANCE.Tab1D, Carte.getTaille(), position.getIndiceTab1Dimension());
            }
            else if (this.peuple is PeupleNain)
            {
                tabDeplacement =  wrapperAlgo.Algo_deplacementPossibleNainInit(MonteurPartie.INSTANCE.Tab1D, Carte.getTaille(), this.position.getIndiceTab1Dimension());
            }
            // ON est sur que se sera un Viking 
            else 
            {
                tabDeplacement =  wrapperAlgo.Algo_deplacementPossibleVikingInit(MonteurPartie.INSTANCE.Tab1D, Carte.getTaille(), this.position.getIndiceTab1Dimension());
            }

            int i = 0; 
            for(i= 0; i < Carte.getTaille() * Carte.getTaille(); i++)
            {
                tabRes[i] = tabDeplacement[i];
            }

            return tabRes;

        }
        

        public void attaquer(Coord caseAttaquee)
        {
            throw new System.NotImplementedException();
        }

        public void deplacer(Coord caseDeplacement)
        {
            this.position = caseDeplacement;
        }

        public void debutTour()
        {
            this.pointDeDeplacement = 1;
        }


    }
}
