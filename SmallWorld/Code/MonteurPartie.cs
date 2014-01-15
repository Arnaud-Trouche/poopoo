using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Wrapper;

namespace Code
{
    /// <summary>
    /// Monteur partie créer les différents composants qui constituent une partie.
    /// Création de la carte, des joueurs, des peuples, ect...
    /// </summary>
    [Serializable]
    public class MonteurPartie : iMonteurPartie
    {
        public static MonteurPartie INSTANCE = new MonteurPartie();
        private int difficulte;
        private String j1;
        private String j2;
        private int p1;
        private int p2;
        private Joueur joueur1;
        private Joueur joueur2;
        private FabriqueCase fab;

        private int tailleCarte;
        private int nbTours;
        private int nbUnites;
        [NonSerialized]
        private WrapperAlgo wrapperAlgo;

        //Les cartes unidimensionnelles servent à faire la communication 
        //avec le wrapper qui renvoie des tableauxv en int*
        private int[] carte1D;

        private Carte carte;


        public int Difficulte {
            get
            {
                return difficulte;
            }
            set
            {
                difficulte = value;
            }
        }   
        public String J1 {
            get
            {
                return j1;
            }
            set
            {
                j1 = value;
            }
        }
        public String J2
        {
            get
            {
                return j2;
            }
            set
            {
                j2 = value;
            }
        }
        public int P1
        {
            get
            {
                return p1;
            }
            set
            {
                p1 = value;
            }
        }
        public int P2
        {
            get
            {
                return p2;
            }
            set
            {
                p2 = value;
            }
        }

       /// <summary>
       /// Initialise les éléments du Monteur (Carte, Joueurs, Peuples).
       /// Créer la carte de la bonne taille et créer les joueurs ainsi 
       /// que leur peuple avec le bon nombre d'unités.
       /// </summary>
       /// <returns>Renvoie vrai si l'initialisation a réussi, faux sinon.</returns>
       public bool initialiser()
       {
           //Arnaud me send la diff le nom J1 J2 P1 P2 
           carte = new Carte();
           joueur1 = new Joueur();
           joueur2 = new Joueur();
           fab = new FabriqueCase();

           //Récupère la bonne taille de carte en fonction
           //de la difficulté (Stratégie)
           switch (difficulte)
           {
               case Constants.DEMO:
                   tailleCarte = Constants.DEMO;
                   break;

               case Constants.PETITE:
                   tailleCarte = Constants.PETITE;
                   break;

               case Constants.NORMALE:
                   tailleCarte = Constants.NORMALE;
                   break;

               default:
                   break;
           }
           //Carte servant à faire le lien avec le Wrapper
           carte1D = new int[tailleCarte * tailleCarte];
           // On a toutes les infos pour créer la Carte
           creerCarte();

           //Il faut maintenant créer les joueurs
           //Commencons par créer le Peuple d'un joueur
           creerJoueurs(j1, p1, j2, p2, nbUnites);
           //Tous les éléments sont crées, on peut lancer le jeu.
           lancerJeu();
           return true;
       }

       /// <summary>
       /// Créer la carte de la bonne taille.
       /// </summary>
       public unsafe void creerCarte()
       {

           carte.definirTaille(tailleCarte);
           //Récupération des caractéristiques
           this.tailleCarte = carte.getTaille();
           Jeu.INSTANCE.Carte = new Carte();
           Jeu.INSTANCE.Carte.definirTaille(tailleCarte);
           //On a la carte donc on a accès au nombre de tours et le nombre d'unités
           this.nbTours = carte.NbTours;
           this.nbUnites = carte.NbUnites;

           //Invoque le wrapper qui créer la carte de la bonne taille.
           int* tab1D = wrapperAlgo.creationCarte(tailleCarte);
           int i;
           //Copie le tableau du Wrapper dans le tableau compatible C sharp
           for (i = 0; i < tailleCarte * tailleCarte; i++)
               carte1D[i] = tab1D[i];

           Jeu.INSTANCE.carte1D = carte1D;
           //Transmet le tableau des types de cases à la Fabrique
           fab.setTabCases(ref carte1D);

       }

       /// <summary>
       /// Creer les deux joueurs de la partie.
       /// </summary>
       /// <param name="j1">Nom du joueur 1</param>
       /// <param name="p1">Peuple du joueur 1</param>
       /// <param name="j2">Nom du joueur 2</param>
       /// <param name="p2">Peuple du joueur 2</param>
       /// <param name="nbUnitesParJoueurs">Nombre d'unités par joueurs</param>
       public unsafe void creerJoueurs(String j1, int p1, String j2, int p2, int nbUnitesParJoueurs) 
       {
          int* tab1D = transformePointeur(carte1D); 
           // On obtient la position du Joueur1 et du Joueur2
           int* positionsJ = wrapperAlgo.positionnerJoueurs(tab1D, tailleCarte);
           Coord posJ1 = new Coord(positionsJ[0]);
           Coord posJ2 = new Coord(positionsJ[1]);

           //Appelle la fabrique pour créer peuples (et donc leurs unités)
           Peuple peupleJ1 = FabriquePeuple.INSTANCE.creerPeuple(p1, nbUnitesParJoueurs,posJ1);
           Peuple peupleJ2 = FabriquePeuple.INSTANCE.creerPeuple(p2, nbUnitesParJoueurs, posJ2);

           joueur1.Nom = j1;
           joueur2.Nom = j2;
           joueur1.Peuple = peupleJ1;
           joueur2.Peuple = peupleJ2;

       }
        /// <summary>
        /// Constructeur de MonteurPartie
        /// </summary>
        private MonteurPartie()
       {
           wrapperAlgo = new WrapperAlgo();
       }
        
       /// <summary>
       /// Lance le jeu car tout a été monté. Donne le relais à la classe Jeu.
       /// </summary>
        public void lancerJeu(){
            Jeu.INSTANCE.lancerJeu(carte, joueur1, joueur2, nbTours, fab, carte1D);
        }

        /// <summary>
        /// Transforme un tableau c# [] en pointeur pour le wrapper
        /// </summary>
        /// <param name="tab">Le tableau c# à transformer (de taille tailleCarte*tailleCarte*sizeof(int))</param>
        /// <returns></returns>
        public static unsafe int* transformePointeur(int[] tab)
        {
            WrapperAlgo wrapperAlgo = new WrapperAlgo();
            int tailleCarte = Jeu.INSTANCE.Carte.getTaille();  
            //Réalise un malloc pour allouer le tableau représentant la carte en unidimensionnel
            int* tabCases = wrapperAlgo.Algo_mymalloc(tailleCarte); 
            //Affecte cases par cases le tableau nouvellement alloué par la carte unidimensionnel 
            for (int i = 0; i < tailleCarte * tailleCarte; i++)
                tabCases[i] = Jeu.INSTANCE.carte1D[i];
            return tabCases;
        }
    }
}
