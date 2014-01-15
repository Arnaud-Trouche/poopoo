using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Code
{
    /// <summary>
    /// Classe représenant une Carte de jeu.
    /// Elle contient le nombre de Tours, le nombre d'unités par joueur
    /// et la taille en nombre de case par coté de la carte. 
    /// </summary>
    public interface iCarte
    {

        /// <summary>
        /// Définit la taille de la Carte en fonction de la difficulté.
        /// </summary>
        /// <param name="taille">Taille de la Carte</param>
        void definirTaille(int taille);


        /// <summary>
        /// Renvoie la taille de la Carte.
        /// </summary>
        /// <returns>La taille de la Carte</returns>
        int getTaille();
    }
}
