using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Code
{
    public class MonteurPartie : iMonteurPartie
    {

        // ATTENTION !!
        // penser à transmettre le tableau de case à FabriqueCase
        // AVANT de faire des appels à obtenirCase

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
