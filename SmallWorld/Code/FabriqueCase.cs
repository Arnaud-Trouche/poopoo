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
        private Montagne montagne;
        private Eau eau;
        private Desert desert;
        private Plaine plaine;
        private Foret foret;

        private FabriqueCase()
        {
            mapCases = new Dictionary<Coord, iCase>();
            montagne = new Montagne();
            eau = new Eau();
            desert = new Desert();
            plaine = new Plaine();
            foret = new Foret();
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
                        cas = montagne;
                        mapCases.Add(c, montagne);
                        break;

                    case Code.Constants.EAU:
                        cas = eau;
                        mapCases.Add(c, eau);
                        break;

                    case Code.Constants.DESERT:
                        cas = desert;
                        mapCases.Add(c, desert);
                        break;

                    case Code.Constants.PLAINE:
                        cas = plaine;
                        mapCases.Add(c, plaine);
                        break;

                    case Code.Constants.FORET:
                        cas = foret;
                        mapCases.Add(c, foret);
                        break;
                }
            }
            else {
                cas = mapCases[c];
            }
            return cas;
        }
    }
}
