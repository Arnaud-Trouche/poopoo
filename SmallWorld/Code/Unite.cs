
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Wrapper;

namespace Code
{
    /// <summary>
    /// Classe Unite qui gère les Unités et leurs caractéristiques. 
    /// Propose des méthodes gérant le déplacement, l'attaque et le score générer par une unité
    /// </summary>
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
        public Coord Position    {get;set;}

        /// <summary>
        /// Constructeur d'une Unité 
        /// Initialise les caractéristiques d'une unité par défaut
        /// </summary>
        /// <param name="pos">Position de l'unité créée</param>
        /// <param name="peuple">Peuple d'où provient l'unité</param>
        public Unite(Coord pos, Peuple peuple)
        {
            this.pointVie = 5;
            this.pointDefense = 1;
            this.pointAttaque = 2;
            this.position = pos;
            this.peuple = peuple;
            this.pointDeDeplacement = 1;
        }

        /// <summary>
        /// Renvoie un tableau unidimensionnel représentant les cases sur lesquelles une unité 
        /// est en mesure de se déplacer
        /// </summary>
        /// <returns>Tableau unidimensionnel des cases où l'unité peut se déplacer</returns>
        public int[] deplacementPossibles()
        {
            WrapperAlgo wrapperAlgo = new WrapperAlgo();
            int tailleCarte = Jeu.INSTANCE.Carte.getTaille();

            //Réalise un malloc pour allouer le tableau représentant la carte en unidimensionnel
            int* tabCases = wrapperAlgo.Algo_mymalloc(tailleCarte);

            //Affecte cases par cases le tableau nouvellement alloué par la carte unidimensionnel 
            for (int i = 0; i < tailleCarte * tailleCarte; i++)
                tabCases[i] = Jeu.INSTANCE.carte1D[i];

            //Initialisation du tableau qui va contenir les déplacements possibles
            int[] tabRes = new int[Jeu.INSTANCE.Carte.getTaille() * Jeu.INSTANCE.Carte.getTaille()];
            int* tabDeplacement;

            //On switch sur les types de Peuple
            //car comportement différent selon le peuple
            //Les Gaulois sont les seuls qui peuvent se déplacer possiblement 2fois car un
            //déplacement sur une case Plaine ne retire que 1/2 point de déplacement 
            if (this.peuple is PeupleGaulois)
            {
                //Si on au moins un pt de Déplacement, appel du Wrapper pur l'aglo des déplacements possibles des gaulois
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
            //Si peuple Nain
            else if (this.peuple is PeupleNain)
            {
                tabDeplacement = wrapperAlgo.Algo_deplacementPossibleNainInit(tabCases, Jeu.INSTANCE.Carte.getTaille(), this.position.getIndiceTab1Dimension());
            }
            //Sinon on est sur que se sera un Viking 
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

        /// <summary>
        /// Fonction attaquer : détermination du meilleur adversaire, du nombre de combat à faire
        /// et lance les combats.
        /// Retourne Vrai si l'unité attaqué a gagné le combat, faux sinon.
        /// </summary>
        /// <param name="caseAttaquee"></param>
        /// <returns></returns>
        public bool attaquer(Coord caseAttaquee)
        {
            //Récupère la liste des unités adverses sur la case attaqué et calcul le nombre de combat qu'il va falloir faire
            Joueur adversaire = Jeu.INSTANCE.recupAdversaire();
            List<Unite> lesAdversaires = adversaire.Peuple.getUnitesPos(caseAttaquee);
            int nbCombatsAFaire = lesAdversaires.Count;

            int i = 0;
            bool gagne = false;

            //on génère un nombre de combat aléatoire
            int nbRoundCombat;

            //Réalise le nombe de combats à faire = nombre d'unités ennemis sur la case attaquée
            for (i = 0; i < nbCombatsAFaire; i++)
            {
                //Sélectionne la meilleur unité ennemie
                Unite meilleureU = adversaire.Peuple.meilleureUnite(caseAttaquee);
                Random r = new Random();
                nbRoundCombat = 3 + r.Next(Math.Max(this.PointVie, meilleureU.PointVie)-1);

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

        /// <summary>
        /// Enclenche les affrontements entre l'attanquant et l'attaqué
        /// S'arrête si le nombre d'affrontements est atteint ou si une des deux unités est morte
        /// Renvoie Vrai si l'attaquant a gagné le combat et Faux sinon
        /// </summary>
        /// <param name="adverse">Adversaire que l'attaquant va défier</param>
        /// <param name="nbCombats">Nombre d'affrontements à réaliser entre les deux unités</param>
        /// <returns></returns>
        public bool combat(Unite adverse, int nbCombats)
        {
            Random rand = new Random();
            //ALGO POLY
            while ((nbCombats > 0) && (pointVie > 0) && (adverse.pointVie > 0))
            {
                double probaAttPerd = 0.5; 
                if (PointAttaque != adverse.PointDefense)
                {
                    double coeff = (double)(Math.Abs(PointAttaque - adverse.PointDefense)) / (double)(Math.Max(PointAttaque, adverse.PointDefense));
                    double pond = coeff * 0.5;

                    if (PointAttaque > adverse.PointDefense)
                        probaAttPerd = 0.5 - pond;

                    if (PointAttaque < adverse.PointDefense)
                        probaAttPerd = 0.5 + pond;
                }



                if (rand.Next(100) < probaAttPerd*100)
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

        /// <summary>
        /// Vérifie si le déplacement de l'unité sur la case voulue est possible (selon les points de déplacement, le peuple en question)
        /// enclenche un combat si le déplacement se fait sur une case occupée par des unités adverses.
        /// </summary>
        /// <param name="caseDeplacement">Case où l'on souhaite se déplacer </param>
        public void deplacer(Coord caseDeplacement)
        {
            bool deplacementPossible = false;
   
            //Si point de déplacement n'est pas nulle, on peut se déplacer
            if (pointDeDeplacement > 0)
            {
                deplacementPossible = true;
            }

            if (deplacementPossible)
            {
                bool combatGagne = true;
                //Verifie s'il y'a un adversaire sur la case et donc combat
                foreach (var uni in Jeu.INSTANCE.recupAdversaire().Peuple.Unites)
                {

                    if (uni.Position.Equals(caseDeplacement))
                    {
                        combatGagne = attaquer(caseDeplacement);
                        //Il faut sortir de foreach
                        break;
                    }
                }

                //Si on a gagné le combat, on se deplace sur la case gagnée
                //seulement s'il n'y a plus d'unité ennemies sur la case
                int nbUnitesAdversaires = 0;
                foreach (var uni in Jeu.INSTANCE.recupAdversaire().Peuple.Unites)
                {
                    if (uni.Position.Equals(caseDeplacement))
                    {
                        nbUnitesAdversaires++;
                    }
                }
                //test de vérif
                if (combatGagne && nbUnitesAdversaires == 0)
                {
                    this.position = caseDeplacement;
                    //Les unités perdent 1 point sauf si c'st un gaulois que se déplacent sur une Plaine
                    if ((this.peuple is PeupleGaulois) && (Jeu.INSTANCE.fab.obtenirCase(caseDeplacement) is Plaine))
                        PointDeplacement = PointDeplacement - 0.5;
                    else
                        pointDeDeplacement--;
                }

            }

        }

        /// <summary>
        /// Renvoie le score que génère une unité en fonction de son peuple, sa position sur la carte.
        /// </summary>
        /// <returns>Le score que l'unité génère.</returns>
        public int score()
        {

            //Switch sur les types de peuple
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
                //Les vikings engrenge un point de plus par case d'eau adjacente
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

        /// <summary>
        /// Remet le point de déplacement à l'unité (invoquée à cahque nouveau tour).
        /// </summary>
        public void debutTour()
        {
            this.pointDeDeplacement = 1;
        }


    }
}
