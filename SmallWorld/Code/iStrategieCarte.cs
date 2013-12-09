using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Modelisation
{
    public interface iStrategieCarte
    {
        int getNbTours();

        int getNbUnites();

        int getHauteurCases();

        void creerCarte();
    }
}
