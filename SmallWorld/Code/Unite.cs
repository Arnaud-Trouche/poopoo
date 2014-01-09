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
            this.pointDeDeplacement = 1;
            wrapperAlgo = new WrapperAlgo();
        }

     
        public int[] deplacementPossibles()
        {
            int[] tabRes = new int[Carte.getTaille() * Carte.getTaille()];
            int* tabDeplacement;

            if (this.peuple is PeupleGaulois)
            {
                if (pointDeDeplacement == 1)
                {
                    tabDeplacement = wrapperAlgo.Algo_deplacementPossibleGaulois1(MonteurPartie.INSTANCE.Tab1D, Carte.getTaille(), position.getIndiceTab1Dimension());
                }
                //Il lui reste 0.5 points de depl et donc seuls les cases Plaines VOISINES SONT ok
                else
                {
                    tabDeplacement = wrapperAlgo.Algo_deplacementPossibleGaulois2(MonteurPartie.INSTANCE.Tab1D, Carte.getTaille(), position.getIndiceTab1Dimension());
                }
            }
            else if (this.peuple is PeupleNain)
            {
                tabDeplacement = wrapperAlgo.Algo_deplacementPossibleNainInit(MonteurPartie.INSTANCE.Tab1D, Carte.getTaille(), this.position.getIndiceTab1Dimension());
            }
            // ON est sur que se sera un Viking 
            else
            {
                tabDeplacement = wrapperAlgo.Algo_deplacementPossibleVikingInit(MonteurPartie.INSTANCE.Tab1D, Carte.getTaille(), this.position.getIndiceTab1Dimension());
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
            //TODO
        }

        public void deplacer(Coord caseDeplacement)
        {
            bool deplacementPossible = false;
            if (pointDeDeplacement > 0)
            {
                deplacementPossible = true;
            }

            if (deplacementPossible)
            {
                if ((this.peuple is PeupleGaulois)  && (FabriqueCase.INSTANCE.obtenirCase(caseDeplacement) is Plaine))
                        PointDeplacement = PointDeplacement - 0.5;
                else 
                    pointDeDeplacement--;

                this.position = caseDeplacement;

                Peuple p = Jeu.INSTANCE.recupAdversaire().Peuple;
                if (p.getUnite(position) != null)
                {
                    attaquer(position);
                }
               
            }

        }

        public int score()
        {
           

            if (this.peuple is PeupleGaulois)
            {
                if (FabriqueCase.INSTANCE.obtenirCase(position) is Plaine)
                {
                    return 2;
                }
                else if (FabriqueCase.INSTANCE.obtenirCase(position) is Montagne)
                {
                    return 0;
                }
                else
                {
                    return 1;
                }
            }
            else if (this.peuple is PeupleNain)
            {
                if (FabriqueCase.INSTANCE.obtenirCase(position) is Foret)
                {
                    return 2;
                }
                else if (FabriqueCase.INSTANCE.obtenirCase(position) is Plaine)
                {
                    return 0;
                }
                else
                {
                    return 1;
                }
            }
            //C'est forcement un Viking
            else
            {
            
                    int sc = 0;

                    //Si pas a gauche
                    if (position.X > 0)
                    { 
                        Coord pos = new Coord(position.X-1,position.Y);
                        if (FabriqueCase.INSTANCE.obtenirCase(pos) is Eau)
                            sc++;

                    }

                    //Si pas a droite
                    if (position.X < Carte.getTaille() - 2)
                    {
                        Coord pos = new Coord(position.X + 1, position.Y);
                        if (FabriqueCase.INSTANCE.obtenirCase(pos) is Eau)
                            sc++;


                    }

                    //Si pas en haut
                    if (position.Y > 0)
                    {
                        Coord pos = new Coord(position.X, position.Y - 1);
                        if (FabriqueCase.INSTANCE.obtenirCase(pos) is Eau)
                            sc++;

                    }

                    //Si pas en bas
                    if (position.Y < Carte.getTaille() - 2)
                    {
                        Coord pos = new Coord(position.X, position.Y + 1);
                        if (FabriqueCase.INSTANCE.obtenirCase(pos) is Eau)
                            sc++;

                    }

                    if (FabriqueCase.INSTANCE.obtenirCase(position) is Montagne || FabriqueCase.INSTANCE.obtenirCase(position) is Foret || FabriqueCase.INSTANCE.obtenirCase(position) is Plaine)
                        sc++;

                    return sc;
                }

        }
        public void debutTour()
        {
            this.pointDeDeplacement = 1;
        }


    }
}
