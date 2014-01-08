using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Wrapper;

namespace Code
{
    public class Jeu : iJeu
    {
        private Carte c;
        private Joueur j1;
        private Joueur j2;
        private bool j1Joue;
        private Joueur jActif;
        private Joueur jVainceur;
        private int nbActions;
        private int nbTours;
        private int nbToursActuels;
        public static Jeu INSTANCE = new Jeu();
        private WrapperAlgo wrapperAlgo;


        public Joueur JVainceur
        {
            get
            {
                return jVainceur;
            }
            set
            {
                jVainceur = value;
            }
        }


        public int NbToursActuels
        {
            get
            {
                return nbToursActuels;
            }
            set
            {
                nbToursActuels = value;
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
        public int NbActions
        {
            get
            {
                return nbActions;
            }
            set
            {
                nbActions = value;
            }

        }
        public Joueur JActif
        {
            get
            {
                return jActif;
            }
            set
            {
                jActif = value;
            }

        }
        public Joueur J2
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

        public Joueur J1
        {
            get
            {
                return j1;
            }
            set
            {
                j1 = value;
            }

        }
        public Carte Carte
        {
            get
            {
                return c;
            }
            set
            {
                c = value;
            }

        }

        public Jeu()
       {
           wrapperAlgo = new WrapperAlgo();
        }

        public Joueur recupAdversaire(){
            if (j1Joue) return j2;
            return j1;
        }
    
        public void lancerJeu(Carte c, Joueur j1, Joueur j2, int nombreTours)
        {
            
            this.c = c;
            this.j1 = j1;
            this.j2 = j2;
            nbTours = nombreTours;
            nbActions = 0;
            nbToursActuels = 0;

            //choix joueurs au hasard
            if (RandomNumber(0, 100) < 50)
            {
                jActif = j1;
                j1Joue = true;
            }
            else
            {
                jActif = j2;
                j1Joue = false;
            }

        }



        public Joueur finPartie()
        {
            //Plusieurs cas de figure
            if (nbToursActuels == nbTours) 
            {
                if (j1.Score >= j2.Score) 
                {
                    jVainceur = j1;
                }
                else
                {
                    jVainceur = j2;
                }

                return jVainceur;
            }
            else 
            {
                //Un des deux joueurs est mort
                if (j1.Peuple.nombreUnitesRestantes() == 0)
                {
                    jVainceur = j2;
                }
                else 
                {
                    jVainceur = j1;
                }
            
                return jVainceur;
            }
            
        }


        private int RandomNumber(int min, int max)
        {
            Random random = new Random();
            return random.Next(min, max);
        } 

    }
}
