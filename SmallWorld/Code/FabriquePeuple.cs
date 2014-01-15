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
    [Serializable]
    public class FabriquePeuple : iFabriquePeuple
    {
        public static FabriquePeuple INSTANCE = new FabriquePeuple();
    
        private FabriquePeuple()
        {
            
        }
        
        /// <summary>
        /// Créer un peuple du type passé en paramètre avec le bon nombre d'unités 
        /// à la bonne position.
        /// </summary>
        /// <param name="peuple">Le type du peuple désiré</param>
        /// <param name="nbUnites">Le nombre d'unités voulu</param>
        /// <param name="pos">La position du peuple créer</param>
        /// <returns></returns>
        public Peuple creerPeuple(int peuple, int nbUnites, Coord pos)
        {
            //Switch sur le type de peuple.
            if (peuple == -1) return null;
            switch (peuple)
            {
                 
                case Constants.GAULOIS:
                    //Créer un PeupleGaulois 
                    return new PeupleGaulois(nbUnites, pos);

                case Constants.NAIN:
                    //Créer un PeupleGaulois 
                    return new PeupleNain(nbUnites, pos);

                case Constants.VIKING:
                    //Créer un PeupleGaulois 
                    return new PeupleViking(nbUnites, pos);

                default:
                    return null;
            }
        }


    }
}
