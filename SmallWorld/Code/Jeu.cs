using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Runtime.Serialization;
using System.Runtime.Serialization.Formatters.Binary;
using System.IO;
    
namespace Code
{
    /// <summary>
    /// Classe modélisant le coeur du Jeu. Elle possède toutes les attributs
    /// nécessaires au déroulement d'une partie.  
    /// </summary>
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
        //Un tour de jeu est composée de deux actions (un par joueur)
        private int nbActions;
        public int NbTours { get; set; }
        public bool PartieFinie { get; set; }
        public FabriqueCase fab { get; set; }
        public int[] carte1D { get; set; }

        [NonSerialized]
        public static Jeu INSTANCE = new Jeu();


  
        /// <summary>
        /// Constructeur de Jeu. Indique que la Partie n'est pas terminée.
        /// </summary>
        public Jeu()
        {
           PartieFinie = false;
        }

        /// <summary>
        /// Retourne le joueur adversaire du joueur courant.
        /// </summary>
        /// <returns>Le joueur adversaire au joueur courant.</returns>
        public Joueur recupAdversaire(){
            if (j1Joue) return J2;
            return J1;
        }
    
        /// <summary>
        /// Méthode appelé par le Monteur en amont qui a créer et transmis tous les paramètres 
        /// nécessaires à la création d'une partie.
        /// </summary>
        /// <param name="c">La carte du Jeu</param>
        /// <param name="j1">Le Joueur 1</param>
        /// <param name="j2">Le Joueur 2</param>
        /// <param name="nombreTours">Le nombre de tours de la partie</param>
        /// <param name="fab">La FabriqueCase</param>
        /// <param name="tab">La carte unidimensionnelle qui sert pour le Wrapper</param>
        public void lancerJeu(Carte c, Joueur j1, Joueur j2, int nombreTours, FabriqueCase fab, int[] tab)
        {            
            this.Carte = c;
            this.J1 = j1;
            this.J2 = j2;
            this.fab = fab;
            this.carte1D = tab;
            NbTours = nombreTours;
            //Décalage du nombre d'actions et de tour pour 
            //être en cohérence avec l'affichage dans l'interface 
            //graphique (car Binding)
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

        /// <summary>
        /// Renvoie un tableau unidimensionnelle traduisant 
        /// les cases où une unité est capable de se déplacer 
        /// selon son état.
        /// </summary>
        /// <param name="u">L'unité considérée</param>
        /// <returns>Tableau unidimensionnelle des cases où l'unité peut se déplacer</returns>
        public int[] suggestionDeplacement(Unite u)
        {
            return u.deplacementPossibles();
        }

        /// <summary>
        /// Retourne Vrai si le tour terminée, faux sinon. Réalise des traitements 
        /// si le tour est finie (affectation nouveau qui va jouer joueur, ect).
        /// </summary>
        /// <returns>Retourne Vrai si le tour est terminée, faux sinon.</returns>
        public bool finTour()
        {

            //Un des joueurs sans unité ou alors nombre de d'actions épuisé
            if (J1.Peuple.nombreUnitesRestantes() == 0 || J2.Peuple.nombreUnitesRestantes() == 0 || nbActions == (NbTours*2 + 1))
            {
                PartieFinie = true;
                finPartie();
                return false;
            }

            //Les actions sont incrémentées dès qu'un joueur prend la main
            //donc un nbTour = 1/2 nbActions
            nbActions++;
            if ((nbActions % 2) == 0)
                    NbToursActuels++;

            //On remet le point de déplacement au joueur qui vient de jouer
            JActif.Peuple.remettrePtDeplacement();

            //On passe au joueur suivant
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

        /// <summary>
        ///  Retourne Vrai si la partie est terminée, faux sinon. Réalise des traitements 
        /// si la partie est finie (affectation joueur ayant gagné).
        /// </summary>
        /// <returns>Retourne Vrai si la partie est terminée, faux sinon.</returns>
        public Joueur finPartie()
        {
            //Plusieurs cas de figure
            //Nombre de tours épuisé
            if (NbToursActuels == NbTours) 
            {
                //Détermine le vainceur avec le score
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

        /// <summary>
        /// Met à jour le score des deux joueurs.
        /// </summary>
        public void updateScore()
        {
            int score = 0;
            score = calculeScore(J1);

            J1.Score = score;

            score = calculeScore(J2);
            J2.Score = score;

        }

        /// <summary>
        /// Calcule le score d'un joueur.
        /// </summary>
        /// <param name="j">Joueur voulue</param>
        /// <returns>Le score du Joueur</returns>
        public int calculeScore(Joueur j)
        {
            return j.calculeScore();

        }

        /// <summary>
        /// Fonction réalisant un rand entre deux nombre.
        /// </summary>
        /// <param name="min">Limite basse</param>
        /// <param name="max">Limite Haute</param>
        /// <returns>Un entier entre min et maw.</returns>
        private int RandomNumber(int min, int max)
        {
            Random random = new Random();
            return random.Next(min, max);
        }

        /// <summary>
        /// Serialize dans un fichier binaire, l'instance courante de jeu.
        /// </summary>
        /// <param name="nomFich">Le nom du fichier sauvegarde</param>
        public void sauvegarder(String nomFich)
        {          
            IFormatter formatter = new BinaryFormatter();
            Stream stream = new FileStream(nomFich, FileMode.Create, FileAccess.Write, FileShare.None);
            formatter.Serialize(stream, this);
            stream.Close();
        }

        /// <summary>
        /// Déserialize un fichier pour reprendre une partie.
        /// </summary>
        /// <param name="nomFich">Nom du fichier sauvegardé à charger</param>
        public void charger(String nomFich)
        {
            IFormatter formatter = new BinaryFormatter();
            Stream stream = new FileStream(nomFich, FileMode.Open, FileAccess.Read, FileShare.Read);
            Jeu obj = (Jeu)formatter.Deserialize(stream);
            stream.Close();

            //Affecte champs par champs l'instance courante de Jeu
            //par celle qui a été sauvegardé;
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
            this.carte1D = obj.carte1D;
         }

    }
}
