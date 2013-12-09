using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Modelisation
{
    public class Unite : iUnite
    {
        private int pointVie;
        private int pointDefense;
        private int pointAttaque;
        private Coord position;
        /*private Peuple* pere;
        private Case* caseActuelle;*/
        private int pointDeDeplacement;
    
        public void attaquer(Coord caseAttaquee)
        {
            throw new System.NotImplementedException();
        }

        public void deplacer(Coord caseDeplacement)
        {
            throw new System.NotImplementedException();
        }
    }
}
