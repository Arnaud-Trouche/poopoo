﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Modelisation
{
    public interface iUnite
    {
        void attaquer(Coord caseAttaquee);

        void deplacer(Coord caseDeplacement);
    }
}