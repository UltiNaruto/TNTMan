using System;
using System.Drawing;
using TNTMan.map;

namespace TNTMan.entitees
{
    class Flamme : Entite
    {
        Joueur proprietaire;
        protected int tempsAvantExtinction;

        public Flamme(int x, int y, Joueur _proprietaire)
        {
            statut = true;
            position = new PointF(x + 0.5f, y + 0.5f);
            proprietaire = _proprietaire;
            tempsAvantExtinction = 200;
        }

        public Joueur getProprietaire()
        {
            return this.proprietaire;
        }

        public int getTempsAvantExtinction()
        {
            return tempsAvantExtinction;
        }

        public override void dessiner(IntPtr rendu)
        {
            Point _position = Map.getPositionEcranDepuis((int)position.X, (int)position.Y, 32, 32);
            if (tempsAvantExtinction > 0)
            {
                Gfx.remplirRectangle(_position.X, _position.Y, 32, 32, 1, Color.Orange, Color.DarkRed);
            }
        }

        public override void mettreAJour(Map map)
        {
            if (tempsAvantExtinction > 0)
            {
                map.tuerEntiteA(position.X, position.Y);
                tempsAvantExtinction -= 16; // décrémente de 16 ms par image
            }
            else
                tuer();
        }
    }
}
