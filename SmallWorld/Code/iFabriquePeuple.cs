using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Code
{
    public interface iFabriquePeuple
    {

        iPeuple creerPeuple(int peuple, int nbUnites, Coord pos);
    }
}
