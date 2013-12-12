using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Code
{
    public class FabriqueCase : iFabriqueCase
    {
        public static FabriqueCase INSTANCE = new FabriqueCase();

        private Dictionary<Coord, iCase> mapCases;
        private int[] casesInt;

        public FabriqueCase()
        {
            mapCases = new Dictionary<Coord, iCase>();
        }

        public void setTabCases(ref int[] tab){
            casesInt = tab;
        }

        public iCase obtenirCase(Coord c)
        {
            iCase cas = null;
            if (!mapCases.ContainsKey(c))
            {
                int type = casesInt[c.getIndiceTab1Dimension()];
                switch (type)
                {
                    case Code.Constants.MONTAGNE:
                        cas = new Montagne();
                        break;

                    case Code.Constants.EAU:
                        cas = new Eau();
                        break;

                    case Code.Constants.DESERT:
                        cas = new Desert();
                        break;

                    case Code.Constants.PLAINE:
                        cas = new Plaine();
                        break;

                    case Code.Constants.FORET:
                        cas = new Foret();
                        break;
                }
                mapCases.Add(c, cas);
            }
            else {
                cas = mapCases[c];
            }
            return cas;
        }
    }
}
