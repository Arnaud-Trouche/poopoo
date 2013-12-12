using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Code
{
    public interface iPeuple
    {
        public iPeuple(int nbUnites, Coord pos);

        public Unite getUnite(Coord coordonnee);
    }
}
