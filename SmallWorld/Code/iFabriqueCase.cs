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
    public interface iFabriqueCase
    {
        /// <summary>
        /// Assigne le tableau contenant les types de cases par celui qui est passé en paramètre.
        /// </summary>
        /// <param name="tab">Tableau unidimensionnel qui sera affecté</param>
        void setTabCases(ref int[] tab);

        /// <summary>
        /// Renvoit la Case (et donc son type) présente à la coordonnée en paramètre
        /// ou si la Case n'existe pas, on la rajoute dans le tableau associatif.
        /// </summary>
        /// <param name="c">La coordonnée concernée</param>
        /// <returns>La Case présente à la coordonnée c.</returns>
        iCase obtenirCase(Coord c);

    }
}
