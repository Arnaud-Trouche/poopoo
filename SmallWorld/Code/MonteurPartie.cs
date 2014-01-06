using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

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


        private MonteurPartie()
        {
            //rien ?
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
        }
        public String J1
        {
            set
            {
                j1 = value;
            }
        }
        public String J2
        {
            set
            {
                j2 = value;
            }
        }
        public int P1 {
            set { 
                p1 = value; 
            }
        }
        public int P2
        {
            set
            {
                p2 = value;
            }
        }


        public Carte Carte
        {
            get
            {
                throw new System.NotImplementedException();
            }
            set
            {
            }
        }

        public Joueur Joueur
        {
            get
            {
                throw new System.NotImplementedException();
            }
            set
            {
            }
        }
    
        public bool initialiser()
        {
            throw new System.NotImplementedException();
        }

        public bool restaurer()
        {
            throw new System.NotImplementedException();
        }

        public bool sauvegarder()
        {
            throw new System.NotImplementedException();
        }
    }
}
