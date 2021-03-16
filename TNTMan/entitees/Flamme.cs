using System;
using System.Drawing;
using TNTMan.map;

namespace TNTMan.entitees
{
    class Flamme : Entite
    {
        Joueur proprietaire;
        DateTime tempsExplosion;
        protected int tempsAvantExtinction;

        public Flamme(int x, int y, Joueur _proprietaire, DateTime temps_actuel, Map _map) : base(_map)
        {
            statut = true;
            position = new PointF(x + 0.5f, y + 0.5f);
            proprietaire = _proprietaire;
            tempsExplosion = temps_actuel;
            tempsAvantExtinction = 300;
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

        public override void mettreAJour()
        {
            DateTime temps_actuel = DateTime.Now;
            if ((temps_actuel - tempsExplosion).TotalMilliseconds > tempsAvantExtinction)
            {
                tuer();
            }
        }
    }
}
