using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Code
{
    public class Jeu : iJeu
    {
        private Carte c;
        private Joueur j1;
        private Joueur j2;
        private bool j1Joue;

        public static Jeu INSTANCE = new Jeu();

        public Carte Carte
        {
            get
            {
                return c;
            }
        }

        public Joueur recupAdversaire(){
            if (j1Joue) return j2;
            return j1;
        }
    
        public void lancerJeu(Carte c, Joueur j1, Joueur j2)
        {
            this.c = c;
            this.j1 = j1;
            this.j2 = j2;
            j1Joue = true;
        }
    }
}
