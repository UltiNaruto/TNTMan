using System;
using System.Drawing;

namespace TNTMan.ecrans
{
    class Bouton
    {
        int id;
        Point position;
        Color couleur;
        Color couleurSel;
        string texte;
        bool curseurDans(Point curseur)
        {
            return false;
        }

        void dessiner(IntPtr rendu)
        {

        }
    }
}
