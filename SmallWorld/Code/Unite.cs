using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Code
{
    public class Unite : iUnite
    {
        private int pointVie;
        private int pointDefense;
        private int pointAttaque;
        private int pointDeDeplacement;
        private int peuple;
        private Coord position;

        public int PointVie
        {
            get
            {
                return pointVie;
            }
        }

        public int PointAttaque
        {
            get
            {
                return pointAttaque;
            }
        }
        public int PointDefense
        {
            get
            {
                return pointDefense;
            }
        }
        public int PointDeplacement
        {
            get
            {
                return pointDeDeplacement;
            }
        }

        public int Peuple    // the peuple property
        {
            get
            {
                return peuple;
            }
        }

        public Coord Position    // the position property
        {
            get
            {
                return position;
            }
        }

        public Unite(Coord pos, int peuple)
        {
            this.pointVie = 2;
            this.pointDefense = 1;
            this.pointAttaque = 2;
            this.position = pos;
            this.peuple = peuple;
            this.pointDeDeplacement = 0;
        }

        public void action(Coord casecliquee)
        {
            Peuple p = Jeu.INSTANCE.recupAdversaire().Peuple;
            if (p.getUnite(casecliquee) != null)
            {
                attaquer(casecliquee);
            }
            else
            {
                deplacer(casecliquee);    
            }
        }
    
        public void attaquer(Coord caseAttaquee)
        {
            throw new System.NotImplementedException();
        }

        public void deplacer(Coord caseDeplacement)
        {
            throw new System.NotImplementedException();
        }

        public void debutTour()
        {
            this.pointDeDeplacement = 1;
        }


    }
}
