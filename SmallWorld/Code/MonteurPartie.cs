using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Wrapper;

namespace Code
{
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

        private int nbCases;
        private int tailleCarte;
        private int nbTours;
        private int nbUnites;
        private int[] carte1D;

        private Carte carte;
        private Jeu jeu;

        public int[] Carte1D
        {
            get
            {
                return carte1D;
            }
            set
            {
                carte1D = value;
            }
        }

        public Joueur Joueur1
        {
            get
            {
               return joueur1;
            }
        }

        public Joueur Joueur2
        {
            get
            {
                return joueur2;
            }
        }

        public int NbCases
        { 
            get
            {
                return nbCases;
            }
        
        }
        public int TailleCarte
        {
            get
            {
                return tailleCarte;
            }

        }
        public int NbTours
        {
            get
            {
                return nbTours;
            }

        }
        public int NbUnites
        {
            get
            {
                return nbUnites;
            }

        }
       public Carte Carte
        {
            get
            {
                return carte;
            }
           set
            {
                carte = value;
            }
        }

       public Jeu Jeu
       {
           get
           {
               return jeu;
           }
           set
           {
               jeu = value;
           }
       }

       // ATTENTION !!
       // penser à transmettre le tableau de case à FabriqueCase
       // AVANT de faire des appels à obtenirCase

       public int Difficulte
       {
           set
           {
               difficulte = value;
           }
           get
           {
               return difficulte;
           }   
       }
       public String J1
       {
           set
           {
               j1 = value;
           }
           get
           {
               return j1;
           }   
       }
       public String J2
       {
           set
           {
               j2 = value;
           }
           get
           {
               return j2;
           }   
       }
       public int P1
       {
           set
           {
               p1 = value;
           }
           get
           {
               return p1;
           }   
       }
       public int P2
       {
           set
           {
               p2 = value;
           }
           get
           {
               return p2;
           }   
       }

       public unsafe void creerCarte()
       {
           carte.definirTaille(tailleCarte);
           WrapperAlgo wrapperAlgo = new WrapperAlgo();

           int* tabCases1Dimension = wrapperAlgo.creationCarte(tailleCarte);
           int i;
           for (i = 0; i < tailleCarte * tailleCarte; i++)
               carte1D[i] = tabCases1Dimension[i];

           FabriqueCase.INSTANCE.setTabCases(ref carte1D);

           carte.creerCarte();
       }

       public unsafe void creerJoueurs(String j1, int p1, String j2, int p2, int nbUnitesParJoueurs) 
       {
           //Avant de creer les objets Joueurs il faut creer les Peuples
           //Avant de creer les Peuples il est nécessaire de connaitre l'endroit ou seront placees les unités
           WrapperAlgo wrapperAlgo = new WrapperAlgo();
           int* tab1D = null;
           int i = 0;
           for (i = 0; i < tailleCarte * tailleCarte; i++)
               tab1D[i] = carte1D[i];
           
           // On obtient la position du Joueur1 et du Joueur2
           int* positionsJ = wrapperAlgo.positionnerJoueurs(tab1D, tailleCarte);
           Coord posJ1 = new Coord(positionsJ[0]);
           Coord posJ2 = new Coord(positionsJ[1]);

           Peuple peupleJ1 = FabriquePeuple.INSTANCE.creerPeuple(p1, nbUnitesParJoueurs,posJ1);
           Peuple peupleJ2 = FabriquePeuple.INSTANCE.creerPeuple(p2, nbUnitesParJoueurs, posJ2);

           joueur1.Nom = j1;
           joueur2.Nom = j2;
           joueur1.Peuple = peupleJ1;
           joueur2.Peuple = peupleJ2;

       }
 
       /*public Peuple creerPeuple(Coord c, int nbUnites)
       {
           return null;
       }*/

       public bool initialiser()
       {
           //Arnaud me send la diff le nom J1 J2 P1 P2 
           carte = new Carte();
           jeu = new Jeu();
           Joueur joueur1 = new Joueur();
           Joueur joueur2 = new Joueur();

           switch(difficulte)
           {
               case Constants.DEMO :
                     tailleCarte = Constants.DEMO;
               break;

               case Constants.PETITE :
                     tailleCarte = Constants.PETITE;
               break;

               case Constants.NORMALE :
                    tailleCarte = Constants.NORMALE;
               break;

               default :
               break;
           }

           carte1D = new int[tailleCarte * tailleCarte];
           // On a toutes les infos pour créer la Carte
           creerCarte();

           nbUnites = carte.getNbUnites();

           //Il faut maintenant créer les joueurs
           //Commencons par créer le Peuple d'un joueur
           creerJoueurs(j1, p1, j2, p2, nbUnites);

           return true;
       }


        private MonteurPartie()
        {
            //rien ?
        }

        public bool restaurer()
        {
            throw new System.NotImplementedException();
        }

        public bool sauvegarder()
        {
            throw new System.NotImplementedException();
        }

        public void definirTaille(int taille){

        }


        public void definirRace(String nom){


        }
       
        public void lancerJeu(Carte c, Joueur j1, Joueur j2, int nbTours){


        }
    }
}
