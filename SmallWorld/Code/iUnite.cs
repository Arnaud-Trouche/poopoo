using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Code
{
    /// <summary>
    /// Classe Unite qui gère les Unités et leurs caractéristiques. 
    /// Propose des méthodes gérant le déplacement, l'attaque et le score générer par une unité
    /// </summary>
    public interface iUnite
    {

        /// <summary>
        /// Renvoie un tableau unidimensionnel représentant les cases sur lesquelles une unité 
        /// est en mesure de se déplacer
        /// </summary>
        /// <returns>Tableau unidimensionnel des cases où l'unité peut se déplacer</returns>
        int[] deplacementPossibles();

        /// <summary>
        /// Fonction attaquer : détermination du meilleur adversaire, du nombre de combat à faire
        /// et lance les combats.
        /// Retourne Vrai si l'unité attaqué a gagné le combat, faux sinon.
        /// </summary>
        /// <param name="caseAttaquee"></param>
        /// <returns></returns>
        bool attaquer(Coord caseAttaquee);

        /// <summary>
        /// Enclenche les affrontements entre l'attanquant et l'attaqué
        /// S'arrête si le nombre d'affrontements est atteint ou si une des deux unités est morte
        /// Renvoie Vrai si l'attaquant a gagné le combat et Faux sinon
        /// </summary>
        /// <param name="adverse">Adversaire que l'attaquant va défier</param>
        /// <param name="nbCombats">Nombre d'affrontements à réaliser entre les deux unités</param>
        /// <returns></returns>
        bool combat(Unite adverse, int nbCombats);

        /// <summary>
        /// Vérifie si le déplacement de l'unité sur la case voulue est possible (selon les points de déplacement, le peuple en question)
        /// enclenche un combat si le déplacement se fait sur une case occupée par des unités adverses.
        /// </summary>
        /// <param name="caseDeplacement">Case où l'on souhaite se déplacer </param>
        void deplacer(Coord caseDeplacement);

        /// <summary>
        /// Renvoie le score que génère une unité en fonction de son peuple, sa position sur la carte.
        /// </summary>
        /// <returns>Le score que l'unité génère.</returns>
        int score();


        /// <summary>
        /// Remet le point de déplacement à l'unité (invoquée à cahque nouveau tour).
        /// </summary>
        void debutTour();

        Coord Position { get; }
    }
}
