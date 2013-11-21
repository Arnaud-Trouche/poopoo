using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Modelisation
{
    public interface iJoueur
    {
        String getNom();

        Peuple getPeuple();

        int getScore();

        void setNom(String nom);

        void setPeuple(string Peuple);

        void setScore(int score);
    }
}
