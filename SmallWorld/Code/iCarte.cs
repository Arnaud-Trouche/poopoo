﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Code
{
    public interface Cases
    {
    }

    public interface iCarte
    {
        void creerCarte();

        void definirTaille(int taille);

        int getNbTours();

        int getNbUnites();
    }
}