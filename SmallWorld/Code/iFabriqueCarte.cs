using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Code
{
    public interface iFabriqueCase
    {
       iCase obtenirCase(Coord c);
    }
}
