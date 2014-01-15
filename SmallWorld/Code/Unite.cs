using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Wrapper;

namespace Code
{
    [Serializable]
    public unsafe class Unite : iUnite
    {
        private int pointVie;
        private int pointDefense;
        private int pointAttaque;
        private double pointDeDeplacement;
        private Peuple peuple;
        private Coord position;

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
        }


        public int[] deplacementPossibles()
        {
            WrapperAlgo wrapperAlgo = new WrapperAlgo();
            int tailleCarte = Jeu.INSTANCE.Carte.getTaille();
            int* tabCases = wrapperAlgo.Algo_mymalloc(tailleCarte);
            for (int i = 0; i < tailleCarte * tailleCarte; i++)
                tabCases[i] = Jeu.INSTANCE.carte1D[i];

            int[] tabRes = new int[Jeu.INSTANCE.Carte.getTaille() * Jeu.INSTANCE.Carte.getTaille()];
            int* tabDeplacement;

            if (this.peuple is PeupleGaulois)
            {
                if (pointDeDeplacement == 1)
                {
                    tabDeplacement = wrapperAlgo.Algo_deplacementPossibleGaulois1(tabCases, Jeu.INSTANCE.Carte.getTaille(), position.getIndiceTab1Dimension());
                }
                //Il lui reste 0.5 points de depl et donc seuls les cases Plaines VOISINES SONT ok
                else
                {
                    tabDeplacement = wrapperAlgo.Algo_deplacementPossibleGaulois2(tabCases, Jeu.INSTANCE.Carte.getTaille(), position.getIndiceTab1Dimension());
                }
            }
            else if (this.peuple is PeupleNain)
            {
                tabDeplacement = wrapperAlgo.Algo_deplacementPossibleNainInit(tabCases, Jeu.INSTANCE.Carte.getTaille(), this.position.getIndiceTab1Dimension());
            }
            // ON est sur que se sera un Viking 
            else
            {
                tabDeplacement = wrapperAlgo.Algo_deplacementPossibleVikingInit(tabCases, Jeu.INSTANCE.Carte.getTaille(), this.position.getIndiceTab1Dimension());
            }

            for (int i = 0; i < Jeu.INSTANCE.Carte.getTaille() * Jeu.INSTANCE.Carte.getTaille(); i++)
            {
                tabRes[i] = tabDeplacement[i];
            }

            return tabRes;

        }


        public bool attaquer(Coord caseAttaquee)
        {
            Joueur adversaire = Jeu.INSTANCE.recupAdversaire();
            List<Unite> lesAdversaires = adversaire.Peuple.getUnitesPos(caseAttaquee);
            int nbCombatsAFaire = lesAdversaires.Count;

            int i = 0;
            bool gagne = false;
            //on génère un nombre de combat aléatoire
            int nbRoundCombat;

            for (i = 0; i < nbCombatsAFaire; i++)
            {
                Unite meilleureU = adversaire.Peuple.meilleureUnite(caseAttaquee);
                Random r = new Random();
                nbRoundCombat = 3 + r.Next(Math.Max(this.PointVie, meilleureU.PointVie));

                gagne = this.combat(meilleureU, nbRoundCombat);

                //L'unité attaquante est morte, le combat est perdu 
                if (this.PointVie == 0) //l'attaquant a perdu, son unité meurt
                {
                    (Jeu.INSTANCE.JActif).Peuple.Unites.Remove(this);
                    return gagne;
                }
                //Sinon l'unité attaqué est morte
                if (meilleureU.pointVie == 0)
                {
                    adversaire.Peuple.Unites.Remove(meilleureU);
                }

            }
            //Si l'unite attaquante a survecu a toutes les unites adverses on a gagné le combat
            gagne = true;
            return gagne;

        }


        public bool combat(Unite adverse, int nbCombats)
        {
            while ((nbCombats > 0) && (pointVie > 0) && (adverse.pointVie > 0))
            {
                double probaAttaquantPerd = 0.5; //Par défaut on est à 50%
                if (PointAttaque != adverse.PointDefense)
                {
                    double coefficient = (double)(Math.Abs(PointAttaque - adverse.PointDefense)) / (double)(Math.Max(PointAttaque, adverse.PointDefense));
                    double ponderation = coefficient * 0.5;

                    if (PointAttaque > adverse.PointDefense)
                        probaAttaquantPerd = 0.5 - ponderation;

                    if (PointAttaque < adverse.PointDefense)
                        probaAttaquantPerd = 0.5 + ponderation;
                }

                Random r = new Random();
                if (r.Next(100) < probaAttaquantPerd*100)
                {
                    PointVie--;
                }
                else
                {
                    adverse.PointVie--;
                }
                nbCombats--;
            }
            //On a gagne le combat, il faut retourner dans la fonction pour enchainer les autres
            if (pointVie > 0)
                return true;

            //On a perdu le combat
            if (adverse.pointVie > 0)
                return false;

            return true;
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
                bool combatGagne = true;

                if ((this.peuple is PeupleGaulois) && (Jeu.INSTANCE.fab.obtenirCase(caseDeplacement) is Plaine))
                    PointDeplacement = PointDeplacement - 0.5;
                else
                    pointDeDeplacement--;

                //Verifie s'il y'a un adversaire sur la case et donc combat
                foreach (var uni in Jeu.INSTANCE.recupAdversaire().Peuple.Unites)
                {
                    //le == marche pas je sais pas pk
                    if (uni.Position.Equals(caseDeplacement))
                    {
                        combatGagne = attaquer(caseDeplacement);
                        //Il faut sortir de foreach
                        break;
                    }
                }
                //Si on a gagné le combat, on se deplace sur la case gagnée
                if (combatGagne)
                    this.position = caseDeplacement;

            }

        }

        public int score()
        {


            if (this.peuple is PeupleGaulois)
            {
                if (Jeu.INSTANCE.fab.obtenirCase(position) is Plaine)
                {
                    return 2;
                }
                else if (Jeu.INSTANCE.fab.obtenirCase(position) is Montagne)
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
                if (Jeu.INSTANCE.fab.obtenirCase(position) is Foret)
                {
                    return 2;
                }
                else if (Jeu.INSTANCE.fab.obtenirCase(position) is Plaine)
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
                    Coord pos = new Coord(position.X - 1, position.Y);
                    if (Jeu.INSTANCE.fab.obtenirCase(pos) is Eau)
                        sc++;

                }

                //Si pas a droite
                if (position.X < Jeu.INSTANCE.Carte.getTaille() - 1)
                {
                    Coord pos = new Coord(position.X + 1, position.Y);
                    if (Jeu.INSTANCE.fab.obtenirCase(pos) is Eau)
                        sc++;


                }

                //Si pas en haut
                if (position.Y > 0)
                {
                    Coord pos = new Coord(position.X, position.Y - 1);
                    if (Jeu.INSTANCE.fab.obtenirCase(pos) is Eau)
                        sc++;

                }

                //Si pas en bas
                if (position.Y < Jeu.INSTANCE.Carte.getTaille() - 1)
                {
                    Coord pos = new Coord(position.X, position.Y + 1);
                    if (Jeu.INSTANCE.fab.obtenirCase(pos) is Eau)
                        sc++;

                }

                if (Jeu.INSTANCE.fab.obtenirCase(position) is Montagne || Jeu.INSTANCE.fab.obtenirCase(position) is Foret || Jeu.INSTANCE.fab.obtenirCase(position) is Plaine)
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
