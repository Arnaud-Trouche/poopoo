using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Code
{
    public interface iCase
    {
        int getPtScore(int peuple, Coord coordCase);
        bool deplacementPossible(int peuple, Coord init, Coord nouvelle);

    }
}
