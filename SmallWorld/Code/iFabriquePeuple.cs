using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Code
{
    /// <summary>
    /// Classe FabriquePeuple. Elle sert à créer les peuples
    /// avec le bon nombre d'unités et le bon type de peuple.
    /// </summary>
    public interface iFabriquePeuple
    {
        /// <summary>
        /// Créer un peuple du type passé en paramètre avec le bon nombre d'unités 
        /// à la bonne position.
        /// </summary>
        /// <param name="peuple">Le type du peuple désiré</param>
        /// <param name="nbUnites">Le nombre d'unités voulu</param>
        /// <param name="pos">La position du peuple créer</param>
        /// <returns></returns>
        Peuple creerPeuple(int peuple, int nbUnites, Coord pos);
    }
}
