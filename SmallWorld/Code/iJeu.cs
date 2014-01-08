using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Code
{
    public interface iJeu
    {
        void lancerJeu(Carte c, Joueur j1, Joueur j2, int nnombreTours);
    }
}
