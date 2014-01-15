using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Code
{
    /// <summary>
    /// Monteur partie créer les différents composants qui constituent une partie.
    /// Création de la carte, des joueurs, des peuples, ect...
    /// </summary>
    public interface iMonteurPartie
    {
        /// <summary>
        /// Initialise les éléments du Monteur (Carte, Joueurs, Peuples).
        /// Créer la carte de la bonne taille et créer les joueurs ainsi 
        /// que leur peuple avec le bon nombre d'unités.
        /// </summary>
        /// <returns>Renvoie vrai si l'initialisation a réussi, faux sinon.</returns>
        bool initialiser();

    }
}
