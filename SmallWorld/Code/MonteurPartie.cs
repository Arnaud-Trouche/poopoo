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

        private Carte carte;
        private Jeu jeu;

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
           int[] tab = new int[tailleCarte * tailleCarte];
           int i;
           for (i = 0; i < tailleCarte * tailleCarte; i++)
               tab[i] = tabCases1Dimension[i];

           FabriqueCase.INSTANCE.setTabCases(ref tab);

           carte.creerCarte();
       }

       public Peuple creerPeuple(Coord c, int nbUnites)
       {
           return null;
       }

       public bool initialiser()
       {
           //Arnaud me send la diff le nom J1 J2 P1 P2 
           Carte carte = new Carte();
           Jeu jeu = new Jeu();
           
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

           creerCarte();
          // creerPeuple()
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
