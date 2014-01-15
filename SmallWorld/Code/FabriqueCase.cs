using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Code
{
    /// <summary>
    /// Classe FabriqueCase. Elle fait office de poids-mouche. 
    /// Elle sert à créer les cases en faisant une instanciation paresseuse.
    /// </summary>
    [Serializable]
    public class FabriqueCase : iFabriqueCase
    {
        //Tableau associatif liant une coordonnée à une Case 
        private Dictionary<Coord, iCase> mapCases;

        //Tableau unidimensionnel qui traduit le type des cases selon la coordonnée.
        private int[] casesInt;

        //Possède une instance de chaque Case
        private Montagne montagne;
        private Eau eau;
        private Desert desert;
        private Plaine plaine;
        private Foret foret;

        /// <summary>
        /// Constructeur de la FabriqueCase qui initialise ses attributs
        /// </summary>
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

        /// <summary>
        /// Renvoit la Case (et donc son type) présente à la coordonnée en paramètre
        /// ou si la Case n'existe pas, on la rajoute dans le tableau associatif.
        /// </summary>
        /// <param name="c">La coordonnée concernée</param>
        /// <returns>La Case présente à la coordonnée c.</returns>
        public iCase obtenirCase(Coord c)
        {
            iCase cas = null;

            //Si on n'a jamais crée  
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
