using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Runtime.Serialization;
using System.Runtime.Serialization.Formatters.Binary;
using System.IO;
    
namespace Code
{
    [Serializable]  
    public class Jeu : iJeu
    {                        
        public Carte Carte { get; set; }
        public Joueur J1 { get; set; }
        public Joueur J2 { get; set; }
        private bool j1Joue;
        public Joueur JActif { get; set; }
        public Joueur JVainqueur { get; set; }
        public int NbToursActuels { get; set; }
        private int nbActions;
        public int NbTours { get; set; }
        public bool PartieFinie { get; set; }
        public FabriqueCase fab { get; set; }

        [NonSerialized]
        public static Jeu INSTANCE = new Jeu();


  

        public Jeu()
        {
           PartieFinie = false;
        }

        public Joueur recupAdversaire(){
            if (j1Joue) return J2;
            return J1;
        }
    
        public void lancerJeu(Carte c, Joueur j1, Joueur j2, int nombreTours, FabriqueCase fab)
        {            
            this.Carte = c;
            this.J1 = j1;
            this.J2 = j2;
            this.fab = fab;
            NbTours = nombreTours;
            nbActions = 2;
            NbToursActuels = 1;

            //choix joueurs au hasard
            if (RandomNumber(0, 100) < 50)
            {
                JActif = j1;
                j1Joue = true;
            }
            else
            {
                JActif = j2;
                j1Joue = false;
            }

        }

        public int[] suggestionDeplacement(Unite u)
        {
            return u.deplacementPossibles();
        }

        public bool finTour()
        {
            //Si il y a une fin de partie
            if (J1.Peuple.nombreUnitesRestantes() == 0 || J2.Peuple.nombreUnitesRestantes() == 0 || nbActions == (NbTours*2 + 1))
            {
                PartieFinie = true;
                finPartie();
                return false;
            }
                
            nbActions++;
            if ((nbActions % 2) == 0)
                    NbToursActuels++;


            JActif.Peuple.remettrePtDeplacement();

            //sinon on passe au joueur suivant
            //Si c'était joueur1 qui jouait c'est au tour du joueur2
            if (j1Joue == true)
            {
                j1Joue = false;
                JActif = J2;
            }
            //Sinon c'est au tour du joueur1
            else
            {
                j1Joue = true;
                JActif = J1;
            }

            return true;

        }


        public Joueur finPartie()
        {
            //Plusieurs cas de figure
            if (NbToursActuels == NbTours) 
            {
                if (J1.Score >= J2.Score) 
                {
                    JVainqueur = J1;
                }
                else
                {
                    JVainqueur = J2;
                }

                return JVainqueur;
            }
            else 
            {
                //Un des deux joueurs est mort
                if (J1.Peuple.nombreUnitesRestantes() == 0)
                {
                    JVainqueur = J2;
                }
                else 
                {
                    JVainqueur = J1;
                }
            
                return JVainqueur;
            }
            
        }

        public void updateScore()
        {
            int score = 0;
            score = calculeScore(J1);

            J1.Score = score;

            score = calculeScore(J2);
            J2.Score = score;

        }

        public int calculeScore(Joueur j)
        {
            return j.calculeScore();

        }

        private int RandomNumber(int min, int max)
        {
            Random random = new Random();
            return random.Next(min, max);
        }

        public void sauvegarder(String nomFich)
        {          
            IFormatter formatter = new BinaryFormatter();
            Stream stream = new FileStream(nomFich, FileMode.Create, FileAccess.Write, FileShare.None);
            formatter.Serialize(stream, this);
            stream.Close();
        }

        public void charger(String nomFich)
        {
            IFormatter formatter = new BinaryFormatter();
            Stream stream = new FileStream(nomFich, FileMode.Open, FileAccess.Read, FileShare.Read);
            Jeu obj = (Jeu)formatter.Deserialize(stream);
            stream.Close();

            this.Carte = obj.Carte;
            this.J1 = obj.J1;
            this.J2 = obj.J2;
            this.j1Joue = obj.j1Joue;
            this.JActif = obj.JActif;
            this.JVainqueur = obj.JVainqueur;
            this.NbToursActuels = obj.NbToursActuels;
            this.nbActions = obj.nbActions;
            this.PartieFinie = obj.PartieFinie;
            this.NbTours = obj.NbTours;
            this.fab = obj.fab;
         }
    }
}
