using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Modelisation
{
    public interface iStrategieCarte
    {
        bool creerCarte(int tailleCarte);

        void creerCarte(ref Case[] cases);

        int getNbTours();

        int getNbUnites();
    }
}
