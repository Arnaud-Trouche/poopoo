using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Code
{
    [Serializable]
    public class FabriqueCase : iFabriqueCase
    {
        private Dictionary<Coord, iCase> mapCases;
        private int[] casesInt;
        private Montagne montagne;
        private Eau eau;
        private Desert desert;
        private Plaine plaine;
        private Foret foret;

        public FabriqueCase()
        {
            mapCases = new Dictionary<Coord, iCase>();
            montagne = new Montagne();
            eau = new Eau();
            desert = new Desert();
            plaine = new Plaine();
            foret = new Foret();
        }

        /// <summary>
        /// Assigne le tableau contenant les types de cases par celui qui est passé en paramètre.
        /// </summary>
        /// <param name="tab">Tableau unidimensionnel qui sera affecté</param>
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
